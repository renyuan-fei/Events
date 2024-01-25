import {AuthResponse, LoginRequest, RegisterRequest} from "@type/Account.ts";
import apiClient from "@apis/BaseApi.ts";
import {useMutation, useQuery} from "react-query";
import {loginAction, logoutAction} from "@features/user/userSlice.ts";
import {useAppDispatch} from "@store/store.ts";
import {handleResponse} from "@apis/ApiHandler.ts";
import {ApiResponse} from "@type/ApiResponse.ts";
import {debounce} from "@mui/material";
import {UserProfile} from "@type/UserProfile.ts";


// 登录请求
async function login(requestData: LoginRequest): Promise<AuthResponse> {
    const response = await apiClient.post<ApiResponse<AuthResponse>>('/api/Account/Login', requestData);
    return handleResponse(response);
}

async function register(requestData: RegisterRequest): Promise<AuthResponse> {
    const response = await apiClient.post<ApiResponse<AuthResponse>>('/api/Account/Register', requestData);
    return handleResponse(response);
}


async function logout(): Promise<void> {
    await apiClient.post<AuthResponse>('/api/Account/Logout');
}

async function getCurrentUser(): Promise<AuthResponse> {
    const response = await apiClient.get<ApiResponse<AuthResponse>>('/api/Account/')
    console.log(response);
    return handleResponse(response);
}

async function GetUserProfile(id: string): Promise<UserProfile> {
    if (!id) {
        id = ''
    }
    const response = await apiClient.get<ApiResponse<UserProfile>>(`/api/Users/${id}`);
    return handleResponse(response);
}

export const useGetUserProfileQuery = (id: string | undefined) => {
    let key = id;
    if (id === undefined) {
        key = 'main'
    }
    return useQuery(
        ['UserProfile', key],
        () => GetUserProfile(id || ''),
        {
            onSuccess: (data: UserProfile) => {
                console.log(data);
            },
            onError: (error: any) => {
                console.log(error);
            },
            enabled: !!id // 只有在 id 非空时才启用查询
        }
    );
}

export const checkEmailRegistered = debounce(async (email: string) => {
    try {
        const response = await apiClient.get(`/api/Account/email`, {params: {email}});
        return response.status !== 200;
    } catch (error) {
        return true; // 假设错误意味着电子邮件已注册
    }
}, 500); // 500 毫秒的延迟


export const useLoginMutation = (
    additionalOnSuccess?: (data: AuthResponse) => void,
    additionalOnError?: (error: any) => void
) => {
    const dispatch = useAppDispatch();

    return useMutation(
        (loginRequest: LoginRequest) => login(loginRequest),
        {
            onSuccess: (data: AuthResponse) => {

                console.log("login:", data);
                dispatch(loginAction({
                    token: data.token,
                    userName: data.displayName,
                    imageUrl: data.image,
                }));

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


export const useRegisterMutation = (additionalOnSuccess?: (data: AuthResponse) => void,
                                    additionalOnError?: (error: any) => void) => {
    const dispatch = useAppDispatch();

    return useMutation(
        (registerRequest: RegisterRequest) => register(registerRequest),
        {
            onSuccess: (data: AuthResponse) => {
                dispatch(loginAction({
                    token: data.token,
                    userName: data.displayName,
                    imageUrl: data.image
                }));
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


export const useLogoutMutation = (additionalOnSuccess?: () => void,
                                  additionalOnError?: (error: any) => void) => {
    const dispatch = useAppDispatch()

    return useMutation(() => logout(), {
        onSuccess() {
            dispatch(logoutAction());

            if (additionalOnSuccess) {
                additionalOnSuccess();
            }
        },
        onError(error: any) {

            if (additionalOnError) {
                additionalOnError(error);
            }
        }
    });
};


export const useGetCurrentUserQuery = () => {
    const dispatch = useAppDispatch()

    return useQuery("userInfo", () => getCurrentUser(), {
        enabled: false,
        onSuccess(data: AuthResponse) {
            dispatch(loginAction({
                token: data.token,
                userName: data.displayName,
                imageUrl: data.image
            }));
        },
        onError(error: any) {
            console.log(error);
        }
    })
}