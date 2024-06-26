import {useDispatch} from "react-redux";
import {useMutation} from "react-query";
import {cancelActivity} from "@apis/Activities.ts";
import {queryClient} from "@apis/queryClient.ts";
import {setAlertInfo} from "@features/commonSlice.ts";


const useCancelAttendActivityMutation =  (activityId: string)=> {
    const dispatch = useDispatch();

    const {isLoading, mutateAsync} = useMutation(() => cancelActivity(activityId),{
        onSuccess: () => {
            queryClient.invalidateQueries(["activity", activityId]);
            dispatch(setAlertInfo({
                open: true,
                message: 'Activity canceled',
                severity: 'success'
            }));
        },
        onError: () => {
            dispatch(setAlertInfo({
                open: true,
                message: 'Failed to cancel activity',
                severity: 'error'
            }));
        }
    });

    return {isCanceling: isLoading, cancelActivity: mutateAsync}
}

export default useCancelAttendActivityMutation;