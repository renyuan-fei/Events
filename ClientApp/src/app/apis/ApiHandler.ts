import { AxiosResponse } from "axios";
import { ApiResponse } from "@type/ApiResponse.ts";

export function handleResponse<T>(response: AxiosResponse<ApiResponse<T>>): T {
    return response.data.data;
}
