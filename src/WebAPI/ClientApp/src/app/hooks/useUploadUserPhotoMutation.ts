import {useAppDispatch} from "@store/store.ts";
import {useMutation} from "react-query";
import {uploadUserPhoto} from "@apis/Photos.ts";
import {queryClient} from "@apis/queryClient.ts";
import {setAlertInfo} from "@features/commonSlice.ts";

const useUploadUserPhotoMutation = (userId: string) => {
    const dispatch = useAppDispatch();

    const {isLoading, mutateAsync} = useMutation(
        (photo: FormData) => uploadUserPhoto(photo),
        {
            onSuccess() {
                queryClient.invalidateQueries(['TopPhotos', userId]);
                dispatch(setAlertInfo({
                    open: true,
                    message: "Photo uploaded successfully",
                    variant: "success"
                }));
            },
            onError() {
                dispatch(setAlertInfo({
                    open: true,
                    message: "Photo upload failed",
                    variant: "error"
                }));
            }
        }
    )

    return {isUploading:isLoading, upload: mutateAsync};
};

export default useUploadUserPhotoMutation;
