import {useAppDispatch} from "@store/store.ts";
import {useMutation} from "react-query";
import {uploadActivityMainPhoto} from "@apis/Photos.ts";
import {setAlertInfo} from "@features/commonSlice.ts";

const useUploadActivityInitialPhotoMutation = () => {
    const dispatch = useAppDispatch();

    const {isLoading, mutateAsync} = useMutation(
        ({photo, id}: {
            photo: FormData,
            id?: string
        }) => uploadActivityMainPhoto(photo, id!),
        {
            onError() {
                dispatch(setAlertInfo({
                    open: true,
                    message: "Activity's main photo updated failed",
                    severity: "error"
                }));
            }
        });
    return {isUploading: isLoading, upload: mutateAsync};
};

export default useUploadActivityInitialPhotoMutation;