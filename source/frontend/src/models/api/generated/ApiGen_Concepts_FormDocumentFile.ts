/**
 * File autogenerated by TsGenerator.
 * Do not manually modify, changes made to this file will be lost when this file is regenerated.
 */
import { ApiGen_Base_BaseAudit } from './ApiGen_Base_BaseAudit';
import { ApiGen_Concepts_FormDocumentType } from './ApiGen_Concepts_FormDocumentType';

// LINK: @backend/apimodels/Models/Concepts/FormDocument/FormDocumentFileModel.cs
export interface ApiGen_Concepts_FormDocumentFile extends ApiGen_Base_BaseAudit {
  id: number | null;
  fileId: number;
  formDocumentType: ApiGen_Concepts_FormDocumentType | null;
}
