import apiClient from "@apis/BaseApi.ts";
import {ApiResponse} from "@type/ApiResponse.ts";
import {handleResponse} from "@apis/ApiHandler.ts";
import {UserProfile} from "@type/UserProfile.ts";

export const GetUserProfile = async (id: string): Promise<UserProfile> => {
    const response = await apiClient.get<ApiResponse<UserProfile>>(`/api/Users/${id}`);
    return handleResponse(response);
}

export const UpdateUserProfile = async (profile: UserProfile): Promise<any> => {
    const response = await apiClient.put<ApiResponse<any>>(`/api/Users`, profile);
    return handleResponse(response);
}