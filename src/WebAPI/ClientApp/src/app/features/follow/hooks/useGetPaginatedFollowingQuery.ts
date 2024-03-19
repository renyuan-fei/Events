import {useInfiniteQuery} from 'react-query';
import {getPaginatedFollowing} from '@apis/Following.ts';
import {useRef} from "react";

const useGetPaginatedFollowingQuery = (Id: string, pageSize: number) => {

    const initialTimestamp = useRef(new Date().toISOString());

    const {
        isFetchingNextPage,
        data,
        fetchNextPage,
        hasNextPage
    } = useInfiniteQuery(
        ['following', Id],
        ({pageParam = 1}) => getPaginatedFollowing(Id, pageSize, pageParam, initialTimestamp.current),
        {
            getNextPageParam: (lastPage) => {
                return lastPage.pageNumber < lastPage.totalPages ? lastPage.pageNumber + 1 : undefined;
            }
        }
    );

    const following = data?.pages.flatMap(page => page.items) ?? [];

    return {following, isFetchingNextPage, fetchNextPage, hasNextPage};
};

export default useGetPaginatedFollowingQuery;
