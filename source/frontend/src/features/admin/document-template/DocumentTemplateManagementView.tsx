import { Col, Row } from 'react-bootstrap';
import Form from 'react-bootstrap/Form';
import styled from 'styled-components';

import AdminIcon from '@/assets/images/admin-icon.svg?react';
import LoadingBackdrop from '@/components/common/LoadingBackdrop';
import { Scrollable as ScrollableBase } from '@/components/common/Scrollable/Scrollable';
import { Section } from '@/components/common/Section/Section';
import * as CommonStyled from '@/components/common/styles';
import DocumentListContainer from '@/features/documents/list/DocumentListContainer';
import { ApiGen_CodeTypes_DocumentRelationType } from '@/models/api/generated/ApiGen_CodeTypes_DocumentRelationType';
import { ApiGen_Concepts_FormDocumentType } from '@/models/api/generated/ApiGen_Concepts_FormDocumentType';

export interface IDocumentTemplateManagementViewProp {
  isLoading: boolean;
  formDocumentTypes: ApiGen_Concepts_FormDocumentType[] | undefined;
  selectedFormDocumentTypeCode: string | undefined;
  setSelectedFormDocumentTypeCode: (formTypeCode: string) => void;
}

export const DocumentTemplateManagementView: React.FunctionComponent<
  React.PropsWithChildren<IDocumentTemplateManagementViewProp>
> = props => {
  const onSelectChange = (selectedType: React.ChangeEvent<HTMLInputElement>) => {
    const formDocumentTypeCode = selectedType.target.value;
    props.setSelectedFormDocumentTypeCode(formDocumentTypeCode);
  };

  return (
    <ListPage>
      <Scrollable>
        <LoadingBackdrop show={props.isLoading} />
        <CommonStyled.H1>
          <AdminIcon title="Admin Tools icon" width="2.6rem" height="2.6rem" fill="currentColor" />
          <span className="ml-2">PIMS Document Template Management</span>
        </CommonStyled.H1>
        <Section>
          <Row>
            <Col xs="auto">Form Type:</Col>
            <Col xs="auto">
              <Form.Group aria-label="Select activity type">
                <Form.Control as="select" onChange={onSelectChange} className="form-select">
                  <option>Select a form type</option>
                  {props.formDocumentTypes?.map(types => {
                    return (
                      <option
                        value={types.formTypeCode ?? ''}
                        key={'form-type-' + types.formTypeCode}
                      >
                        {types.description}
                      </option>
                    );
                  })}
                </Form.Control>
              </Form.Group>
            </Col>
          </Row>
        </Section>
        {props.selectedFormDocumentTypeCode !== undefined && (
          <DocumentListContainer
            parentId={props.selectedFormDocumentTypeCode}
            addButtonText="Add a Form Document Template"
            relationshipType={ApiGen_CodeTypes_DocumentRelationType.Templates}
          />
        )}
      </Scrollable>
    </ListPage>
  );
};

export default DocumentTemplateManagementView;

export const ListPage = styled.div`
  display: flex;
  flex-direction: column;
  flex-grow: 1;
  width: 100%;
  gap: 2.5rem;
  padding: 0;
`;

export const Scrollable = styled(ScrollableBase)`
  padding: 1.6rem 3.2rem;
  width: 100%;
`;
