import { useQuery } from 'react-query';
import {GetPaginatedActivities} from "@apis/Activities.ts";
import {useLocation} from "react-router-dom";

const useGetPaginatedActivitiesQuery =() => {
    const location = useLocation();

    const pageNumber = new URLSearchParams(location.search).get('page') || '1';
    const pageSize = new URLSearchParams(location.search).get('pageSize') || '8';

    const { isLoading, data } = useQuery(
        ['paginatedActivities', pageNumber, pageSize],
        () => GetPaginatedActivities(parseInt(pageNumber), parseInt(pageSize)),
        {
            keepPreviousData: true,
        }
    );

    return { activities: data?.items, isActivitiesLoading: isLoading,pageCount:data?.totalPages || 0};
};

export default useGetPaginatedActivitiesQuery;
