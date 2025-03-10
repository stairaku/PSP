using System;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Pims.Api.Services;
using Pims.Core.Test;
using Pims.Dal.Exceptions;
using Pims.Core.Security;
using Xunit;
using Pims.Core.Exceptions;

namespace Pims.Api.Test.Services
{
    [Trait("category", "unit")]
    [Trait("category", "api")]
    [Trait("area", "expropriation-payments")]
    [ExcludeFromCodeCoverage]
    public class ExpropriationPaymentServiceTest
    {
        private readonly TestHelper _helper;

        public ExpropriationPaymentServiceTest()
        {
            this._helper = new TestHelper();
        }

        [Fact]
        public void GetById_NoPermission()
        {
            var service = this.CreateServiceWithPermissions();

            // Act
            Action act = () => service.GetById(1);

            // Assert
            act.Should().Throw<NotAuthorizedException>();
        }

        private ExpropriationPaymentService CreateServiceWithPermissions(params Permissions[] permissions)
        {
            var user = PrincipalHelper.CreateForPermission(permissions);
            this._helper.CreatePimsContext(user, true);
            return this._helper.Create<ExpropriationPaymentService>(user);
        }
    }
}
