import {useMutation} from "react-query";
import {uploadUserAvatar} from "../apis/Photos";
import {queryClient} from "@apis/queryClient";
import {useAppDispatch} from "@store/store";
import {setAlertInfo} from "@features/commonSlice";

const useUploadUserAvatarMutation = (userId: string) => {
    const dispatch = useAppDispatch();
    const {isLoading,mutateAsync} = useMutation(
        (photo: FormData) => uploadUserAvatar(photo),
        {
            onSuccess() {
                queryClient.fetchQuery(['userInfo']);
                queryClient.invalidateQueries(['UserProfile', userId]);
                dispatch(setAlertInfo({
                    open: true,
                    message: "Avatar uploaded successfully",
                    variant: "success"
                }));
            },
            onError() {
                dispatch(setAlertInfo({
                    open: true,
                    message: "Avatar upload failed",
                    variant: "error"
                }));
            }
        });
    return {isUploading:isLoading, upload: mutateAsync};
}

export default useUploadUserAvatarMutation;