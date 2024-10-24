/**
 * File autogenerated by TsGenerator.
 * Do not manually modify, changes made to this file will be lost when this file is regenerated.
 */
import { UtcIsoDate } from '@/models/api/UtcIsoDateTime';

import { ApiGen_Base_CodeType } from './ApiGen_Base_CodeType';
import { ApiGen_Concepts_ConsultationLease } from './ApiGen_Concepts_ConsultationLease';
import { ApiGen_Concepts_FileWithChecklist } from './ApiGen_Concepts_FileWithChecklist';
import { ApiGen_Concepts_LeasePeriod } from './ApiGen_Concepts_LeasePeriod';
import { ApiGen_Concepts_LeaseRenewal } from './ApiGen_Concepts_LeaseRenewal';
import { ApiGen_Concepts_LeaseTenant } from './ApiGen_Concepts_LeaseTenant';
import { ApiGen_Concepts_Project } from './ApiGen_Concepts_Project';
import { ApiGen_Concepts_PropertyLease } from './ApiGen_Concepts_PropertyLease';

// LINK: @backend/apimodels/Models/Concepts/Lease/LeaseModel.cs
export interface ApiGen_Concepts_Lease extends ApiGen_Concepts_FileWithChecklist {
  amount: number | null;
  motiName: string | null;
  programName: string | null;
  documentationReference: string | null;
  note: string | null;
  description: string | null;
  lFileNo: string | null;
  tfaFileNumber: string | null;
  psFileNo: string | null;
  otherCategoryType: string | null;
  otherProgramType: string | null;
  otherPurposeType: string | null;
  otherType: string | null;
  startDate: UtcIsoDate | null;
  expiryDate: UtcIsoDate | null;
  terminationDate: UtcIsoDate | null;
  renewalCount: number;
  paymentReceivableType: ApiGen_Base_CodeType<string> | null;
  type: ApiGen_Base_CodeType<string> | null;
  initiatorType: ApiGen_Base_CodeType<string> | null;
  responsibilityType: ApiGen_Base_CodeType<string> | null;
  categoryType: ApiGen_Base_CodeType<string> | null;
  purposeType: ApiGen_Base_CodeType<string> | null;
  region: ApiGen_Base_CodeType<number> | null;
  programType: ApiGen_Base_CodeType<string> | null;
  returnNotes: string | null;
  responsibilityEffectiveDate: UtcIsoDate | null;
  fileProperties: ApiGen_Concepts_PropertyLease[] | null;
  consultations: ApiGen_Concepts_ConsultationLease[] | null;
  tenants: ApiGen_Concepts_LeaseTenant[] | null;
  periods: ApiGen_Concepts_LeasePeriod[] | null;
  renewals: ApiGen_Concepts_LeaseRenewal[] | null;
  isResidential: boolean;
  isCommercialBuilding: boolean;
  isOtherImprovement: boolean;
  hasPhysicalFile: boolean;
  hasDigitalFile: boolean;
  hasPhysicalLicense: boolean | null;
  hasDigitalLicense: boolean | null;
  cancellationReason: string | null;
  terminationReason: string | null;
  primaryArbitrationCity: string | null;
  isExpired: boolean;
  project: ApiGen_Concepts_Project | null;
  isPublicBenefit: boolean | null;
  isFinancialGain: boolean | null;
  feeDeterminationNote: string | null;
}
