import {AxiosError} from "axios";
import {ApiResponse} from "@type/ApiResponse.ts";
import {useQuery} from "react-query";
import {isFollowing} from "@apis/Following.ts";

const useIsFollowingQuery = (targetUserId: string, currentUserId: string) => {
    // if the current user is the same as the target user, we don't need to query the following status
    const isEnabled = currentUserId !== targetUserId;

    const {isLoading, data} = useQuery<boolean, AxiosError<ApiResponse<any>>>(
        ["IsFollowing",targetUserId],
        () => isFollowing(targetUserId),
        {
            onError(error: AxiosError<ApiResponse<any>>) {
                console.log(error);
            },
            enabled: isEnabled
        }
    )

    return {isLoading, isFollowing: data};
}

export default useIsFollowingQuery;