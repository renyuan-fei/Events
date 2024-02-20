import {AuthResponse, LoginRequest} from "@type/Account.ts";
import {useAppDispatch} from "@store/store.ts";
import {useMutation} from "react-query";
import {loginAction} from "@features/user/userSlice.ts";
import {login} from "@apis/Account.ts";
import {AxiosError} from "axios";
import {setAlertInfo, setLoginForm} from "@features/commonSlice.ts";
import {queryClient} from "@apis/queryClient.ts";
import {useState} from "react";
import {useNavigate} from "react-router";
import {ApiResponse} from "@type/ApiResponse.ts";

const useLoginMutation = () => {
    const dispatch = useAppDispatch();
    const navigate = useNavigate();
    const [isLoggingIn, setIsLoggingIn] = useState(false);

    const {mutateAsync} = useMutation<AuthResponse, AxiosError<ApiResponse<any>>, LoginRequest>(
        async (loginRequest: LoginRequest) => await login(loginRequest),
        {
            onMutate: async () => {
                setIsLoggingIn(true);
            },
            onSuccess: async (data: AuthResponse) => {
                dispatch(loginAction({token: data.token}));
                await queryClient.refetchQueries('userInfo');

                dispatch(setAlertInfo({
                    open: true,
                    message: 'You have been logged in',
                    severity: 'success',
                }));
                dispatch(setLoginForm(false));
                navigate('/home');
            },
            onError: (error: AxiosError<ApiResponse<any>>) => {
                dispatch(setAlertInfo({
                    open: true,
                    message: error.response?.data.message || 'Login failed',
                    variant: "error"
                }));
            },
            onSettled: () => {
                setIsLoggingIn(false);
            },
        }
    );

    // 将 isLoading 替换为 isLoggingIn 以确保在整个登录过程中保持加载状态
    return {loginMutate:mutateAsync, isLoading: isLoggingIn};
};

export default useLoginMutation;
