import {GetPaginatedAttendee} from "@apis/Activities.ts";
import {useRef} from "react";
import {useInfiniteQuery} from "react-query";

const useGetPaginatedAttendeeQuery = (activityId: string, pageSize: number) => {
    // 初始时间戳可以在这里设置，比如使用当前时间
    const initialTimestamp = useRef(new Date().toISOString());

    const {data, fetchNextPage, hasNextPage, isFetchingNextPage } = useInfiniteQuery(
        ['paginatedAttendees', activityId],
        async ({ pageParam = 1 }) => {
            return GetPaginatedAttendee(activityId, pageParam, pageSize, initialTimestamp.current);
        },
        {
            getNextPageParam: (lastPage) => {
                return lastPage.pageNumber < lastPage.totalPages ? lastPage.pageNumber + 1 : undefined;
            },
        }
    );

    const attendees = data?.pages.flatMap(page => page.items) ?? [];

    return {attendees, fetchNextPage, hasNextPage, isFetchingNextPage }
}

export default useGetPaginatedAttendeeQuery;