/**
 * File autogenerated by TsGenerator.
 * Do not manually modify, changes made to this file will be lost when this file is regenerated.
 * Generated on 27/11/2023 10:19
 */
import { ApiGen_BaseAudit } from './ApiGen_BaseAudit';
import { ApiGen_PropertyActivity } from './ApiGen_PropertyActivity';

// LINK: @backend/apimodels/Models/Concepts/Property/PropertyActivityInvoiceModel.cs
export interface ApiGen_PropertyActivityInvoice extends ApiGen_BaseAudit {
  id: number;
  invoiceDateTime: string;
  invoiceNum: string | null;
  description: string | null;
  pretaxAmount: number;
  gstAmount: number | null;
  pstAmount: number | null;
  totalAmount: number | null;
  isPstRequired: boolean | null;
  isDisabled: boolean | null;
  propertyActivityId: number;
  propertyActivity: ApiGen_PropertyActivity | null;
}