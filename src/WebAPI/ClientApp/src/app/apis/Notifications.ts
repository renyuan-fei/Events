import apiClient from "@apis/BaseApi.ts";
import {handleResponse} from "@apis/ApiHandler.ts";
import {PaginatedResponse} from "@type/PaginatedResponse.ts";
import {NotificationMessage} from "@type/NotificationMessage.ts";
import {ApiResponse} from "@type/ApiResponse.ts";

export const getPaginatedNotifications = async (pageNumber:number,pageSize:number,initialTimeStamp:string): Promise<PaginatedResponse<NotificationMessage>> => {
    const response = await apiClient.get<ApiResponse<PaginatedResponse<NotificationMessage>>>('/api/Notifications/', {
        params: {pageNumber,pageSize,initialTimeStamp}
    });
    return handleResponse(response);
};