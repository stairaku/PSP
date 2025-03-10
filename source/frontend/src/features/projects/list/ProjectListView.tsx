import { useCallback } from 'react';
import { Col, Row } from 'react-bootstrap';
import { FaBriefcase, FaPlus } from 'react-icons/fa';
import { useHistory } from 'react-router-dom';

import * as CommonStyled from '@/components/common/styles';
import { StyledAddButton } from '@/components/common/styles';
import { Claims } from '@/constants/claims';
import { useApiProjects } from '@/hooks/pims-api/useApiProjects';
import useKeycloakWrapper from '@/hooks/useKeycloakWrapper';
import { useSearch } from '@/hooks/useSearch';
import { ApiGen_Concepts_Project } from '@/models/api/generated/ApiGen_Concepts_Project';

import { IProjectFilter } from '../interfaces';
import { defaultFilter, ProjectFilter } from './ProjectFilter/ProjectFilter';
import { ProjectSearchResultModel } from './ProjectSearchResults/models';
import { ProjectSearchResults } from './ProjectSearchResults/ProjectSearchResults';
import * as Styled from './styles';

/**
 * Page that displays Project files information.
 */
export const ProjectListView: React.FunctionComponent<React.PropsWithChildren<unknown>> = () => {
  const { searchProjects } = useApiProjects();
  const { hasClaim } = useKeycloakWrapper();
  const history = useHistory();

  const {
    results,
    filter,
    sort,
    totalItems,
    currentPage,
    totalPages,
    pageSize,
    setFilter,
    setSort,
    setCurrentPage,
    setPageSize,
    loading,
  } = useSearch<ApiGen_Concepts_Project, IProjectFilter>(
    defaultFilter,
    searchProjects,
    'No matching results can be found. Try widening your search criteria.',
  );

  // update internal state whenever the filter bar changes
  const changeFilter = useCallback(
    (filter: IProjectFilter) => {
      setFilter(filter);
    },
    [setFilter],
  );

  return (
    <Styled.ListPage>
      <Styled.Scrollable>
        <CommonStyled.H1>
          <FaBriefcase title="Project icon" fill="currentColor" className={'pb-1'} />
          <span className="ml-2">Projects</span>
        </CommonStyled.H1>
        <Styled.PageToolbar>
          <Row>
            <Col>
              <ProjectFilter
                filter={filter}
                setFilter={changeFilter}
                initialFilter={defaultFilter}
              />
            </Col>
          </Row>
        </Styled.PageToolbar>
        {hasClaim(Claims.PROJECT_ADD) && (
          <StyledAddButton onClick={() => history.push('/mapview/sidebar/project/new')}>
            <FaPlus />
            &nbsp;Create Project
          </StyledAddButton>
        )}
        <ProjectSearchResults
          results={results.map(x => ProjectSearchResultModel.fromApi(x))}
          totalItems={totalItems}
          pageIndex={currentPage}
          pageSize={pageSize}
          pageCount={totalPages}
          sort={sort}
          setSort={setSort}
          setPageSize={setPageSize}
          setPageIndex={setCurrentPage}
          loading={loading}
        ></ProjectSearchResults>
      </Styled.Scrollable>
    </Styled.ListPage>
  );
};

export default ProjectListView;
