import { cleanup, render } from '@testing-library/react';
import { createMemoryHistory } from 'history';
import { LatLng } from 'leaflet';
import { useMap } from 'react-leaflet';

import { createRouteProvider } from '@/utils/test-utils';

import { IPopupContentProps, LayerPopupContent } from './LayerPopupContent';

const history = createMemoryHistory();

vi.mock('react-leaflet');

// Mock react-leaflet dependencies
const map: Partial<L.Map> = {
  getZoom: vi.fn(),
  getBoundsZoom: vi.fn(),
  flyToBounds: vi.fn(),
};

vi.mocked(useMap).mockReturnValue(map as unknown as ReturnType<typeof useMap>);

const mockLayer: IPopupContentProps = {
  config: {},
  data: {
    feature_area_sqm: '10000',
    feature_length_m: '500',
    municipality: 'Rural',
    objectid: '0',
    owner_type: 'Private',
    parcel_class: 'Subdivision',
    parcel_name: '000000000',
    parcel_start_date: '2020-01-01',
    parcel_status: 'Active',
    PID: '000000001',
    pid_number: '000000000',
    PIN: '1',
    plan_number: 'VIP00000',
    regional_district: 'Fake District',
    se_anno_cad_data: '',
    when_updated: '2020-01-01',
  },
};

const renderPopup = (props: IPopupContentProps) => {
  const wrapper = createRouteProvider(history);
  return render(<LayerPopupContent {...props} />, { wrapper });
};

describe('Layer Popup Content', () => {
  afterEach(cleanup);

  it('Renders correctly', () => {
    const { asFragment } = renderPopup(mockLayer);
    expect(asFragment()).toMatchSnapshot();
  });
});
