import {useQuery} from "react-query";
import {GetUserProfile} from "@apis/Account.ts";
import {UserProfile} from "@type/UserProfile.ts";
import {AxiosError} from "axios";
import {setAlertInfo} from "@features/commonSlice.ts";
import {useAppDispatch} from "@store/store.ts";

const useGetUserProfileQuery = (id: string) => {
    const dispatch = useAppDispatch()

    const {data,isLoading} = useQuery<UserProfile,AxiosError>(
        ['UserProfile', id],
        () => GetUserProfile(id || ''),
        {
            onSuccess: (data: UserProfile) => {
                console.log(data);
            },
            onError(error: AxiosError) {
                dispatch(setAlertInfo({open: true,message: error.message, variant: "error"}));
            },
            enabled: !!id // 只有在 id 非空时才启用查询
        }
    );

    return {data,isLoading};
}

export default useGetUserProfileQuery;