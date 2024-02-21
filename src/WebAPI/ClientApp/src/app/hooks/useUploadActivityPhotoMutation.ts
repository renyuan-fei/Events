import {uploadActivityPhoto} from "@apis/Photos.ts";
import {useMutation} from "react-query";
import {queryClient} from "@apis/queryClient.ts";
import {useAppDispatch} from "@store/store.ts";
import {setAlertInfo} from "@features/commonSlice.ts";

const useUploadActivityPhotoMutation = (id: string) => {
    const dispatch = useAppDispatch();

    const {isLoading, mutateAsync} = useMutation(
        (photo: FormData) => uploadActivityPhoto(photo, id),
        {
            onSuccess() {
                queryClient.fetchQuery(['TopPhotos', id]);

                dispatch(setAlertInfo({
                    open: true,
                    message: "Activity photo uploaded successfully",
                    variant: "success"
                }));
            },
            onError() {
                dispatch(setAlertInfo({
                    open: true,
                    message: "Activity photo upload failed",
                    variant: "error"
                }));
            }
        });
    return {isUploading:isLoading, upload: mutateAsync};
};

export default useUploadActivityPhotoMutation;