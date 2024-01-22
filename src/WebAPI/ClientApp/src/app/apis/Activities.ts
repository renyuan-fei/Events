import apiClient from "@apis/BaseApi.ts";
import {handleResponse} from "@apis/ApiHandler.ts";
import {ApiResponse} from "@type/ApiResponse.ts";
import {Activity} from "@type/Activity.ts";
import {useQuery} from "react-query";

async function GetActivities({
                                        page = 1,
                                        pageSize = 10,
                                        searchTerm = []
                                    }: {
    page: number,
    pageSize: number,
    searchTerm?: string[]
} = { page: 1, pageSize: 10, searchTerm: [] }): Promise<paginatedResponse> {

    const params = new URLSearchParams();
    searchTerm.forEach(term => params.append("searchTerm", term));

    let url = `/api/Activities/${page}/${pageSize}`;
    if (params.toString()) {
        url += `?${params.toString()}`;
    }

    const response = await apiClient.get<ApiResponse<paginatedResponse>>(url);

    return handleResponse(response);
}
async function GetActivity(id: string) {
    const response = await apiClient.get<ApiResponse<Activity>>(`/api/Activities/${id}`)
    return handleResponse(response);
}

// const { data, isLoading, error } = useQuery<ApiResponse<paginatedResponse>, Error>(['upcomingActivities', 1, 8], () => GetActivities({ page: 1, pageSize: 8 }));

export const useGetActivitiesQuery = () => {
    return useQuery(['paginatedActivities', 1, 8], () => GetActivities({ page: 1, pageSize: 8 }), {
    })
}

export const useGetActivityQuery = (id: string) => {
    return useQuery(['activities', id], () => GetActivity(id), {
        enabled: !!id
    })
}