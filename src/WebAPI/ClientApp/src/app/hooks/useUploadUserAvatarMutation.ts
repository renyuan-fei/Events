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
                queryClient.invalidateQueries(['userProfile', userId]);
                dispatch(setAlertInfo({
                    open: true,
                    message: "Avatar uploaded successfully",
                    severity: "success"
                }));
            },
            onError() {
                dispatch(setAlertInfo({
                    open: true,
                    message: "Avatar upload failed",
                    severity: "error"
                }));
            }
        });
    return {isUploading:isLoading, upload: mutateAsync};
}

export default useUploadUserAvatarMutation;