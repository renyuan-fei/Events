import { useInfiniteQuery } from 'react-query';
import { getPaginatedFollowers } from '@apis/Following.ts';
import {useRef} from "react";

const useGetPaginatedFollowersQuery = (id: string, pageSize: number) => {
    const initialTimestamp = useRef(new Date().toISOString());

    const {
        isFetchingNextPage,
        data,
        fetchNextPage,
        hasNextPage
    } = useInfiniteQuery(
        ['followers', id],
        async ({ pageParam = 1 }) => getPaginatedFollowers(id, pageSize, pageParam,initialTimestamp.current),
        {
            getNextPageParam: (lastPage) => {
                return lastPage.pageNumber < lastPage.totalPages ? lastPage.pageNumber + 1 : undefined;
            }
        }
    );

    const followers = data?.pages.flatMap(page => page.items) ?? [];

    return { followers, isFetchingNextPage, fetchNextPage, hasNextPage };
};

export default useGetPaginatedFollowersQuery;
