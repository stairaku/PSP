using FluentAssertions;
using Moq;
using Pims.Api.Services;
using Pims.Core.Test;
using Pims.Dal.Exceptions;
using Pims.Dal.Repositories;
using Pims.Dal.Security;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Pims.Api.Test.Services
{
    [Trait("category", "unit")]
    [Trait("category", "api")]
    [Trait("area", "compensation-requisition")]
    [ExcludeFromCodeCoverage]

    public class CompReqFinancialServiceTest
    {
        private readonly TestHelper _helper;

        public CompReqFinancialServiceTest()
        {
            _helper = new TestHelper();
        }

        [Fact]
        public void GetAllByAcquisitionFileId_Success()
        {
            // Arrange
            var service = CreateWithPermissions(Permissions.CompensationRequisitionView, Permissions.AcquisitionFileView);
            var repo = _helper.GetService<Mock<ICompReqFinancialRepository>>();
            repo.Setup(x => x.GetAllByAcquisitionFileId(It.IsAny<long>(), null));

            // Act
            service.GetAllByAcquisitionFileId(1, null);

            // Assert
            repo.Verify(x => x.GetAllByAcquisitionFileId(It.IsAny<long>(), null), Times.Once);
        }

        [Fact]
        public void GetAllByAcquisitionFileId_Unauthorized()
        {
            // Arrange
            var service = CreateWithPermissions(Permissions.CompensationRequisitionEdit);
            var repo = _helper.GetService<Mock<ICompReqFinancialRepository>>();

            Action act = () => service.GetAllByAcquisitionFileId(1, null);
            act.Should().Throw<NotAuthorizedException>();
        }


        private CompReqFinancialService CreateWithPermissions(params Permissions[] permissions)
        {
            var user = PrincipalHelper.CreateForPermission(permissions);
            return _helper.Create<CompReqFinancialService>(user);
        }
    }
}