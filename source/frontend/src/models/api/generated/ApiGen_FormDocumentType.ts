/**
 * File autogenerated by TsGenerator.
 * Do not manually modify, changes made to this file will be lost when this file is regenerated.
 * Generated on 27/11/2023 10:19
 */
import { ApiGen_BaseAudit } from './ApiGen_BaseAudit';

// LINK: @backend/apimodels/Models/Concepts/FormDocument/FormDocumentTypeModel.cs
export interface ApiGen_FormDocumentType extends ApiGen_BaseAudit {
  formTypeCode: string | null;
  documentId: number | null;
  description: string | null;
  displayOrder: number | null;
}