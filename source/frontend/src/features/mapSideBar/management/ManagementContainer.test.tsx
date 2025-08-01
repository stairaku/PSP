import axios from 'axios';
import MockAdapter from 'axios-mock-adapter';
import { Formik } from 'formik';
import { createMemoryHistory } from 'history';
import noop from 'lodash/noop';

import { mockLastUpdatedBy } from '@/mocks/lastUpdatedBy.mock';
import { mockLookups } from '@/mocks/lookups.mock';
import {
  mockManagementFilePropertiesResponse,
  mockManagementFileResponse,
} from '@/mocks/managementFiles.mock';
import { lookupCodesSlice } from '@/store/slices/lookupCodes';
import {
  act,
  render,
  RenderOptions,
  screen,
  userEvent,
  waitFor,
  waitForElementToBeRemoved,
} from '@/utils/test-utils';

import {
  getMockApiAssociation,
  getMockPropertyAssociations,
} from '@/mocks/propertyAssociations.mock';
import { SideBarContextProvider } from '../context/sidebarContext';
import { PropertyForm } from '../shared/models';
import ManagementContainer, { IManagementContainerProps } from './ManagementContainer';
import { IManagementViewProps } from './ManagementView';

const history = createMemoryHistory();
const mockAxios = new MockAdapter(axios);
const mockManagementFileApi = mockManagementFileResponse();

// mock auth library

const onClose = vi.fn();

// Need to mock this library for unit tests
vi.mock('react-visibility-sensor', () => {
  return {
    default: vi.fn().mockImplementation(({ children }) => {
      if (children instanceof Function) {
        return children({ isVisible: true });
      }
      return children;
    }),
  };
});

let viewProps!: IManagementViewProps;
const ManagementContainerView = (props: IManagementViewProps) => {
  viewProps = props;
  return (
    <Formik innerRef={props.formikRef} onSubmit={noop} initialValues={{ value: 0 }}>
      {({ values }) => <>{values.value}</>}
    </Formik>
  );
};
const DEFAULT_PROPS: IManagementContainerProps = {
  managementFileId: 1,
  onClose,
  View: ManagementContainerView,
};

describe('ManagementContainer component', () => {
  // render component under test
  const setup = (
    props: IManagementContainerProps = { ...DEFAULT_PROPS },
    renderOptions: RenderOptions = {},
  ) => {
    const utils = render(
      <SideBarContextProvider>
        <ManagementContainer {...props} View={ManagementContainerView} />
      </SideBarContextProvider>,
      {
        store: {
          [lookupCodesSlice.name]: { lookupCodes: mockLookups },
        },
        useMockAuthentication: true,
        claims: renderOptions?.claims ?? [],
        history,
        ...renderOptions,
      },
    );

    return {
      ...utils,
      getCloseButton: () => utils.getByTitle('close'),
    };
  };

  beforeEach(() => {
    history.replace('/mapview/sidebar/management/1');

    mockAxios.onGet(new RegExp('users/info/*')).reply(200, {});
    mockAxios
      .onGet(new RegExp('managementfiles/1/properties'))
      .reply(200, mockManagementFilePropertiesResponse());
    mockAxios
      .onPut(new RegExp('managementfiles/1/properties'))
      .reply(200, mockManagementFilePropertiesResponse());
    mockAxios.onGet(new RegExp('managementfiles/1/updateInfo')).reply(200, mockLastUpdatedBy(1));
    mockAxios.onGet(new RegExp('managementfiles/1')).reply(200, mockManagementFileApi);
    mockAxios.onGet(new RegExp('properties/1/associations')).reply(200, {
      ...getMockPropertyAssociations(),
      managementAssociations: [getMockApiAssociation(1), getMockApiAssociation(2)],
    });
  });

  afterEach(() => {
    mockAxios.resetHistory();
    vi.clearAllMocks();
  });

  it('renders as expected', async () => {
    const { asFragment, getByTestId } = setup();

    const spinner = getByTestId('filter-backdrop-loading');
    await waitForElementToBeRemoved(spinner);

    expect(asFragment()).toMatchSnapshot();
  });

  it('renders a spinner while loading', async () => {
    const { getByTestId } = setup();

    const spinner = getByTestId('filter-backdrop-loading');
    expect(spinner).toBeVisible();

    await waitForElementToBeRemoved(spinner);
  });

  it('should not display the spinner when properties are loading and the property selector is being displayed', async () => {
    const { getByTestId } = setup(undefined, { claims: [] });

    const spinner = getByTestId('filter-backdrop-loading');
    await waitForElementToBeRemoved(spinner);

    mockAxios.onGet(new RegExp('managementfiles/1/properties')).timeout();
    await act(async () => viewProps.onEditProperties());
    await act(async () => {
      viewProps.canRemove(1);
    });
    expect(spinner).not.toBeVisible();
  });

  it('should call the onUpdateProperty when related function is called', async () => {
    const { getByTestId } = setup(undefined, { claims: [] });

    const spinner = getByTestId('filter-backdrop-loading');
    await waitForElementToBeRemoved(spinner);

    await act(async () => {
      await viewProps.onUpdateProperties(mockManagementFileApi);
    });
    expect(spinner).not.toBeVisible();
    expect(
      mockAxios.history.put.filter(x => x.url === '/managementfiles/1/properties?'),
    ).toHaveLength(1);
  });

  it('should retrieve property associations when confirmBeforeAdd function is called', async () => {
    const { getByTestId } = setup(undefined, { claims: [] });

    const spinner = getByTestId('filter-backdrop-loading');
    await waitForElementToBeRemoved(spinner);
    expect(spinner).not.toBeVisible();

    await act(async () => {
      await viewProps.confirmBeforeAdd(new PropertyForm({ apiId: 1 }));
    });
    expect(mockAxios.history.get.filter(x => x.url === '/properties/1/associations')).toHaveLength(
      1,
    );
  });

  it('should show error popup when user adds a property outside of the user account regions', async () => {
    const { getByTestId } = setup(undefined, { claims: [] });

    const spinner = getByTestId('filter-backdrop-loading');
    await waitForElementToBeRemoved(spinner);

    const errorMessage = 'You cannot add a property that is outside of your user account region';
    mockAxios.onPut(new RegExp('managementfiles/1/properties')).reply(400, { error: errorMessage });
    await act(async () => {
      await viewProps.onUpdateProperties(mockManagementFileApi);
    });
    expect(spinner).not.toBeVisible();
    expect(await screen.findByText(errorMessage)).toBeVisible();
  });

  it('should change menu index when not editing', async () => {
    const { getByTestId } = setup(undefined, { claims: [] });

    const spinner = getByTestId('filter-backdrop-loading');
    await waitForElementToBeRemoved(spinner);

    await act(async () => viewProps.onSelectProperty(1));
    expect(history.location.pathname).toBe('/mapview/sidebar/management/1/property/1');
  });

  it('should go back to file summary when not editing', async () => {
    const { getByTestId } = setup(undefined, { claims: [] });

    const spinner = getByTestId('filter-backdrop-loading');
    await waitForElementToBeRemoved(spinner);

    await act(async () => viewProps.onSelectProperty(1));
    expect(history.location.pathname).toBe('/mapview/sidebar/management/1/property/1');

    await act(async () => viewProps.onSelectFileSummary());
    expect(history.location.pathname).toBe('/mapview/sidebar/management/1');
  });

  it('displays a warning if form is dirty and menu index changes', async () => {
    const { getByTestId } = setup(undefined, { claims: [] });

    const spinner = getByTestId('filter-backdrop-loading');
    await waitForElementToBeRemoved(spinner);

    await act(async () => viewProps.setIsEditing(true));
    await act(async () => (viewProps.formikRef.current as any).setFieldValue('value', 1));
    expect(await screen.findByText('1')).toBeVisible();
    await act(async () => viewProps.onSelectProperty(1));

    const warning = await screen.findByText(/Confirm Changes/i);
    expect(warning).toBeVisible();
  });

  it('cancels edit if user confirms modal', async () => {
    const { getByTestId } = setup(undefined, { claims: [] });

    const spinner = getByTestId('filter-backdrop-loading');
    await waitForElementToBeRemoved(spinner);

    await act(async () => viewProps.setIsEditing(true));
    await act(async () => (viewProps.formikRef.current as any).setFieldValue('value', 1));
    expect(await screen.findByText('1')).toBeVisible();
    await act(async () => viewProps.onSelectProperty(1));

    expect(history.location.pathname).toBe('/mapview/sidebar/management/1');

    const yesButton = await screen.findByTitle('ok-modal');
    await act(async () => {
      userEvent.click(yesButton);
    });

    const params = new URLSearchParams(history.location.search);
    expect(history.location.pathname).toBe('/mapview/sidebar/management/1/property/1');
    expect(params.has('edit')).toBe(false);
  });

  it('cancels edit if form is not dirty and menu index changes', async () => {
    const { getByTestId } = setup(undefined, { claims: [] });

    const spinner = getByTestId('filter-backdrop-loading');
    await waitForElementToBeRemoved(spinner);

    await act(async () => viewProps.setIsEditing(true));
    await act(async () => viewProps.onSelectProperty(1));

    const params = new URLSearchParams(history.location.search);
    expect(params.has('edit')).toBe(false);
  });

  it('displays a warning if form is dirty and user clicks to go back to file summary', async () => {
    const { getByTestId } = setup(undefined, { claims: [] });

    const spinner = getByTestId('filter-backdrop-loading');
    await waitForElementToBeRemoved(spinner);

    // go to property, then edit it
    await act(async () => viewProps.onSelectProperty(1));
    await act(async () => viewProps.setIsEditing(true));
    await act(async () => (viewProps.formikRef.current as any).setFieldValue('value', 1));
    expect(await screen.findByText('1')).toBeVisible();

    // go back to file summary
    await act(async () => viewProps.onSelectFileSummary());

    const warning = await screen.findByText(/Confirm Changes/i);
    expect(warning).toBeVisible();
  });

  it('on success function refetches management file', async () => {
    const { getByTestId } = setup(undefined, { claims: [] });

    const spinner = getByTestId('filter-backdrop-loading');
    await waitForElementToBeRemoved(spinner);

    await act(async () => viewProps.onSuccess(true, true));

    expect(mockAxios.history.get.filter(x => x.url === '/managementfiles/1')).toHaveLength(2);
  });

  it('on success function cancels edit', async () => {
    const { getByTestId } = setup(undefined, { claims: [] });

    const spinner = getByTestId('filter-backdrop-loading');
    await waitForElementToBeRemoved(spinner);

    await act(async () => viewProps.setIsEditing(true));
    await act(async () => viewProps.onSuccess());

    const params = new URLSearchParams(history.location.search);
    expect(params.has('edit')).toBe(false);
  });

  it('on save function submits the form', async () => {
    const { getByTestId } = setup(undefined, { claims: [] });

    const spinner = getByTestId('filter-backdrop-loading');
    await waitForElementToBeRemoved(spinner);

    await act(async () => viewProps.onSave());

    await waitFor(async () => viewProps.formikRef.current?.submitCount === 1);
  });

  it('triggers the popup for business rule violation', async () => {
    setup(undefined, { claims: [] });

    const spinner = screen.getByTestId('filter-backdrop-loading');
    await waitForElementToBeRemoved(spinner);

    // simulate 400 error coming back from API
    mockAxios.onPut(new RegExp('managementfiles/1/properties')).reply(400, {
      error: 'This property cannot be deleted because it is part of a subdivision or consolidation',
      type: 'BusinessRuleViolationException',
    });

    await act(async () => {
      await viewProps.onUpdateProperties(mockManagementFileResponse());
    });

    expect(
      await screen.findByText(
        /This property cannot be deleted because it is part of a subdivision or consolidation/i,
      ),
    ).toBeVisible();

    const button = await screen.findByTitle('ok-modal');
    expect(button).toBeVisible();
    await act(async () => userEvent.click(button));
  });
});
