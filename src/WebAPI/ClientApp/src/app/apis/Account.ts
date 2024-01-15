import {AuthResponse, LoginRequest, RegisterRequest} from "@type/Account.ts";
import axiosInstance from "@apis/BaseApi.ts";
import {useMutation, useQuery} from "react-query";
import {loginAction} from "@features/user/userSlice.ts";
import {useAppDispatch} from "@store/store.ts";


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

export async function getCurrentUser(): Promise<AuthResponse> {
    const response = await axiosInstance.get<AuthResponse>('/api/Account/')
    return response.data;
}

// 使用 useMutation 钩子
export const useLoginQuery = (
    loginRequest: LoginRequest,
) => {
    const dispatch = useAppDispatch()

    return useQuery(
        "userInfo",
        () => login({
            email: loginRequest.email,
            password: loginRequest.password
        }),
        {
            enabled: false,
            onSuccess(data: AuthResponse) {
                console.log(data);
                dispatch(loginAction({token: data.token}));
            },
            onError(error: any) {
                console.log(error);
            }
        }
    );
};

export const useRegisterQuery = (
    registerRequest: RegisterRequest,
) => {
    const dispatch = useAppDispatch()

    return useQuery(
        "userInfo",
        () => register({
            email: registerRequest.email,
            displayName: registerRequest.displayName,
            password: registerRequest.password,
            confirmPassword: registerRequest.confirmPassword,
            phoneNumber: registerRequest.phoneNumber
        }),
        {
            enabled: false,
            onSuccess(data: AuthResponse) {
                console.log(data);
                dispatch(loginAction({token: data.token}));
            },
            onError(error: any) {
                console.log(error);
            }
        }
    );
};

export const useLogoutMutation = () => {
    const dispatch = useAppDispatch()

    return useMutation("userInfo",() => logout(), {
        onSuccess() {
            dispatch(loginAction({token: null}));
        },
        onError(error: any) {
            console.log(error);
        }
    });
};

export const useGetCurrentUserQuery = () => {
    const dispatch = useAppDispatch()

    return useQuery("userInfo", () => getCurrentUser(), {
        enabled: false,
        onSuccess(data: AuthResponse) {
            console.log(data);
            dispatch(loginAction({token: data.token}));
        },
        onError(error: any) {
            console.log(error);
        }
    })
}