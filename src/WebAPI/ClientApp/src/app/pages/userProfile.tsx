import {useNavigate, useParams} from "react-router";
import Box from "@mui/material/Box";
import { Grid, Stack, useTheme } from "@mui/material";
import LoadingComponent from "@ui/LoadingComponent.tsx";
import {queryClient} from "@apis/queryClient.ts";
import {userInfo} from "@type/userInfo.ts";
import useIsFollowingQuery from "@features/Profile/hooks/useIsFollowingQuery.ts";
import useGetUserProfileQuery from "@features/Profile/hooks/useGetUserProfileQuery.ts";
import useTopPhotos from "@features/Profile/hooks/useTopPhotosQuery.ts";
import CustomImageList from "@ui/Custom/CustomImageList.tsx";
import UserCard from "@features/Profile/UserCard.tsx";
import UserInformation from "@features/Profile/UserInformation.tsx";

const UserProfile = () => {
    const theme = useTheme();
    const navigate = useNavigate();
    const { userId } = useParams<{ userId: string }>();
    const { data : userProfile, isLoading: isUserProfileLoading } = useGetUserProfileQuery(userId!);
    const { data : Photos, isLoading: isPhotosLoading } = useTopPhotos(userId!);
    const currentUserId = queryClient.getQueryData<userInfo>("userInfo")?.id;
    const isCurrentUser = currentUserId === userId;

    const { isLoading: isFollowingLoading, isFollowing } = useIsFollowingQuery(userId!, currentUserId!);

    if (isUserProfileLoading|| isPhotosLoading || (isFollowingLoading && !isCurrentUser)) {
        return <LoadingComponent />;
    }

    if (!userProfile) {
        navigate("/notfound");
        return null;
    }

    const {
        photos,
        remainingCount
    } = Photos!;

    const {
        id,
        displayName,
        bio,
        email,
        phoneNumber,
        image,
        followers,
        following,
    } = userProfile!;

    if (!userProfile) {
        return <div>Error</div>;
    }


    return (
        <Box>
            <Grid container spacing={theme.spacing(8)}>
                <Grid item lg={4}>
                    <UserCard id={id} image={image} isCurrentUser={isCurrentUser} isFollowed={isFollowing!} followers={followers} following={following} />
                </Grid>
                <Grid item lg={8}>
                    <Stack spacing={2}>
                        <UserInformation name={displayName} email={email} bio={bio} phoneNumber={phoneNumber}/>
                        <CustomImageList id={id} isCurrentUser={isCurrentUser} photos={photos} remainingCount={remainingCount}/>
                    </Stack>
                </Grid>
            </Grid>
        </Box>
    );
}

export default UserProfile;
