using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Pims.Api.Areas.Property.Controllers;
using Pims.Core.Comparers;
using Pims.Core.Test;
using Pims.Dal.Security;
using Pims.Dal.Services;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Pims.Api.Test.Controllers.Property
{
    [Trait("category", "unit")]
    [Trait("category", "api")]
    [Trait("group", "property")]
    [ExcludeFromCodeCoverage]
    public class PropertyControllerTest
    {
        #region Get
        /// <summary>
        /// Make a successful request to fetch property by PID.
        /// </summary>
        [Fact]
        public void GetConceptPropertyByPid_Success()
        {
            // Arrange
            var pid = 12345;
            var helper = new TestHelper();
            var controller = helper.CreateController<PropertyController>(Permissions.PropertyView);
            var property = EntityHelper.CreateProperty(pid);

            var service = helper.GetService<Mock<IPimsService>>();
            var mapper = helper.GetService<IMapper>();

            service.Setup(m => m.PropertyService.GetByPid(It.IsAny<string>())).Returns(property);

            // Act
            var result = controller.GetConceptPropertyWithPid(pid.ToString());

            // Assert
            var actionResult = Assert.IsType<JsonResult>(result);
            var actualResult = Assert.IsType<Models.Concepts.PropertyModel>(actionResult.Value);
            var expectedResult = mapper.Map<Models.Concepts.PropertyModel>(property);
            Assert.Equal(expectedResult, actualResult, new DeepPropertyCompare());
            service.Verify(m => m.PropertyService.GetByPid(It.IsAny<string>()), Times.Once());
        }
        #endregion
        #region Update
        /// <summary>
        /// Make a successful request to update a property.
        /// </summary>
        [Fact]
        public void UpdateConceptProperty_Success()
        {
            // Arrange
            var helper = new TestHelper();
            var controller = helper.CreateController<PropertyController>(Permissions.PropertyEdit);
            var property = EntityHelper.CreateProperty(12345);

            var repository = helper.GetService<Mock<IPimsService>>();
            var mapper = helper.GetService<IMapper>();

            repository.Setup(m => m.PropertyService.Update(It.IsAny<Pims.Dal.Entities.PimsProperty>())).Returns(property);

            // Act
            var result = controller.UpdateConceptProperty(mapper.Map<Models.Concepts.PropertyModel>(property));

            // Assert
            var actionResult = Assert.IsType<JsonResult>(result);
            var actualResult = Assert.IsType<Models.Concepts.PropertyModel>(actionResult.Value);
            var expectedResult = mapper.Map<Models.Concepts.PropertyModel>(property);
            Assert.Equal(expectedResult, actualResult, new DeepPropertyCompare());
            repository.Verify(m => m.PropertyService.Update(It.IsAny<Pims.Dal.Entities.PimsProperty>()), Times.Once());
        }
        #endregion
    }
}