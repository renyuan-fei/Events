import {useMutation} from "react-query";
import {AddAttendee} from "@apis/Activities.ts";
import {useDispatch} from "react-redux";
import {setAlertInfo} from "@features/commonSlice.ts";
import {AxiosError} from "axios";
import {ApiResponse} from "@type/ApiResponse.ts";
import {queryClient} from "@apis/queryClient.ts";

const useAddAttendeeMutation = (activityId: string) => {
    const dispatch = useDispatch();

    const {isLoading, mutateAsync} = useMutation(
        () => AddAttendee(activityId),
        {
            onSuccess: () => {
                queryClient.invalidateQueries(['activity',activityId])
                dispatch(setAlertInfo({
                    open: true,
                    message: "You have successfully joined the activity",
                    severity: "success"
                }))
            },
            onError: (error: AxiosError<ApiResponse<any>>) => {
                dispatch(setAlertInfo({
                    open: true,
                    message: error.response?.data.message || 'Failed to join the activity',
                    severity: "error"
                }));
            }
        });

    return {
        isAddingAttendee: isLoading,
        addAttendee: mutateAsync
    }
}

export default useAddAttendeeMutation;