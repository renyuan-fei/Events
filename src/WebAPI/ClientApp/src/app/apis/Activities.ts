import apiClient from "@apis/BaseApi.ts";
import {handleResponse} from "@apis/ApiHandler.ts";
import {ApiResponse} from "@type/ApiResponse.ts";
import {Activity} from "@type/Activity.ts";
import {Item, PaginatedResponse} from "@type/PaginatedResponse.ts";

export const GetPaginatedActivities = async (
    pageNumber: number = 1,
    pageSize: number = 10,
    filters: Record<string, string>
): Promise<PaginatedResponse<Item>> => {
    const params = new URLSearchParams({
        ...filters,
        PageNumber: pageNumber.toString(),
        PageSize: pageSize.toString(),
    });

    const url = `/api/Activities${params.toString() ? `?${params}` : ''}`;

    const response = await apiClient.get<ApiResponse<PaginatedResponse<Item>>>(url);
    return handleResponse(response);
}

export const GetActivity = async (id: string) => {
    const response = await apiClient.get<ApiResponse<Activity>>(`/api/Activities/${id}`)
    return handleResponse(response);
}

export const AddAttendee = async (activityId: string): Promise<any> => {
    const response = await apiClient.post<ApiResponse<any>>(`/api/Activities/${activityId}/attendee`);
    return handleResponse(response);
}

export const RemoveAttendee = async (activityId: string): Promise<any> => {
    const response = await apiClient.delete<ApiResponse<any>>(`/api/Activities/${activityId}/attendee`);
    return handleResponse(response);
}

// TODO cancel attend activity (only for host user)

// TODO create activity

// TODO update activity