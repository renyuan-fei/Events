import {useNavigate, useParams} from "react-router";
import Box from "@mui/material/Box";
import {Grid, useTheme} from "@mui/material";
import PhotoGallery from "@ui/Custom/PhotoGallery.tsx";
import {useGetActivityQuery} from "@features/activity/hooks/useGetActivityQuery.ts";
import LoadingComponent from "@ui/LoadingComponent.tsx";
import {queryClient} from "@apis/queryClient.ts";
import {userInfo} from "@type/UserInfo.ts";
import useUploadActivityPhotoMutation from "@hooks/useUploadActivityPhotoMutation.ts";
import useDeleteActivityPhotoMutation from "@hooks/useDeleteActivityPhotoMutation.ts";
import DetailTitle from "@features/activity/DetailTitle.tsx";
import DetailBody from "@features/activity/DetailBody.tsx";
import DetailAttendees from "@features/activity/DetailAttendees.tsx";
import DetailComments from "@features/activity/DetailComments.tsx";
import DetailSidebar from "@features/activity/DetailSidebar.tsx";
import useTopPhotosQuery from "@features/profile/hooks/useTopPhotosQuery.ts";
import useUploadActivityMainPhotoMutation
    from "@hooks/useUploadActivityMainPhotoMutation.ts";

const ActivityDetailPage = () => {
    const theme = useTheme();
    const navigate = useNavigate();
    const {activityId} = useParams<{ activityId: string }>();
    const currentUserId = queryClient.getQueryData<userInfo>("userInfo")?.id;
    const {activityDetail, isGettingActivity} = useGetActivityQuery(activityId!);
    const {data, isPhotosLoading} = useTopPhotosQuery(activityId!);
    const uploadHook = useUploadActivityPhotoMutation(activityId!)
    const deleteHook = useDeleteActivityPhotoMutation(activityId!)
    const updateHook = useUploadActivityMainPhotoMutation(activityId!);

    if (isPhotosLoading || isGettingActivity) {
        return <LoadingComponent/>;
    }

    const {
        title,
        hostUser,
        imageUrl,
        description,
        attendees
    } = activityDetail!;

    const {
        photos,
        remainingCount
    } = data!;

    const isCurrentUser = currentUserId === hostUser?.id;

    if (!activityDetail) {
        navigate('/notfound');
        return null;
    }


    return (
        <Box>
            <DetailTitle title={title} hostUser={hostUser}/>

            <Box sx={{flexGrow: 1, marginBottom: theme.spacing(2)}}>
                <Grid container spacing={2}>

                    <Grid item md={7.5}>
                        <DetailBody title={title}
                                    imageUrl={imageUrl}
                                    description={description}
                                    isCurrentUser={isCurrentUser}
                                    uploadHook={updateHook}/>
                        <DetailAttendees attendees={attendees}/>
                        <PhotoGallery uploadHook={uploadHook}
                                      deleteHook={deleteHook}
                                      photos={photos}
                                      id={activityId}
                                      isCurrentUser={isCurrentUser}
                                      remainingCount={remainingCount}/>
                        <DetailComments activityId={activityId}/>
                    </Grid>

                    <Grid item xs={12} md={4.5} sx={{display: {xs: 'none', md: 'block'}}}>
                        <DetailSidebar activityId={activityId!}/>
                    </Grid>
                </Grid>
            </Box>
        </Box>
    );
}

export default ActivityDetailPage;