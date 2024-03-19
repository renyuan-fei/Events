import {useDispatch} from "react-redux";
import {useMutation} from "react-query";
import {unfollow} from "@apis/Following.ts";
import {queryClient} from "@apis/queryClient.ts";
import {setAlertInfo} from "@features/commonSlice.ts";
import {userInfo} from "@type/UserInfo.ts";


const useUnfollowMutation = (id :string) => {
    const dispatch = useDispatch();
    const currentUserId = queryClient.getQueryData<userInfo>("userInfo")?.id;

    const {isLoading, mutateAsync} = useMutation(
        () => unfollow(id),
        {
            onSuccess: () => {
                console.log(id)
                queryClient.invalidateQueries(["userProfile", id]);
                queryClient.invalidateQueries(["IsFollowing", id]);
                queryClient.invalidateQueries(["following", currentUserId]);

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