import {useQuery} from "react-query";
import {UserProfile} from "@type/UserProfile.ts";
import {AxiosError} from "axios";
import {setAlertInfo} from "@features/commonSlice.ts";
import {useAppDispatch} from "@store/store.ts";
import {GetUserProfile} from "@apis/Profile.ts";

const useGetUserProfileQuery = (id: string) => {
    const dispatch = useAppDispatch()

    const {data,isLoading} = useQuery<UserProfile,AxiosError>(
        ['UserProfile', id],
        () => GetUserProfile(id || ''),
        {
            onError(error: AxiosError) {
                dispatch(setAlertInfo({open: true,message: error.message, severity: "error"}));
            },
            enabled: !!id // 只有在 id 非空时才启用查询
        }
    );

    return {data,isLoading};
}

export default useGetUserProfileQuery;