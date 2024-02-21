import {useNavigate, useParams} from "react-router";
import Box from "@mui/material/Box";
import {Grid, useTheme} from "@mui/material";
import DetailComments from "@ui/DetailComments.tsx";
import PhotoGallery from "@ui/Custom/PhotoGallery.tsx";
import DetailSidebar from "@ui/DetailSidebar.tsx";
import DetailAttendees from "@ui/DetailAttendees.tsx";
import DetailBody from "@ui/DetailBody.tsx";
import DetailTitle from "@ui/DetailTitle.tsx";
import {useGetActivityQuery} from "@features/activity/hooks/useGetActivityQuery.ts";
import LoadingComponent from "@ui/LoadingComponent.tsx";
import useTopPhotosQuery from "@features/Profile/hooks/useTopPhotosQuery.ts";
import {queryClient} from "@apis/queryClient.ts";
import {userInfo} from "@type/userInfo.ts";
import useUploadActivityPhotoMutation from "@hooks/useUploadActivityPhotoMutation.ts";
import useDeleteActivityPhotoMutation from "@hooks/useDeleteActivityPhotoMutation.ts";

const ActivityDetailPage = () => {
    const theme = useTheme();
    const navigate = useNavigate();
    const {activityId} = useParams<{ activityId: string }>();
    const {activityDetail, isGettingActivity} = useGetActivityQuery(activityId!);
    const {data, isPhotosLoading} = useTopPhotosQuery(activityId!);
    const currentUserId = queryClient.getQueryData<userInfo>("userInfo")?.id;
    const uploadHook = useUploadActivityPhotoMutation(activityId!)
    const deleteHook = useDeleteActivityPhotoMutation(activityId!)

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
                                    description={description}/>
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
                        <DetailSidebar/>
                    </Grid>
                </Grid>
            </Box>
        </Box>
    );
}

export default ActivityDetailPage;