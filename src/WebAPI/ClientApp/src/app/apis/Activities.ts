import apiClient from "@apis/BaseApi.ts";
import {handleResponse} from "@apis/ApiHandler.ts";
import {ApiResponse} from "@type/ApiResponse.ts";
import {Activity} from "@type/Activity.ts";

export const GetActivities = async ({
                                 page = 1,
                                 pageSize = 10,
                                 searchTerm = []
                             }: {
    page: number,
    pageSize: number,
    searchTerm?: string[]
} = { page: 1, pageSize: 10, searchTerm: [] }): Promise<paginatedResponse<Item>> => {

    const params = new URLSearchParams();
    params.append("page", page.toString());
    params.append("pageSize", pageSize.toString());

    searchTerm.forEach(term => params.append("searchTerm", term));

    let url = `/api/Activities`;
    if (params.toString()) {
        url += `?${params.toString()}`;
    }

    const response = await apiClient.get<ApiResponse<paginatedResponse<Item>>>(url);

    return handleResponse(response);
}
export const GetActivity = async (id: string) => {
    const response = await apiClient.get<ApiResponse<Activity>>(`/api/Activities/${id}`)
    return handleResponse(response);
}