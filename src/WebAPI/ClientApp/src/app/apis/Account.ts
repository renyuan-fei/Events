import {AuthResponse, LoginRequest, RegisterRequest} from "@type/Account.ts";
import apiClient from "@apis/BaseApi.ts";
import {handleResponse} from "@apis/ApiHandler.ts";
import {ApiResponse} from "@type/ApiResponse.ts";

// 登录请求
export const login = async (requestData: LoginRequest): Promise<AuthResponse> => {
    const response = await apiClient.post<ApiResponse<AuthResponse>>('/api/Account/Login', requestData);
    return handleResponse(response);
}

export const register = async (requestData: RegisterRequest): Promise<AuthResponse> => {
    const response = await apiClient.post<ApiResponse<AuthResponse>>('/api/Account/Register', requestData);
    return handleResponse(response);
}

export const logout = async (): Promise<void> => {
    await apiClient.post<AuthResponse>('/api/Account/Logout');
}

export const getCurrentUser = async (): Promise<AuthResponse> => {
    const response = await apiClient.get<ApiResponse<AuthResponse>>('/api/Account/')
    return handleResponse(response);
}

export const checkEmailRegistered = async (email: string) => {
    try {
        const response = await apiClient.get(`/api/Account/email`, {params: {email}});
        console.log(response);
        const result = response.status !== 200;
        console.log(result);
        return result;
    } catch (error) {
        console.error(error);
        return true; // 假设错误意味着电子邮件已注册
    }
};






