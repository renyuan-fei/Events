import { useQuery } from 'react-query';
import { useDispatch } from 'react-redux';
import {GetPaginatedActivities} from "@apis/Activities.ts";
import {setPageCount} from "@features/commonSlice.ts";

const useGetPaginatedActivitiesQuery = (pageNumber: number = 1,pageSize: number = 10, searchItem: string[]) => {
    const dispatch = useDispatch();

    const { isLoading, data } = useQuery(
        ['paginatedActivities', pageNumber, pageSize, searchItem.sort().join(',')],
        () => GetPaginatedActivities( pageNumber,pageSize, searchItem),
        {
            keepPreviousData: true, // 保留上一页数据直到新数据被获取
            onSuccess: (data) => {
                dispatch(setPageCount(data.totalPages));
            },
        }
    );

    return { activities: data?.items, isActivitiesLoading: isLoading};
};

export default useGetPaginatedActivitiesQuery;
