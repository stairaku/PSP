using Pims.Api.Areas.Property.Controllers;
using Pims.Api.Models.Concepts;
using Pims.Core.Extensions;
using Pims.Core.Test;
using Pims.Dal.Security;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Pims.Api.Test.Routes
{
    /// <summary>
    /// PropertyControllerTest class, provides a way to test endpoint routes.
    /// </summary>
    [Trait("category", "unit")]
    [Trait("category", "api")]
    [Trait("group", "property")]
    [Trait("group", "route")]
    [ExcludeFromCodeCoverage]
    public class PropertyControllerTest
    {

        #region Constructors
        public PropertyControllerTest()
        {
        }
        #endregion

        #region Tests
        [Fact]
        public void Controller_Route()
        {
            // Arrange
            // Act
            // Assert
            var type = typeof(PropertyController);
            type.HasAuthorize();
            type.HasArea("properties");
            type.HasRoute("[area]");
            type.HasRoute("v{version:apiVersion}/[area]");
        }

        [Fact]
        public void GetConceptPropertyWithPid_Route()
        {
            // Arrange
            var endpoint = typeof(PropertyController).FindMethod(nameof(PropertyController.GetConceptPropertyWithPid), typeof(string));

            // Act
            // Assert
            Assert.NotNull(endpoint);
            endpoint.HasGet("concept/{pid}");
            endpoint.HasPermissions(Permissions.PropertyView);
        }

        [Fact]
        public void UpdateConceptProperty_Route()
        {
            // Arrange
            var endpoint = typeof(PropertyController).FindMethod(nameof(PropertyController.UpdateConceptProperty), typeof(PropertyModel));

            // Act
            // Assert
            Assert.NotNull(endpoint);
            endpoint.HasPut("concept/{pid}");
            endpoint.HasPermissions(Permissions.PropertyEdit);
        }
        #endregion
    }
}