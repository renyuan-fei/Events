import {useMutation} from "react-query";
import {RemoveAttendee} from "@apis/Activities.ts";
import {useDispatch} from "react-redux";
import {setAlertInfo} from "@features/commonSlice.ts";
import {AxiosError} from "axios";
import {ApiResponse} from "@type/ApiResponse.ts";
import {queryClient} from "@apis/queryClient.ts";

const useRemoveAttendeeMutation = (activityId: string) => {
    const dispatch = useDispatch();

    const {isLoading, mutateAsync} = useMutation(
        () => RemoveAttendee(activityId),
        {
            onSuccess: () => {
                queryClient.invalidateQueries(['activity',activityId])
                dispatch(setAlertInfo({
                    open: true,
                    message: "You have successfully left the activity",
                    severity: "success"
                }))
            },
            onError: (error: AxiosError<ApiResponse<any>>) => {
                dispatch(setAlertInfo({
                    open: true,
                    message: error.response?.data.message || 'Failed to leave the activity',
                    severity: "error"
                }));
            }
        });

    return {
        isRemovingAttendee: isLoading,
        removeAttendee: mutateAsync
    }
}

export default useRemoveAttendeeMutation;