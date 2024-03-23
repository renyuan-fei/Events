import React from "react";
import {useParams} from "react-router";
import {Box, Typography} from "@mui/material";
import AttendeeList from "@features/activity/AttendeeList.tsx";

const AttendeePage:React.FC = () => {
    const {activityId} = useParams<{ activityId: string }>();

    return (
        <>
            <Box sx={{ textAlign: 'center', my: 4 }}>
                <Typography variant="h4" component="h1">
                    Attendee
                </Typography>
            </Box>
            <Box sx={{ display: 'flex', justifyContent: 'center' }}>
                <AttendeeList activityId={activityId!}/>
            </Box>
        </>
    );
};

export default AttendeePage;