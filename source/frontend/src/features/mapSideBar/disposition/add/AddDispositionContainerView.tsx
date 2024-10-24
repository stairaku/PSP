import { FormikHelpers, FormikProps } from 'formik';
import { MdAirlineStops } from 'react-icons/md';

import LoadingBackdrop from '@/components/common/LoadingBackdrop';

import MapSideBarLayout from '../../layout/MapSideBarLayout';
import { PropertyForm } from '../../shared/models';
import SidebarFooter from '../../shared/SidebarFooter';
import { StyledFormWrapper } from '../../shared/styles';
import DispositionForm from '../form/DispositionForm';
import { DispositionFormModel } from '../models/DispositionFormModel';

export interface IAddDispositionContainerViewProps {
  formikRef: React.RefObject<FormikProps<DispositionFormModel>>;
  dispositionInitialValues: DispositionFormModel;
  loading: boolean;
  displayFormInvalid: boolean;
  onSubmit: (
    values: DispositionFormModel,
    formikHelpers: FormikHelpers<DispositionFormModel>,
  ) => void | Promise<any>;
  onCancel: () => void;
  onSave: () => void;
  confirmBeforeAdd: (propertyForm: PropertyForm) => Promise<boolean>;
}

const AddDispositionContainerView: React.FunctionComponent<IAddDispositionContainerViewProps> = ({
  formikRef,
  dispositionInitialValues,
  loading,
  displayFormInvalid,
  onSubmit,
  onSave,
  onCancel,
  confirmBeforeAdd,
}) => {
  return (
    <MapSideBarLayout
      showCloseButton
      title="Create Disposition File"
      icon={<MdAirlineStops title="Disposition file Icon" size={26} fill="currentColor" />}
      onClose={onCancel}
      footer={
        <SidebarFooter
          isOkDisabled={loading}
          onSave={onSave}
          onCancel={onCancel}
          displayRequiredFieldError={displayFormInvalid}
        />
      }
    >
      <StyledFormWrapper>
        <LoadingBackdrop show={loading} parentScreen={true} />
        <DispositionForm
          formikRef={formikRef}
          initialValues={dispositionInitialValues}
          onSubmit={onSubmit}
          confirmBeforeAdd={confirmBeforeAdd}
        ></DispositionForm>
      </StyledFormWrapper>
    </MapSideBarLayout>
  );
};

export default AddDispositionContainerView;
