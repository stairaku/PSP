using System.Collections.Generic;
using Pims.Dal.Entities;
using Pims.Dal.Entities.Models;

namespace Pims.Dal.Repositories
{
    public interface ICompReqFinancialRepository : IRepository<PimsCompReqFinancial>
    {
        IEnumerable<PimsCompReqFinancial> GetAllByAcquisitionFileId(long acquisitionFileId, bool? finalOnly);

        IEnumerable<PimsCompReqFinancial> GetAllByLeaseFileId(long leaseFileId, bool? finalOnly);

        IEnumerable<PimsCompReqFinancial> SearchCompensationRequisitionFinancials(AcquisitionReportFilterModel filter, bool includeAcquisitions = true, bool includeLeases = true);
    }
}
