import {useQuery} from "react-query";
import {GetActivity} from "@apis/Activities.ts";

export const useGetActivityQuery = (id: string) => {
    const {isLoading, isError,data} = useQuery(
        ['activity', id],
        () => GetActivity(id));

    return {isLoading, isError,data};
}