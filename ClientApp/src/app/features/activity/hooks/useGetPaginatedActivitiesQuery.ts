import { useQuery } from 'react-query';
import {getPaginatedActivities} from "@apis/Activities.ts";

const useGetPaginatedActivitiesQuery =(pageNumber:number, pageSize:number,filters?: Record<string, string>) => {

    const { isLoading, data } = useQuery(
        ['paginatedActivities', pageNumber, pageSize, filters],
        () => getPaginatedActivities(pageNumber, pageSize,filters!),
        {
            keepPreviousData: true,
        }
    );

    return { activities: data?.items, isActivitiesLoading: isLoading,pageCount:data?.totalPages || 0};
};

export default useGetPaginatedActivitiesQuery;
