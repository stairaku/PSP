/**
 * File autogenerated by TsGenerator.
 * Do not manually modify, changes made to this file will be lost when this file is regenerated.
 */
import { UtcIsoDate } from '@/models/api/UtcIsoDateTime';

import { ApiGen_Base_BaseAudit } from './ApiGen_Base_BaseAudit';
import { ApiGen_Base_CodeType } from './ApiGen_Base_CodeType';
import { ApiGen_Concepts_Payment } from './ApiGen_Concepts_Payment';

// LINK: @backend/apimodels/Models/Concepts/Lease/LeasePeriodModel.cs
export interface ApiGen_Concepts_LeasePeriod extends ApiGen_Base_BaseAudit {
  id: number | null;
  leaseId: number;
  gstAmount: number | null;
  additionalRentGstAmount: number | null;
  variableRentGstAmount: number | null;
  paymentAmount: number | null;
  statusTypeCode: ApiGen_Base_CodeType<string> | null;
  leasePmtFreqTypeCode: ApiGen_Base_CodeType<string> | null;
  startDate: UtcIsoDate | null;
  expiryDate: UtcIsoDate | null;
  renewalDate: UtcIsoDate | null;
  paymentDueDateStr: string | null;
  paymentNote: string | null;
  isGstEligible: boolean;
  isTermExercised: boolean;
  isFlexible: boolean;
  isVariable: boolean;
  additionalRentPaymentAmount: number | null;
  isAdditionalRentGstEligible: boolean | null;
  additionalRentFreqTypeCode: ApiGen_Base_CodeType<string> | null;
  variableRentPaymentAmount: number | null;
  isVariableRentGstEligible: boolean | null;
  variableRentFreqTypeCode: ApiGen_Base_CodeType<string> | null;
  payments: ApiGen_Concepts_Payment[] | null;
}