import {useDispatch} from "react-redux";
import {useMutation} from "react-query";
import {unfollow} from "@apis/Following.ts";
import {queryClient} from "@apis/queryClient.ts";
import {setAlertInfo} from "@features/commonSlice.ts";


const useUnfollowMutation = (id :string) => {
    const dispatch = useDispatch();

    const {isLoading, mutateAsync} = useMutation(
        () => unfollow(id),
        {
            onSuccess: () => {
                queryClient.invalidateQueries(["UserProfile", id]);
                queryClient.invalidateQueries(["IsFollowing", id]);
                dispatch(setAlertInfo({
                    open: true,
                    message: "Unfollowed successfully",
                    severity: "success"
                }));
            },
            onError: () => {
                dispatch(setAlertInfo({
                    open: true,
                    message: "Unfollow failed",
                    severity: "error"
                }));
            }
    });

    return {
        isUnfollowing:isLoading,
        unfollow:mutateAsync
    }
};

export default useUnfollowMutation;