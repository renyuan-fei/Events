import apiClient from "@apis/BaseApi.ts";
import {ApiResponse} from "@type/ApiResponse.ts";
import {handleResponse} from "@apis/ApiHandler.ts";
import {UserDetailBase} from "@type/UserDetailBase.ts";
import {PaginatedResponse} from "@type/PaginatedResponse.ts";

export const isFollowing = async (targetUserId: string): Promise<boolean> => {
    const response = await apiClient.get<ApiResponse<boolean>>('/api/Follow/IsFollowing', {
        params: {targetUserId}
    });
    return handleResponse(response);
};


export const follow = async (targetUserId: string) => {
    const response = await apiClient.post<ApiResponse<any>>('/api/Follow', null, {
        params: {targetUserId}
    });
    return handleResponse(response);
}


export const unfollow = async (targetUserId: string) => {
    const response = await apiClient.delete<ApiResponse<any>>('/api/Follow', {
        params: {targetUserId}
    });
    return handleResponse(response);
}

export const getPaginatedFollowers = async (targetUserId: string, pageSize: number, page:number): Promise<PaginatedResponse<UserDetailBase>> => {
    const response = await apiClient.get<ApiResponse<PaginatedResponse<UserDetailBase>>>('/api/Follow/Follower', {
        params: {
            targetUserId,
            pageSize,
            page
        }});
    return handleResponse(response);
}

export const getPaginatedFollowing = async (targetUserId: string, pageSize: number, page:number): Promise<PaginatedResponse<UserDetailBase>> => {
    const response = await apiClient.get<ApiResponse<PaginatedResponse<UserDetailBase>>>('/api/Follow/Following', {
        params: {
            targetUserId,
            pageSize,
            page
        }});
    return handleResponse(response);
}
