import {useDispatch} from "react-redux";
import {useMutation} from "react-query";
import {reactiveActivity} from "@apis/Activities.ts";
import {queryClient} from "@apis/queryClient.ts";
import {setAlertInfo} from "@features/commonSlice.ts";


const useReactiveActivityMutation =  (activityId: string)=> {
    const dispatch = useDispatch();

    const {isLoading, mutateAsync} = useMutation(() => reactiveActivity(activityId),{
        onSuccess: () => {
            queryClient.invalidateQueries(["activity", activityId]);
            dispatch(setAlertInfo({
                open: true,
                message: 'Activity reactive',
                severity: 'success'
            }));
        },
        onError: () => {
            dispatch(setAlertInfo({
                open: true,
                message: 'Failed to reactive activity',
                severity: 'error'
            }));
        }
    });

    return {isReactivate: isLoading, reactivateActivity: mutateAsync}
}

export default useReactiveActivityMutation;