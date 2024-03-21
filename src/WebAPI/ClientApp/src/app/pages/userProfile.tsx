import {useNavigate, useParams} from "react-router";
import Box from "@mui/material/Box";
import {Grid, Stack, useTheme} from "@mui/material";
import LoadingComponent from "@ui/LoadingComponent.tsx";
import {queryClient} from "@apis/queryClient.ts";
import {userInfo} from "@type/UserInfo.ts";
import useUploadUserPhotoMutation from "@hooks/useUploadUserPhotoMutation.ts";
import useDeleteUserPhotoMutation from "@hooks/useDeleteUserPhotoMutation.ts";
import useGetUserProfileQuery from "@features/profile/hooks/useGetUserProfileQuery.ts";
import useTopPhotosQuery from "@features/profile/hooks/useTopPhotosQuery.ts";
import UserInformation from "@features/profile/UserInformation.tsx";
import PhotoGallery from "@ui/Custom/PhotoGallery.tsx";
import UserCard from "@features/profile/UserCard.tsx";

const UserProfile = () => {
    const theme = useTheme();
    const navigate = useNavigate();
    const {userId} = useParams<{ userId: string }>();
    const {
        data: userProfile,
        isLoading: isUserProfileLoading
    } = useGetUserProfileQuery(userId!);
    const {data: Photos, isPhotosLoading} = useTopPhotosQuery(userId!);
    const currentUserId = queryClient.getQueryData<userInfo>("userInfo")?.id;
    const isCurrentUser = currentUserId === userId;
    const uploadHook = useUploadUserPhotoMutation(userId!)
    const deleteHook = useDeleteUserPhotoMutation(userId!);

    if (isUserProfileLoading || isPhotosLoading) {
        return <LoadingComponent/>;
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
                    <UserCard id={id}
                              image={image}
                              isCurrentUser={isCurrentUser}
                              followers={followers}
                              following={following}/>
                </Grid>
                <Grid item lg={8}>
                    <Stack spacing={2}>

                        <UserInformation name={displayName}
                                         email={email}
                                         bio={bio}
                                         phoneNumber={phoneNumber}/>

                        <PhotoGallery uploadHook={uploadHook}
                                      deleteHook={deleteHook}
                                      id={id}
                                      isCurrentUser={isCurrentUser}
                                      photos={photos}
                                      remainingCount={remainingCount}/>
                    </Stack>
                </Grid>
            </Grid>
        </Box>
    );
}

export default UserProfile;
