import {useInfiniteQuery} from "react-query";
import {useRef} from "react";
import {getPaginatedPhotos} from "@apis/Photos.ts";

const useGetPaginatedPhotosQuery = (id: string, pageSize: number) => {
    // TODO fix after upload，new photo not display，because initialTimestamp,(return
    //  new photo after add in backend)
    const initialTimestamp = useRef(new Date().toISOString());

    const {data, fetchNextPage, hasNextPage, isFetchingNextPage }= useInfiniteQuery(
        ['paginatedPhotos', id],
        async ({ pageParam = 1 }) => {
            return getPaginatedPhotos(id, pageParam, pageSize, initialTimestamp.current);
        },
        {
            getNextPageParam: (lastPage) => {
                return lastPage.pageNumber < lastPage.totalPages ? lastPage.pageNumber + 1 : undefined;
            },
        }
    );

    const photos = data?.pages.flatMap(page => page.items) ?? [];

    return {photos,fetchNextPage, hasNextPage, isFetchingNextPage};
}

export default useGetPaginatedPhotosQuery;