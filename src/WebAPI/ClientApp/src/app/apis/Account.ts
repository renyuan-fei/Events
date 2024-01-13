import {AuthResponse, LoginRequest, RegisterRequest} from "@type/Account.ts";
import axiosInstance from "@apis/BaseApi.ts";
import {useMutation} from "react-query";


// 登录请求
export async function login
(requestData: LoginRequest): Promise<AuthResponse> {
    const response = await axiosInstance.post<AuthResponse>('/api/Account/Login', requestData);
    return response.data;
}

export async function register(requestData: RegisterRequest): Promise<AuthResponse> {
    const response = await axiosInstance.post<AuthResponse>('/api/Account/Register', requestData);
    return response.data;
}

export async function logout() {
// 这里不再需要设置 Authorization 头部，因为 axiosInstance 的拦截器已经处理了
    await axiosInstance.post('/api/Account/Logout');
}

// 使用 useMutation 钩子
export const useLoginMutation = () => {
    return useMutation(login);
};

export const useRegisterMutation = () => {
    return useMutation(register);
};

export const useLogoutMutation = () => {
    return useMutation(logout);
};