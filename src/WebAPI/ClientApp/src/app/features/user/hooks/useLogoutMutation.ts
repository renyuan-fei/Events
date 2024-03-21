import {useAppDispatch} from "@store/store.ts";
import {useMutation} from "react-query";
import {logoutAction} from "@features/user/userSlice.ts";
import {logout} from "@apis/Account.ts";
import {AxiosError} from "axios";
import {setAlertInfo, setLoginForm} from "@features/commonSlice.ts";
import {queryClient} from "@apis/queryClient.ts";
import {ApiResponse} from "@type/ApiResponse.ts";
import {stopNotificationConnection} from "@config/NotificationHubConnection.ts";
import {useNavigate} from "react-router";

const useLogoutMutation = () => {
    const dispatch = useAppDispatch()
    const navigate = useNavigate();

    const {mutateAsync, isLoading} = useMutation<void, AxiosError<ApiResponse<any>>, void>(
        async () => await logout(),
        {
            onSuccess() {
                queryClient.invalidateQueries();
                dispatch(stopNotificationConnection())
                dispatch(logoutAction());
                dispatch(setAlertInfo({
                    open: true,
                    message: 'You have been logged out',
                    severity: 'success',
                }));
                navigate('/');
                dispatch(setLoginForm(false))
            },
            onError(error: AxiosError<ApiResponse<any>>) {
                dispatch(setAlertInfo({
                    open: true,
                    message: error.response?.data.message || 'Logout failed',
                    severity: "error"
                }));
            },
        });

    return {mutateAsync, isLoading};
};

export default useLogoutMutation;