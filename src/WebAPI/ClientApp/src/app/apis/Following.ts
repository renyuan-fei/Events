import apiClient from "@apis/BaseApi.ts";
import {ApiResponse} from "@type/ApiResponse.ts";
import {handleResponse} from "@apis/ApiHandler.ts";
import {IsFollowing} from "@type/IsFollowing.ts";
import {useQuery} from "react-query";

async function IsFollowingUser(userId :string): Promise<IsFollowing> {
    const response = await apiClient.get<ApiResponse<IsFollowing>>('/api/Follow/IsFollowing/'+ userId);
    return handleResponse(response);
}

// async function FollowUser(userId :string): Promise<IsFollowing> {
//     const response = await apiClient.post<ApiResponse<IsFollowing>>('/api/Follow/'+ userId);
//     return handleResponse(response);
// }
//
// async function UnFollowUser(userId :string): Promise<IsFollowing> {
//     const response = await apiClient.delete<ApiResponse<IsFollowing>>('/api/Follow/'+ userId);
//     return handleResponse(response);
// }

export const useIsFollowingUser = (userId: string) => {
    return useQuery(["IsFollowingUser", userId],
        ()=> IsFollowingUser(userId), {
        enabled: false,
        onSuccess(data: IsFollowing) {
            console.log(data);
        },
        onError(error: any) {
            console.log(error);
        }
    })
}
