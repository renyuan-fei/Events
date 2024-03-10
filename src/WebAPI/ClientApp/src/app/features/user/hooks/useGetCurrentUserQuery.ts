import {useAppDispatch} from "@store/store.ts";
import {useQuery} from "react-query";
import {AuthResponse} from "@type/Account.ts";
import {loginAction} from "@features/user/userSlice.ts";
import {getCurrentUser} from "@apis/Account.ts";
import {AxiosError} from "axios";
import {setAlertInfo} from "@features/commonSlice.ts";
import {ApiResponse} from "@type/ApiResponse.ts";
import {startNotificationConnection} from "@config/NotificationHubConnection.ts";

const useGetCurrentUserQuery = () => {
    const dispatch = useAppDispatch()

    const {refetch, isRefetching} = useQuery<AuthResponse, AxiosError<ApiResponse<any>>, AuthResponse>(
        "userInfo",
        () => getCurrentUser(), {
            enabled: false,
            onSuccess(data: AuthResponse) {
                dispatch(loginAction({
                    token: data.token,
                    userName: data.displayName,
                    imageUrl: data.image
                }));

                dispatch(startNotificationConnection())
            },
            onError(error: AxiosError<ApiResponse<any>>) {
                dispatch(setAlertInfo({
                    open: true,
                    message: error.message,
                    severity: "error"
                }));
            }
        })

    return {refetch, isRefetching};
}

export default useGetCurrentUserQuery;