import apiClient from "@apis/BaseApi.ts";
import {ApiResponse} from "@type/ApiResponse.ts";
import {handleResponse} from "@apis/ApiHandler.ts";

export const isFollowing = async (targetUserId: string): Promise<boolean> => {
    const response = await apiClient.get<ApiResponse<boolean>>(`/api/Follow/IsFollowing/${targetUserId}`);
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
