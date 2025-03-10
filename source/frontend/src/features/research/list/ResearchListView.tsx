import React, { useEffect } from 'react';
import { useCallback } from 'react';
import { Col, Row } from 'react-bootstrap';
import { FaPlus } from 'react-icons/fa';
import { MdTopic } from 'react-icons/md';
import { useHistory } from 'react-router';
import { toast } from 'react-toastify';

import * as CommonStyled from '@/components/common/styles';
import { StyledAddButton } from '@/components/common/styles';
import Claims from '@/constants/claims';
import { useApiResearchFile } from '@/hooks/pims-api/useApiResearchFile';
import useKeycloakWrapper from '@/hooks/useKeycloakWrapper';
import { useSearch } from '@/hooks/useSearch';
import {
  IResearchSearchResult,
  ResearchSearchResultModel,
} from '@/interfaces/IResearchSearchResult';

import { IResearchFilter } from '../interfaces';
import ResearchFilter, { defaultResearchFilter } from './ResearchFilter/ResearchFilter';
import { ResearchSearchResults } from './ResearchSearchResults/ResearchSearchResults';
import * as Styled from './styles';

/**
 * Page that displays leases information.
 */
export const ResearchListView: React.FunctionComponent<React.PropsWithChildren<unknown>> = () => {
  const history = useHistory();
  const { getResearchFiles } = useApiResearchFile();
  const { hasClaim } = useKeycloakWrapper();
  const {
    results,
    filter,
    sort,
    error,
    totalItems,
    currentPage,
    totalPages,
    pageSize,
    setFilter,
    setSort,
    setCurrentPage,
    setPageSize,
    loading,
  } = useSearch<IResearchSearchResult, IResearchFilter>(
    defaultResearchFilter,
    getResearchFiles,
    'No matching results can be found. Try widening your search criteria.',
  );

  // update internal state whenever the filter bar changes
  const changeFilter = useCallback(
    (filter: IResearchFilter) => {
      setFilter(filter);
    },
    [setFilter],
  );

  useEffect(() => {
    if (error) {
      toast.error(error?.message);
    }
  }, [error]);

  return (
    <Styled.ListPage>
      <Styled.Scrollable>
        <CommonStyled.H1>
          <MdTopic title="Research file icon" />
          <span className="ml-2">Research Files</span>
        </CommonStyled.H1>
        <Styled.PageToolbar>
          <Row>
            <Col>
              <ResearchFilter filter={filter} setFilter={changeFilter} />
            </Col>
          </Row>
        </Styled.PageToolbar>
        {hasClaim(Claims.RESEARCH_ADD) && (
          <StyledAddButton onClick={() => history.push('/mapview/sidebar/research/new')}>
            <FaPlus />
            &nbsp;Create a Research File
          </StyledAddButton>
        )}
        <ResearchSearchResults
          results={results.map(r => ResearchSearchResultModel.fromApi(r))}
          totalItems={totalItems}
          pageIndex={currentPage}
          pageSize={pageSize}
          pageCount={totalPages}
          sort={sort}
          setSort={setSort}
          setPageSize={setPageSize}
          setPageIndex={setCurrentPage}
          loading={loading}
        />
      </Styled.Scrollable>
    </Styled.ListPage>
  );
};

export default ResearchListView;
