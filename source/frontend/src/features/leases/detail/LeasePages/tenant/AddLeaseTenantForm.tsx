import { Formik, FormikProps } from 'formik';
import { FaPlus } from 'react-icons/fa';
import { Prompt } from 'react-router';
import { Link } from 'react-router-dom';
import styled from 'styled-components';

import { TableSelect } from '@/components/common/form';
import LoadingBackdrop from '@/components/common/LoadingBackdrop';
import { Section } from '@/components/common/Section/Section';
import { StyledSummarySection } from '@/components/common/Section/SectionStyles';
import { SectionListHeader } from '@/components/common/SectionListHeader';
import { ContactManagerModal } from '@/components/contact/ContactManagerModal';
import { Claims } from '@/constants';
import { TENANT_TYPES } from '@/constants/API';
import { LeaseFormModel } from '@/features/leases/models';
import useLookupCodeHelpers from '@/hooks/useLookupCodeHelpers';
import { IContactSearchResult } from '@/interfaces';
import { mapLookupCode } from '@/utils';

import { AddLeaseTenantYupSchema } from './AddLeaseTenantYupSchema';
import getColumns from './columns';
import { FormTenant } from './models';
import PrimaryContactWarningModal, {
  IPrimaryContactWarningModalProps,
} from './PrimaryContactWarningModal';
import SelectedTableHeader from './SelectedTableHeader';

export interface IAddLeaseTenantFormProps {
  selectedContacts: IContactSearchResult[];
  setSelectedContacts: (selectedContacts: IContactSearchResult[]) => void;
  setSelectedTenants: (selectedContacts: IContactSearchResult[]) => void;
  selectedTenants: FormTenant[];
  onSubmit: (lease: LeaseFormModel) => Promise<void>;
  initialValues?: LeaseFormModel;
  formikRef: React.Ref<FormikProps<LeaseFormModel>>;
  showContactManager: boolean;
  setShowContactManager: (showContactManager: boolean) => void;
  loading?: boolean;
}

export const AddLeaseTenantForm: React.FunctionComponent<
  React.PropsWithChildren<IAddLeaseTenantFormProps & IPrimaryContactWarningModalProps>
> = ({
  selectedContacts,
  setSelectedContacts,
  setSelectedTenants,
  selectedTenants,
  onSubmit,
  initialValues,
  formikRef,
  children,
  showContactManager,
  setShowContactManager,
  saveCallback,
  onCancel,
  loading,
}) => {
  const lookupCodes = useLookupCodeHelpers();
  const tenantTypes = lookupCodes.getByType(TENANT_TYPES).map(c => mapLookupCode(c));
  const onRemove = (remainingTenants: FormTenant[]) => {
    const remainingContacts = remainingTenants.map(t => FormTenant.toContactSearchResult(t));
    setSelectedTenants(remainingContacts);
    setSelectedContacts(remainingContacts);
  };

  return (
    <StyledSummarySection>
      <Section
        header={
          <SectionListHeader
            claims={[Claims.LEASE_EDIT]}
            title="Tenants"
            addButtonText="Select Tenant(s)"
            addButtonIcon={<FaPlus size={'2rem'} />}
            onAdd={() => {
              setShowContactManager(true);
            }}
          />
        }
      >
        <span>
          Note: If the tenants you are trying to find were never added to the &quot;contact
          list&quot; it will not show up. Please add them to the contact list{' '}
          {<Link to="/contact/list">here</Link>}, then you will be able to see them on the &quot;Add
          a Tenant&quot; list.
        </span>

        <Formik
          validationSchema={AddLeaseTenantYupSchema}
          onSubmit={values => {
            onSubmit(values);
          }}
          innerRef={formikRef}
          enableReinitialize
          initialValues={{ ...new LeaseFormModel(), ...initialValues, tenants: selectedTenants }}
        >
          {formikProps => (
            <>
              <LoadingBackdrop show={loading} parentScreen />
              <Prompt
                when={formikProps.dirty && !formikProps.isSubmitting}
                message="You have made changes on this form. Do you wish to leave without saving?"
              />
              <StyledFormBody>
                <TableSelect<FormTenant>
                  selectedItems={selectedTenants}
                  columns={getColumns(tenantTypes)}
                  field="tenants"
                  selectedTableHeader={SelectedTableHeader}
                  onRemove={onRemove}
                ></TableSelect>
                <ContactManagerModal
                  selectedRows={selectedContacts}
                  setSelectedRows={(s: IContactSearchResult[]) => {
                    setSelectedContacts(s);
                  }}
                  display={showContactManager}
                  setDisplay={setShowContactManager}
                  handleModalOk={() => {
                    setShowContactManager(false);
                    setSelectedTenants(selectedContacts);
                  }}
                  handleModalCancel={() => {
                    setShowContactManager(false);
                    setSelectedContacts(
                      selectedTenants.map(t => FormTenant.toContactSearchResult(t)),
                    );
                  }}
                  showActiveSelector={true}
                  isSummary={true}
                ></ContactManagerModal>
              </StyledFormBody>
              {children}
            </>
          )}
        </Formik>
        <PrimaryContactWarningModal
          saveCallback={saveCallback}
          onCancel={onCancel}
          selectedTenants={selectedTenants}
        />
      </Section>
    </StyledSummarySection>
  );
};

const StyledFormBody = styled.div`
  margin-top: 3rem;
  text-align: left;
`;

export default AddLeaseTenantForm;
