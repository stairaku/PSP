import { SideBarType } from '@/components/common/mapFSM/machineDefinition/types';
import { IMapStateMachineContext } from '@/components/common/mapFSM/MapStateMachineContext';
import {
  emptyFullyFeaturedFeatureCollection,
  emptyPimsFeatureCollection,
} from '@/components/common/mapFSM/models';
import { defaultBounds } from '@/components/maps/constants';

export const mapMachineBaseMock: IMapStateMachineContext = {
  requestFlyToBounds: jest.fn(),
  mapFeatureData: {
    pimsFeatures: emptyPimsFeatureCollection,
    fullyAttributedFeatures: emptyFullyFeaturedFeatureCollection,
  },

  isSidebarOpen: false,
  sideBarType: SideBarType.NOT_DEFINED,
  isAdvancedFilterSidebarOpen: false,
  pendingFlyTo: false,
  pendingFitBounds: false,
  requestedFlyTo: {
    location: null,
    bounds: null,
  },
  requestedFitBounds: defaultBounds,
  mapFeatureSelected: null,
  mapLocationSelected: null,
  mapLocationFeatureDataset: null,
  selectedFeatureDataset: null,
  showPopup: false,
  isLoading: false,
  mapFilter: null,

  filePropertyLocations: [],
  isSelecting: false,
  requestFlyToLocation: jest.fn(),

  processFlyTo: jest.fn(),
  processFitBounds: jest.fn(),
  openSidebar: jest.fn(),
  closeSidebar: jest.fn(),
  closePopup: jest.fn(),
  mapClick: jest.fn(),
  mapMarkerClick: jest.fn(),
  setMapFilter: jest.fn(),
  refreshMapProperties: jest.fn(),
  prepareForCreation: jest.fn(),
  startSelection: jest.fn(),
  finishSelection: jest.fn(),
  setFilePropertyLocations: jest.fn(),
  openAdvancedFilterSidebar: jest.fn(),
  closeAdvancedFilterSidebar: jest.fn(),
};