import {
  Api_AcquisitionFile,
  Api_AcquisitionFileRepresentative,
  Api_AcquisitionFileSolicitor,
} from './AcquisitionFile';
import { Api_AuditFields } from './AuditFields';
import { Api_CompensationRequisition } from './CompensationRequisition';
import { Api_ConcurrentVersion_Null } from './ConcurrentVersion';
import { Api_InterestHolder } from './InterestHolder';
import { Api_PayeeCheque } from './PayeeCheque';
import { Api_Person } from './Person';

export interface Api_CompensationPayee extends Api_ConcurrentVersion_Null, Api_AuditFields {
  id: number | null;
  compensationRequisitionId: number | null;
  acquisitionOwnerId: number | null;
  interestHolderId: number | null;
  ownerRepresentativeId: number | null;
  ownerSolicitorId: number | null;
  motiSolicitorId: number | null;
  acquisitionFilePersonId: number | null;
  motiSolicitor: Api_Person | null;
  acquisitionOwner: Api_AcquisitionFile | null;
  compensationRequisition: Api_CompensationRequisition | null;
  interestHolder: Api_InterestHolder | null;
  ownerRepresentative: Api_AcquisitionFileRepresentative | null;
  ownerSolicitor: Api_AcquisitionFileSolicitor | null;
  cheques: Api_PayeeCheque[] | null;
  isDisabled: boolean | null;
}