import {useQuery} from "react-query";
import {GetActivities} from "@apis/Activities.ts";

export const useGetActivitiesQuery = (page: number = 1,pageSize: number = 8) => {
    const {isLoading, data} = useQuery(
        ['paginatedActivities', 1, 8],
        () => GetActivities({
        page: page,
        pageSize: pageSize
    }));

    return {isLoading, data};
}