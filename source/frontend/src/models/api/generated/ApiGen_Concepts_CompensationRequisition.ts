/**
 * File autogenerated by TsGenerator.
 * Do not manually modify, changes made to this file will be lost when this file is regenerated.
 */
import { UtcIsoDate } from '@/models/api/UtcIsoDateTime';

import { ApiGen_Base_BaseAudit } from './ApiGen_Base_BaseAudit';
import { ApiGen_Concepts_AcquisitionFile } from './ApiGen_Concepts_AcquisitionFile';
import { ApiGen_Concepts_AcquisitionFileOwner } from './ApiGen_Concepts_AcquisitionFileOwner';
import { ApiGen_Concepts_AcquisitionFileTeam } from './ApiGen_Concepts_AcquisitionFileTeam';
import { ApiGen_Concepts_CompensationFinancial } from './ApiGen_Concepts_CompensationFinancial';
import { ApiGen_Concepts_CompensationRequisitionProperty } from './ApiGen_Concepts_CompensationRequisitionProperty';
import { ApiGen_Concepts_FinancialCode } from './ApiGen_Concepts_FinancialCode';
import { ApiGen_Concepts_InterestHolder } from './ApiGen_Concepts_InterestHolder';
import { ApiGen_Concepts_Project } from './ApiGen_Concepts_Project';

// LINK: @backend/apimodels/Models/Concepts/CompensationRequisition/CompensationRequisitionModel.cs
export interface ApiGen_Concepts_CompensationRequisition extends ApiGen_Base_BaseAudit {
  id: number | null;
  acquisitionFileId: number;
  acquisitionFile: ApiGen_Concepts_AcquisitionFile | null;
  isDraft: boolean | null;
  fiscalYear: string | null;
  yearlyFinancialId: number | null;
  yearlyFinancial: ApiGen_Concepts_FinancialCode | null;
  chartOfAccountsId: number | null;
  chartOfAccounts: ApiGen_Concepts_FinancialCode | null;
  responsibilityId: number | null;
  responsibility: ApiGen_Concepts_FinancialCode | null;
  finalizedDate: UtcIsoDate | null;
  agreementDate: UtcIsoDate | null;
  expropriationNoticeServedDate: UtcIsoDate | null;
  expropriationVestingDate: UtcIsoDate | null;
  advancedPaymentServedDate: UtcIsoDate | null;
  generationDate: UtcIsoDate | null;
  financials: ApiGen_Concepts_CompensationFinancial[] | null;
  acquisitionOwnerId: number | null;
  acquisitionOwner: ApiGen_Concepts_AcquisitionFileOwner | null;
  interestHolderId: number | null;
  interestHolder: ApiGen_Concepts_InterestHolder | null;
  acquisitionFileTeamId: number | null;
  acquisitionFileTeam: ApiGen_Concepts_AcquisitionFileTeam | null;
  legacyPayee: string | null;
  isPaymentInTrust: boolean | null;
  gstNumber: string | null;
  specialInstruction: string | null;
  detailedRemarks: string | null;
  alternateProjectId: number | null;
  alternateProject: ApiGen_Concepts_Project | null;
  compensationRequisitionProperties: ApiGen_Concepts_CompensationRequisitionProperty[] | null;
}
