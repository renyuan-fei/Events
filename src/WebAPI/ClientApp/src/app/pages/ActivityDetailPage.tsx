import {useNavigate, useParams} from "react-router";
import Box from "@mui/material/Box";
import {DetailTitle} from "@ui/DetailTitle.tsx";
import {DetailBody} from "@ui/DetailBody.tsx";
import {DetailAttendees} from "@ui/DetailAttendees.tsx";
import {Grid, useTheme} from "@mui/material";
import DetailComments from "@ui/DetailComments.tsx";
import {DetailSidebar} from "@ui/DetailSidebar.tsx";
import {useGetActivityQuery} from "@apis/Activities.ts";
import {useEffect} from "react";
import {Photo} from "@type/Photo.ts";
import CustomImageList from "@ui/Custom/CustomImageList.tsx";

export function ActivityDetailPage() {
    const theme = useTheme();
    const navigate = useNavigate();

    const {activityId} = useParams<{ activityId: string }>();

    const {data, isLoading, isError} = useGetActivityQuery(activityId as string);

    useEffect(() => {
        if (!activityId) {
            navigate('/notfound');
        }
    }, [activityId, isError, navigate]);

    if (isLoading) {
        return <div>Loading...</div>;
    }

    if (!data) {
        // Render some UI or redirect if the activity is null
        navigate('/notfound');
        return null;
    }


    return (
        <Box>
            <DetailTitle title={data.title} hostUser={data.hostUser}/>

            <Box sx={{flexGrow: 1, marginBottom: theme.spacing(2)}}>
                <Grid container spacing={2}>
                    <Grid item md={7.5}>
                        <DetailBody title={data.title} imageUrl={data.imageUrl} description={data.description}/>
                        <DetailAttendees attendees={data.attendees}/>
                        <CustomImageList images={new Array<Photo>} Id={activityId} isUser={true}/>
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