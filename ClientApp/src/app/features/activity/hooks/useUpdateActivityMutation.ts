import {updateActivity} from "@apis/Activities.ts";
import {NewActivity} from "@type/NewActivity.ts";
import {useDispatch} from "react-redux";
import {setAlertInfo} from "../../commonSlice";
import {useMutation} from "react-query";

const useUpdateActivityMutation = (id: string) => {
    const dispatch = useDispatch();
    const {isLoading, mutateAsync} = useMutation(
        (activity: NewActivity) => updateActivity(id, activity),
        {
            onSuccess: () => {
                dispatch(setAlertInfo({
                    open: true,
                    severity: "success",
                    message: "Activity updated successfully"
                }))
            },
            onError: () => {
                dispatch(setAlertInfo({
                    open: true,
                    severity: "error",
                    message: "Activity update failed"
                }))
            }
        }
    )

    return {
        isUpdating: isLoading,
        update: mutateAsync
    }
}

export default useUpdateActivityMutation;