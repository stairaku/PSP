using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO.Compression;
using System.Linq;
using System.Threading.Channels;
using DocumentFormat.OpenXml.Office2010.Excel;
using FluentAssertions;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using Moq;
using NetTopologySuite.Geometries;
using NExpect.Interfaces;
using Pims.Api.Constants;
using Pims.Api.Helpers.Exceptions;
using Pims.Api.Models.CodeTypes;
using Pims.Api.Models.Concepts;
using Pims.Api.Services;
using Pims.Core.Exceptions;
using Pims.Core.Test;
using Pims.Dal;
using Pims.Dal.Entities;
using Pims.Dal.Entities.Models;
using Pims.Dal.Exceptions;
using Pims.Dal.Repositories;
using Pims.Dal.Security;
using Xunit;

namespace Pims.Api.Test.Services
{
    [Trait("category", "unit")]
    [Trait("category", "api")]
    [Trait("group", "propertyoperation")]
    [ExcludeFromCodeCoverage]
    public class PropertyOperationServiceTest
    {
        private readonly TestHelper _helper;

        public PropertyOperationServiceTest()
        {
            this._helper = new TestHelper();
        }

        private PropertyOperationService CreateDispositionServiceWithPermissions(params Permissions[] permissions)
        {
            var user = PrincipalHelper.CreateForPermission(permissions);
            return this._helper.Create<PropertyOperationService>(user);
        }

        #region Subdivide

        [Fact]
        public void Subdivide_Should_Fail_NoPermission()
        {
            // Arrange
            var service = this.CreateDispositionServiceWithPermissions();

            // Act
            Action act = () => service.SubdivideProperty(new List<PimsPropertyOperation>());

            // Assert
            act.Should().Throw<NotAuthorizedException>();
        }

        [Fact]
        public void Subdivide_Should_Fail_SourceRetired()
        {
            // Arrange
            var service = this.CreateDispositionServiceWithPermissions(Permissions.PropertyEdit);

            var propertyService = this._helper.GetService<Mock<IPropertyService>>();
            var retiredProperty = EntityHelper.CreateProperty(3);
            retiredProperty.IsRetired = true;
            propertyService.Setup(x => x.GetById(It.IsAny<long>())).Returns(retiredProperty);

            var operation = EntityHelper.CreatePropertyOperation();
            var operations = new List<PimsPropertyOperation>() { operation };

            // Act
            Action act = () => service.SubdivideProperty(operations);

            // Assert
            var exception = act.Should().Throw<BusinessRuleViolationException>();
            exception.WithMessage("Retired properties cannot be subdivided.");
        }

        [Fact]
        public void Subdivide_Should_Fail_MismatchNumbers()
        {
            // Arrange
            var service = this.CreateDispositionServiceWithPermissions(Permissions.PropertyEdit);
            var propertyService = this._helper.GetService<Mock<IPropertyService>>();
            propertyService.Setup(x => x.GetById(It.IsAny<long>())).Returns(EntityHelper.CreateProperty(3));

            var operationOne = EntityHelper.CreatePropertyOperation();
            operationOne.PropertyOperationNo = -1;
            var operations = new List<PimsPropertyOperation>() { operationOne, EntityHelper.CreatePropertyOperation() };

            // Act
            Action act = () => service.SubdivideProperty(operations);

            // Assert
            var exception = act.Should().Throw<BusinessRuleViolationException>();
            exception.WithMessage("All property operations must have matching operation numbers.");
        }

        [Fact]
        public void Subdivide_Should_Fail_MismatchTypes()
        {
            // Arrange
            var service = this.CreateDispositionServiceWithPermissions(Permissions.PropertyEdit);
            var propertyService = this._helper.GetService<Mock<IPropertyService>>();
            propertyService.Setup(x => x.GetById(It.IsAny<long>())).Returns(EntityHelper.CreateProperty(3));

            var operationOne = EntityHelper.CreatePropertyOperation();
            operationOne.PropertyOperationTypeCode = PropertyOperationTypes.CONSOLIDATE.ToString();
            var operations = new List<PimsPropertyOperation>() { operationOne, EntityHelper.CreatePropertyOperation() };

            // Act
            Action act = () => service.SubdivideProperty(operations);

            // Assert
            var exception = act.Should().Throw<BusinessRuleViolationException>();
            exception.WithMessage("All property operations must have matching type codes.");
        }

        [Fact]
        public void Subdivide_Should_Fail_MismatchSource()
        {
            // Arrange
            var service = this.CreateDispositionServiceWithPermissions(Permissions.PropertyEdit);
            var propertyService = this._helper.GetService<Mock<IPropertyService>>();
            propertyService.Setup(x => x.GetById(It.IsAny<long>())).Returns(EntityHelper.CreateProperty(3));

            var operationOne = EntityHelper.CreatePropertyOperation();
            operationOne.SourcePropertyId = 2;
            var operations = new List<PimsPropertyOperation>() { operationOne, EntityHelper.CreatePropertyOperation() };

            // Act
            Action act = () => service.SubdivideProperty(operations);

            // Assert
            var exception = act.Should().Throw<BusinessRuleViolationException>();
            exception.WithMessage("All property operations must have the same PIMS parent property.");
        }

        [Fact]
        public void Subdivide_Should_Fail_DestinationEmpty()
        {
            // Arrange
            var service = this.CreateDispositionServiceWithPermissions(Permissions.PropertyEdit);
            var propertyService = this._helper.GetService<Mock<IPropertyService>>();
            propertyService.Setup(x => x.GetById(It.IsAny<long>())).Returns(EntityHelper.CreateProperty(3));

            var operationOne = EntityHelper.CreatePropertyOperation();
            var operations = new List<PimsPropertyOperation>() { operationOne };

            // Act
            Action act = () => service.SubdivideProperty(operations);

            // Assert
            var exception = act.Should().Throw<BusinessRuleViolationException>();
            exception.WithMessage("Subdivisions must contain at least two child properties.");
        }

        [Fact]
        public void Subdivide_Should_Fail_PidExists()
        {
            // Arrange
            var service = this.CreateDispositionServiceWithPermissions(Permissions.PropertyEdit);
            var propertyService = this._helper.GetService<Mock<IPropertyService>>();
            propertyService.Setup(x => x.GetById(It.IsAny<long>())).Returns(EntityHelper.CreateProperty(3));
            propertyService.Setup(x => x.GetByPid(It.IsAny<string>())).Returns(EntityHelper.CreateProperty(4));

            var operations = new List<PimsPropertyOperation>() { EntityHelper.CreatePropertyOperation(), EntityHelper.CreatePropertyOperation() };

            // Act
            Action act = () => service.SubdivideProperty(operations);

            // Assert
            var exception = act.Should().Throw<BusinessRuleViolationException>();
            exception.WithMessage("Subdivision children may not already be in the PIMS inventory.");
        }

        [Fact]
        public void Subdivide_Success()
        {
            // Arrange
            var service = this.CreateDispositionServiceWithPermissions(Permissions.PropertyEdit);
            var propertyService = this._helper.GetService<Mock<IPropertyService>>();
            var sameProperty = EntityHelper.CreateProperty(3);
            propertyService.Setup(x => x.GetById(It.IsAny<long>())).Returns(sameProperty);
            propertyService.Setup(x => x.GetByPid(It.IsAny<string>())).Throws(new KeyNotFoundException());
            propertyService.Setup(x => x.Update(It.IsAny<PimsProperty>(), false)).Returns(sameProperty);
            propertyService.Setup(x => x.PopulateNewProperty(It.IsAny<PimsProperty>(), true, false)).Returns(sameProperty);

            var operations = new List<PimsPropertyOperation>() { EntityHelper.CreatePropertyOperation(), EntityHelper.CreatePropertyOperation() };

            var repository = this._helper.GetService<Mock<IPropertyOperationRepository>>();
            repository.Setup(x => x.AddRange(It.IsAny<List<PimsPropertyOperation>>())).Returns(operations);

            // Act
            var response = service.SubdivideProperty(operations);

            // Assert
            repository.Verify(x => x.AddRange(It.IsAny<List<PimsPropertyOperation>>()), Times.Once);
            propertyService.Verify(x => x.Update(It.IsAny<PimsProperty>(), false), Times.Once);
            propertyService.Verify(x => x.PopulateNewProperty(It.IsAny<PimsProperty>(), true, false), Times.Exactly(2));
        }

        [Fact]
        public void Subdivide_Success_SameSourceDestinationPid()
        {
            // Arrange
            var service = this.CreateDispositionServiceWithPermissions(Permissions.PropertyEdit);
            var propertyService = this._helper.GetService<Mock<IPropertyService>>();
            var sameProperty = EntityHelper.CreateProperty(3);
            propertyService.Setup(x => x.GetById(It.IsAny<long>())).Returns(sameProperty);
            propertyService.Setup(x => x.GetByPid(It.IsAny<string>())).Returns(sameProperty);
            propertyService.Setup(x => x.Update(It.IsAny<PimsProperty>(), false)).Returns(sameProperty);
            propertyService.Setup(x => x.PopulateNewProperty(It.IsAny<PimsProperty>(), true, false)).Returns(sameProperty);

            var operationWithSameDestSource = EntityHelper.CreatePropertyOperation();
            operationWithSameDestSource.DestinationProperty = sameProperty;
            operationWithSameDestSource.SourceProperty = sameProperty;
            var operations = new List<PimsPropertyOperation>() { operationWithSameDestSource, operationWithSameDestSource };

            var repository = this._helper.GetService<Mock<IPropertyOperationRepository>>();
            repository.Setup(x => x.AddRange(It.IsAny<List<PimsPropertyOperation>>())).Returns(operations);

            // Act
            var response = service.SubdivideProperty(operations);

            // Assert
            repository.Verify(x => x.AddRange(It.IsAny<List<PimsPropertyOperation>>()), Times.Once);
            propertyService.Verify(x => x.Update(It.IsAny<PimsProperty>(), false), Times.Once);
            propertyService.Verify(x => x.PopulateNewProperty(It.IsAny<PimsProperty>(), true, false), Times.Exactly(2));
        }

        #endregion
    }
}