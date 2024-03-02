import {useQuery} from "react-query";
import {GetActivity} from "@apis/Activities.ts";

export const useGetActivityQuery = (id: string | undefined) => {
    const {isLoading, isError,data} = useQuery(
        ['activity', id],
        () => GetActivity(id!),
        {
            // 只有当 id 存在（非空或非undefined）时，才启用查询
            enabled: !!id,
        });

    return {isGettingActivity:isLoading, isError,activityDetail:data};
}