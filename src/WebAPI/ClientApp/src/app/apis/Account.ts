import {AuthResponse, LoginRequest, RegisterRequest} from "@type/Account.ts";
import apiClient from "@apis/BaseApi.ts";
import {useMutation, useQuery} from "react-query";
import {loginAction, logoutAction} from "@features/user/userSlice.ts";
import {useAppDispatch} from "@store/store.ts";
import {setAlertInfo} from "@features/commonSlice.ts";
import {handleResponse} from "@apis/ApiHandler.ts";
import axios from "axios";


// 登录请求
export async function login(requestData: LoginRequest) {
    const response = await apiClient.post<AuthResponse>('/api/Account/Login', requestData);
    return handleResponse(response);
}

export async function register(requestData: RegisterRequest) {
    const response = await apiClient.post<AuthResponse>('/api/Account/Register', requestData);
    return handleResponse(response);
}


export async function logout() : Promise<void> {
    await apiClient.post<AuthResponse>('/api/Account/Logout');
}

export async function getCurrentUser() {
    const response = await apiClient.get<AuthResponse>('/api/Account/')
    return handleResponse(response);
}

export async function checkEmailRegistered (email : string){
    try {
        const response = await axios.get(`https://localhost:7095/api/Account/email`, { params: { email } });
        return response.status !== 200;
    } catch (error) {
        return true; // 假设错误意味着电子邮件已注册
    }
};

export const useLoginMutation = (
    loginRequest: LoginRequest,
    additionalOnSuccess?: (data: AuthResponse) => void,
    additionalOnError?: (error: any) => void
) => {
    const dispatch = useAppDispatch();

    return useMutation(
        () => login(loginRequest),
        {
            onSuccess: (data: AuthResponse) => {
                dispatch(loginAction({ token: data.token , userName: data.displayName, imageUrl: data.image }));

                if (additionalOnSuccess) {
                    additionalOnSuccess(data);
                }
            },
            onError: (error: any) => {

                if (additionalOnError) {
                    additionalOnError(error);
                }
            },
        }
    );
};



export const useRegisterMutation = (registerRequest: RegisterRequest,additionalOnSuccess?: (data: AuthResponse) => void,
                                    additionalOnError?: (error: any) => void) => {
    const dispatch = useAppDispatch();

    return useMutation(
        () => register(registerRequest),
        {
            onSuccess: (data: AuthResponse) => {
                dispatch(loginAction({ token: data.token , userName: data.displayName, imageUrl: data.image }));
                if (additionalOnSuccess) {
                    additionalOnSuccess(data);
                }
            },
            onError: (error: any) => {

                if (additionalOnError) {
                    additionalOnError(error);
                }
            },
        });
};


export const useLogoutMutation = () => {
    const dispatch = useAppDispatch()

    return useMutation(() => logout(), {
        onSuccess() {
            dispatch(logoutAction());
            dispatch(setAlertInfo({
                open: true,
                message: 'You have been logged out',
                severity: 'success',
            }));
        },
        onError(error: any) {
            dispatch(setAlertInfo({
                open: true,
                message: error.message,
                severity: 'error',
            }));
        }
    });
};


export const useGetCurrentUserQuery = () => {
    const dispatch = useAppDispatch()

    return useQuery("userInfo", () => getCurrentUser(), {
        enabled: false,
        onSuccess(data: AuthResponse) {
            dispatch(loginAction({ token: data.token , userName: data.displayName, imageUrl: data.image }));
        },
        onError(error: any) {
            console.log(error);
        }
    })
}