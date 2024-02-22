import apiClient from "./BaseApi";
import {ApiResponse} from "../types/ApiResponse";
import {TopPhotos} from "@type/TopPhotos.ts";
import {handleResponse} from "@apis/ApiHandler.ts";

export const getTopPhotos = async (id: string) => {
    const response = await apiClient.get<ApiResponse<TopPhotos>>(`/api/Photos/Top/${id}`);
    return handleResponse(response);
}

export const uploadUserPhoto = async (photo: FormData) => {
    const response = await apiClient.post<ApiResponse<string>>('/api/Photos', photo);
    return handleResponse(response);
}

export const uploadUserAvatar = async (photo: FormData) => {
    const response = await apiClient.put<ApiResponse<string>>('/api/Photos', photo);
    return handleResponse(response);
}

export const uploadActivityPhoto = async (photo: FormData, id: string) => {
    const response = await apiClient.post<ApiResponse<string>>(`/api/Photos/activity/${id}`, photo);
    return handleResponse(response);
}

export const deleteActivityPhoto = async (activityId: string, publicId: string) => {
    const response = await apiClient.delete<ApiResponse<string>>(`/api/Photos/activity/${activityId}/${publicId}`);
    return handleResponse(response);
}

export const deleteUserPhoto = async (publicId: string) => {
    const response = await apiClient.delete<ApiResponse<string>>(`/api/Photos/${publicId}`);
    return handleResponse(response);
}