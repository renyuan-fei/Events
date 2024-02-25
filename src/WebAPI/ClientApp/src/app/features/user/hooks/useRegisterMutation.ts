import {useMutation} from "react-query";
import {loginAction} from "../userSlice";
import {register} from "@apis/Account.ts";
import {AuthResponse, RegisterRequest} from "@type/Account.ts";
import {useAppDispatch} from "@store/store.ts";
import {AxiosError} from "axios";
import {setAlertInfo, setSignUpForm} from "@features/commonSlice.ts";
import {queryClient} from "@apis/queryClient.ts";
import {useState} from "react";
import {useNavigate} from "react-router";
import {ApiResponse} from "@type/ApiResponse.ts";

const useRegisterMutation = () => {
    const dispatch = useAppDispatch();
    const navigate = useNavigate();
    const [isLoggingIn, setIsLoggingIn] = useState(false);

    const {
        mutateAsync,
    } = useMutation<AuthResponse, AxiosError<ApiResponse<any>>, RegisterRequest>(
        async (registerRequest: RegisterRequest) => await register(registerRequest),
        {
            onMutate: async () => {
                // 当变异开始时，设置登录中状态为 true
                setIsLoggingIn(true);
            },
            onSuccess: async (data: AuthResponse) => {
                dispatch(loginAction({token: data.token}));
                await queryClient.refetchQueries('userInfo');

                dispatch(setAlertInfo({
                    open: true,
                    message: 'Registration successful',
                    severity: 'success',
                }));

                dispatch(setSignUpForm(false))
                navigate('/home');
            },
            onError: (error: AxiosError<ApiResponse<any>>) => {
                console.log(error);
                console.log(error.response);
                dispatch(setAlertInfo({
                    open: true,
                    message: error.response?.data.message || 'Registration failed',
                    severity: "error"
                }));
            },
            onSettled: () => {
                // 无论成功或失败，最终都会调用此函数
                // 在这里设置登录中状态为 false
                setIsLoggingIn(false);
            }
        });

    return {mutateAsync, isLoading : isLoggingIn};
};

export default useRegisterMutation;