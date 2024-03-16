import Box from "@mui/material/Box";
import Typography from "@mui/material/Typography";
import {Avatar, Grid, useTheme} from "@mui/material";
import Divider from "@mui/material/Divider";
import {queryClient} from "@apis/queryClient.ts";
import {Activity} from "@type/Activity.ts";
import React from "react";


interface DetailTitleProps {
    title: string;
    activityId: string;
}

const DetailTitle:React.FC<DetailTitleProps> = ({title,activityId}) => {
    const activity = queryClient.getQueryData<Activity>(['activity', activityId]);
    const hostUser = activity?.attendees.find((attendee) => attendee.isHost);
    const hostUserName = hostUser!.displayName;
    const hostUserAvatar = hostUser!.image;

    const theme = useTheme();
    return (
        <Box>
            <Divider/>
            <Box component="div" sx={{
                padding: theme.spacing(2),
            }}>
                <Grid container spacing={2} alignItems="center">
                    <Grid item xs={12}>
                        <Typography variant="h1" component="h1" sx={{
                            fontWeight: 'bold',
                            fontSize: '30px',
                            lineHeight: '1.1',
                            mb: 2,
                            wordWrap: 'break-word'
                        }}>
                            {title}
                        </Typography>
                    </Grid>
                    <Grid item>
                        <Avatar src={hostUserAvatar} />
                    </Grid>
                    <Grid item>
                        <Typography component="div" sx={{}}>
                            Hosted By
                        </Typography>
                        <Typography component="div" sx={{fontWeight: 'bold'}}>
                            {hostUserName}
                        </Typography>
                    </Grid>
                </Grid>
            </Box>
            <Divider/>
        </Box>
    );
}

export default DetailTitle;