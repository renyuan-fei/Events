import {AxiosResponse} from "axios";

export function handleResponse<T>(response: AxiosResponse<T>): T {
    if (response.status >= 200 && response.status < 300) {
        return response.data;
    } else {
        throw new Error(`Error: ${response.status}`);
    }
}
