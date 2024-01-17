import apiClient from "@apis/BaseApi.ts";

export async function GetActivities({
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

    try {
        const response = await apiClient.get<paginatedResponse>(url);
        return response.data;
    } catch (error) {
        console.error(error);
        throw error;
    }
}





export async function GetActivity(id: string) {
    apiClient.get(`/api/Activities/${id}`)
        .then(response => {
            console.log(response.data);
        }).catch(error => {
        console.log(error);
    })
}