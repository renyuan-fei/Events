import {useAppDispatch} from "@store/store.ts";
import {useMutation} from "react-query";
import {logoutAction} from "@features/user/userSlice.ts";
import {logout} from "@apis/Account.ts";
import {AxiosError} from "axios";
import {setAlertInfo} from "@features/commonSlice.ts";
import {queryClient} from "@apis/queryClient.ts";
import {ApiResponse} from "@type/ApiResponse.ts";

const useLogoutMutation = () => {
    const dispatch = useAppDispatch()

    const {mutateAsync, isLoading} = useMutation<void, AxiosError<ApiResponse<any>>, void>(
        async () => await logout(),
        {
            onSuccess() {
                queryClient.invalidateQueries();
                dispatch(logoutAction());
            },
            onError(error: AxiosError<ApiResponse<any>>) {
                dispatch(setAlertInfo({
                    open: true,
                    message: error.response?.data.message || 'Logout failed',
                    variant: "error"
                }));
            },
        });

    return {mutateAsync, isLoading};
};

export default useLogoutMutation;