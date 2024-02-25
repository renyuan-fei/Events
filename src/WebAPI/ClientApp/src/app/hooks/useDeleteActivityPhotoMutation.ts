import {useMutation} from "react-query";
import {deleteActivityPhoto} from "@apis/Photos.ts";
import {useAppDispatch} from "@store/store.ts";
import {setAlertInfo} from "@features/commonSlice.ts";
import {queryClient} from "@apis/queryClient.ts";

const useDeleteActivityPhotoMutation = (activityId: string) => {
    const dispatch = useAppDispatch();

    const {
        isLoading,
        mutateAsync
    } = useMutation(
        (publicId: string) => deleteActivityPhoto(activityId,publicId),
        {
            onSuccess() {
                queryClient.invalidateQueries('TopPhotos');

                dispatch(setAlertInfo({
                    open: true,
                    message: "Photo deleted successfully",
                    severity: "success"
                }))
            },
            onError() {
                dispatch(setAlertInfo({
                    open: true,
                    message: "Photo delete failed",
                    severity: "error"
                }))
            }
        });

    return {isDeleting: isLoading, delete: mutateAsync};
}

export default useDeleteActivityPhotoMutation;