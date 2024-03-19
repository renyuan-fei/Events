import {useAppDispatch} from "@store/store.ts";
import {useMutation} from "react-query";
import {follow} from "@apis/Following.ts";
import {queryClient} from "@apis/queryClient.ts";
import {setAlertInfo} from "@features/commonSlice.ts";


const useFollowMutation = (id: string) => {
    const dispatch = useAppDispatch();
    const {isLoading, mutateAsync} = useMutation(
        () => follow(id),
        {
            onSuccess() {
                queryClient.invalidateQueries(["userProfile", id]);
                queryClient.invalidateQueries(["IsFollowing", id]);
                dispatch(setAlertInfo({
                    open: true,
                    message: 'You are now following this user',
                    severity: 'success'
                }));
            },
            onError() {
                dispatch({type: "error"});
            }
        })
    return {isFollowing: isLoading, follow: mutateAsync};
};

export default useFollowMutation;