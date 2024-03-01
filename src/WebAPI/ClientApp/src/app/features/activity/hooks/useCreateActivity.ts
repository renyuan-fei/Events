import {CreateActivity} from "@apis/Activities.ts";
import {NewActivity} from "@type/NewActivity.ts";
import {useAppDispatch} from "@store/store.ts";
import {setAlertInfo} from "@features/commonSlice.ts";
import {useMutation} from "react-query";

const UseCreateActivity = () => {
    const dispatch = useAppDispatch();

    const {isLoading, data, mutateAsync} = useMutation(
        (activity: NewActivity) => CreateActivity(activity),
        {
            onError: () => {
                dispatch(setAlertInfo({
                    open: true,
                    severity: "error",
                    message: "Could not create activity"
                }));
            }
        }
    )

    return {
        isCreating:isLoading,
        activityId:data,
        create:mutateAsync
    }
}

export default UseCreateActivity;