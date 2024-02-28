import {useMutation} from "react-query";
import {UpdateUserProfile} from "@apis/Profile.ts";
import {UserProfile} from "@type/UserProfile.ts";
import {useDispatch} from "react-redux";
import {queryClient} from "@apis/queryClient.ts";
import {setAlertInfo} from "../../commonSlice";

const useUpdateUserProfileMutation = (id: string) => {
    const dispatch = useDispatch();

    const {isLoading,mutateAsync} = useMutation((data: UserProfile) => UpdateUserProfile(data), {
        onSuccess: () => {
            queryClient.invalidateQueries(['UserProfile', id]);
            dispatch(setAlertInfo({
                open: true,
                message: "Profile updated successfully",
                severity: "success"
            }));
        },
        onError: () => {
            dispatch(setAlertInfo({
                open: true,
                message: "Profile update failed",
                severity: "error"
            }));
        }
    });

    return {isUpdating: isLoading, update: mutateAsync};
}

export default useUpdateUserProfileMutation;