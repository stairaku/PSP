import React from 'react';
import { FaEye, FaInfoCircle, FaTrash } from 'react-icons/fa';
import { CellProps } from 'react-table';
import styled from 'styled-components';

import { LinkButton } from '@/components/common/buttons';
import { StyledRemoveIconButton } from '@/components/common/buttons/RemoveButton';
import TooltipWrapper from '@/components/common/TooltipWrapper';
import { ColumnWithProps, Table } from '@/components/Table';
import { TableSort } from '@/components/Table/TableSort';
import Claims from '@/constants/claims';
import { PropertyManagementActivityStatusTypes } from '@/constants/propertyMgmtActivityStatusTypes';
import useKeycloakWrapper from '@/hooks/useKeycloakWrapper';
import { ApiGen_Concepts_PropertyActivity } from '@/models/api/generated/ApiGen_Concepts_PropertyActivity';
import { stringToFragment } from '@/utils/columnUtils';
import { prettyFormatDate } from '@/utils/dateUtils';

import { PropertyActivityRow } from './models/PropertyActivityRow';

export interface IManagementActivitiesListProps {
  loading: boolean;
  propertyActivities: PropertyActivityRow[];
  handleView: (activityId: number) => void;
  handleDelete: (activityId: number) => void;
  sort: TableSort<ApiGen_Concepts_PropertyActivity>;
  setSort: (value: TableSort<ApiGen_Concepts_PropertyActivity>) => void;
}

export function createTableColumns(
  onView: (activityId: number) => void,
  onDelete: (activityId: number) => void,
) {
  const columns: ColumnWithProps<PropertyActivityRow>[] = [
    {
      Header: 'Activity type',
      accessor: 'activityType',
      align: 'left',
      sortable: true,
      minWidth: 45,
      maxWidth: 45,
      Cell: (cellProps: CellProps<PropertyActivityRow>) => {
        return stringToFragment(cellProps.row.original?.activityType?.description || '');
      },
    },
    {
      Header: 'Activity sub-type',
      accessor: 'activitySubType',
      align: 'left',
      sortable: true,
      minWidth: 45,
      maxWidth: 45,
      Cell: (cellProps: CellProps<PropertyActivityRow>) => {
        return stringToFragment(cellProps.row.original?.activitySubType?.description || '');
      },
    },
    {
      Header: 'Activity status',
      accessor: 'activityStatusType',
      align: 'left',
      sortable: true,
      minWidth: 35,
      maxWidth: 35,
      Cell: (cellProps: CellProps<PropertyActivityRow>) => {
        return stringToFragment(cellProps.row.original?.activityStatusType?.description || '');
      },
    },
    {
      Header: 'Request added date',
      accessor: 'requestedAddedDate',
      align: 'left',
      sortable: true,
      minWidth: 40,
      maxWidth: 40,
      Cell: (cellProps: CellProps<PropertyActivityRow>) => {
        return stringToFragment(prettyFormatDate(cellProps.row.original?.requestedAddedDate));
      },
    },
    {
      Header: 'Actions',
      align: 'center',
      sortable: false,
      width: 20,
      maxWidth: 20,
      Cell: (cellProps: CellProps<PropertyActivityRow>) => {
        const { hasClaim } = useKeycloakWrapper();
        const activityRow = cellProps.row.original;
        return (
          <StyledDiv>
            {hasClaim(Claims.MANAGEMENT_VIEW) && (
              <LinkButton
                data-testid={`activity-view-${activityRow.id}`}
                icon={
                  <FaEye
                    size="2rem"
                    id={`activity-view-${cellProps.row.id}`}
                    title="property-activity view details"
                  />
                }
                onClick={() => activityRow?.id && onView(activityRow.activityId)}
              />
            )}
            {hasClaim(Claims.MANAGEMENT_DELETE) &&
            activityRow?.activityStatusType?.id ===
              PropertyManagementActivityStatusTypes.NOTSTARTED ? (
              <StyledRemoveIconButton
                id={`activity-delete-${cellProps.row.id}`}
                data-testid={`activity-delete-${activityRow.id}`}
                onClick={() => activityRow.id && onDelete(activityRow.id)}
                title="Delete"
              >
                <FaTrash size="2rem" />
              </StyledRemoveIconButton>
            ) : (
              <TooltipWrapper
                tooltipId={`activity-delete-tooltip-${activityRow.id}`}
                tooltip="Only activity that is not started can be deleted."
              >
                <FaInfoCircle className="tooltip-icon h-24" size="2rem" />
              </TooltipWrapper>
            )}
          </StyledDiv>
        );
      },
    },
  ];

  return columns;
}

const ManagementActivitiesList: React.FunctionComponent<IManagementActivitiesListProps> = ({
  loading,
  propertyActivities,
  handleView,
  handleDelete,
  sort,
  setSort,
  ...rest
}) => {
  return (
    <Table<PropertyActivityRow>
      loading={loading}
      name="PropertyManagementActivitiesTable"
      manualSortBy={true}
      totalItems={propertyActivities.length}
      columns={createTableColumns(handleView, handleDelete)}
      externalSort={{ sort, setSort }}
      data={propertyActivities ?? []}
      noRowsMessage="No property management activities found"
      pageSize={10}
      manualPagination={false}
      {...rest}
    />
  );
};

export default ManagementActivitiesList;

const StyledDiv = styled.div`
  display: flex;
  flex-direction: row;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 0.25rem;
  width: 80%;
`;
