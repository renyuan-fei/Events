import apiClient from "@apis/BaseApi.ts";
import {handleResponse} from "@apis/ApiHandler.ts";
import {ApiResponse} from "@type/ApiResponse.ts";
import {Activity} from "@type/Activity.ts";
import {Item, PaginatedResponse} from "@type/PaginatedResponse.ts";

export const GetPaginatedActivities = async (
    pageNumber: number = 1,
    pageSize: number = 10,
    searchTerm: string[] = [],
    extraParams: Record<string, string | string[]> = {}
): Promise<PaginatedResponse<Item>> => {
    const params = new URLSearchParams({
        ...extraParams,
        PageNumber: pageNumber.toString(),
        PageSize: pageSize.toString(),
        ...searchTerm.reduce((acc, term, index) => ({
            ...acc,
            [`searchTerm[${index}]`]: term
        }), {}),
    });

    const url = `/api/Activities${params.toString() ? `?${params}` : ''}`;

    const response = await apiClient.get<ApiResponse<PaginatedResponse<Item>>>(url);
    return handleResponse(response);
}

export const GetActivity = async (id: string) => {
    const response = await apiClient.get<ApiResponse<Activity>>(`/api/Activities/${id}`)
    return handleResponse(response);
}