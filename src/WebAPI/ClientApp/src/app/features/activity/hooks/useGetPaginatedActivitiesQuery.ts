import { useQuery } from 'react-query';
import {GetPaginatedActivities} from "@apis/Activities.ts";

const useGetPaginatedActivitiesQuery =(pageNumber:number, pageSize:number,filters?: Record<string, string>) => {

    const { isLoading, data } = useQuery(
        ['paginatedActivities', pageNumber, pageSize, filters],
        () => GetPaginatedActivities(pageNumber, pageSize,filters!),
        {
            keepPreviousData: true,
        }
    );

    return { activities: data?.items, isActivitiesLoading: isLoading,pageCount:data?.totalPages || 0};
};

export default useGetPaginatedActivitiesQuery;
