import {useParams} from "react-router";
import Box from "@mui/material/Box";
import {DetailTitle} from "@ui/DetailTitle.tsx";
import {DetailBody} from "@ui/DetailBody.tsx";
import {DetailAttendees} from "@ui/DetailAttendees.tsx";
import {Grid, useTheme} from "@mui/material";

export function ActivityDetailPage() {
    const theme = useTheme();

    // @ts-ignore
    const {activityId} = useParams<{ activityId: string }>();

    const title = "Australia Day BBQ & Live Band at Eastwood"
    const hostUser = {
        username: "Leanne I.",
        id: "test",
    }

    return (
        <Box>
            <DetailTitle title={title} hostUser={hostUser}/>

            <Box sx={{flexGrow: 1, marginBottom: theme.spacing(2)}}>
                <Grid container spacing={2}>
                    <Grid item md={7.5}>
                        <DetailBody/>
                        <DetailAttendees/>
                    </Grid>

                    <Grid item xs={12} md={4.5} sx={{display: {xs: 'none', md: 'block'}}}>

                    </Grid>
                </Grid>
            </Box>
        </Box>
    );

}