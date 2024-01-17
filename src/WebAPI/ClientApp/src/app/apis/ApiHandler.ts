import {AxiosResponse} from "axios";
import {ApiResponse} from "@type/ApiResponse.ts";

export function handleResponse<T>(response: AxiosResponse<ApiResponse<T>>): T {
    console.log(response);
    if (response.status >= 200 && response.status < 300) {
        return response.data.data;
    } else {
        throw new Error(`Error: ${response.data.message}`);
    }
}
