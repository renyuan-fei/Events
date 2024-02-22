import apiClient from "@apis/BaseApi.ts";
import {ApiResponse} from "@type/ApiResponse.ts";
import {handleResponse} from "@apis/ApiHandler.ts";

export const isFollowing = async (targetUserId: string): Promise<boolean> => {
    const response = await apiClient.get<ApiResponse<boolean>>('/api/Follow/IsFollowing', {
        params: { targetUserId }
    });
    return handleResponse(response);
};


export const follow = async (targetUserId: string) => {
    const response = await apiClient.post<ApiResponse<any>>('/api/Follow', null, {
        params: { targetUserId }
    });
    return handleResponse(response);
}


export const unfollow = async (targetUserId: string) => {
    const response = await apiClient.delete<ApiResponse<any>>('/api/Follow', {
        params: { targetUserId }
    });
    return handleResponse(response);
}

// export const getFollowers = async (targetUserId: string): Promise<ApiResponse<string[]>> => {
//
// }

// async function FollowUser(userId :string): Promise<IsFollowing> {
//     const response = await apiClient.post<ApiResponse<IsFollowing>>('/api/Follow/'+ userId);
//     return handleResponse(response);
// }
//
// async function UnFollowUser(userId :string): Promise<IsFollowing> {
//     const response = await apiClient.delete<ApiResponse<IsFollowing>>('/api/Follow/'+ userId);
//     return handleResponse(response);
// }
