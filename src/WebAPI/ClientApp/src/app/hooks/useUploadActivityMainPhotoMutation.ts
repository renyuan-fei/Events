import {useAppDispatch} from "@store/store.ts";
import {useMutation} from "react-query";
import {uploadActivityMainPhoto} from "@apis/Photos.ts";
import {queryClient} from "@apis/queryClient.ts";
import {setAlertInfo} from "@features/commonSlice.ts";

const useUploadActivityMainPhotoMutation = (id?: string) => {
    const dispatch = useAppDispatch();

    const {isLoading, mutateAsync} = useMutation(
        ({photo,id}:{photo:FormData,id?:string}) => uploadActivityMainPhoto(photo, id!),
        {
            onSuccess() {
                queryClient.fetchQuery(['activity', id]);

                dispatch(setAlertInfo({
                    open: true,
                    message: "Activity's main photo updated successfully",
                    severity: "success"
                }));
            },
            onError() {
                dispatch(setAlertInfo({
                    open: true,
                    message: "Activity's main photo updated failed",
                    severity: "error"
                }));
            }
        });
    return {isUploading:isLoading, upload: mutateAsync};
};

export default useUploadActivityMainPhotoMutation;