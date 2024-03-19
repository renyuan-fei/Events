import React from "react";
import { Box, Button, Paper, Typography, useTheme } from "@mui/material";
import { useNavigate } from "react-router";
import DetailAttendeeList from "./DetailAttendeeList.tsx";
import {queryClient} from "@apis/queryClient.ts";
import {Activity} from "@type/Activity.ts"; // 导入 AttendeeList 组件

interface DetailAttendeesProps {
    id: string;
}

const DetailAttendees: React.FC<DetailAttendeesProps> = ({ id }) => {
    const theme = useTheme();
    const attendees = queryClient.getQueryData<Activity>(['activity',id])?.attendees;
    const navigate = useNavigate();

    const handleSeeAllClick = () => {
        navigate(`/activity/${id}/attendees`);
    };

    return (
        <Box sx={{ marginTop: theme.spacing(2), marginBottom: theme.spacing(3) }}>
            <Box sx={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', marginBottom: theme.spacing(1) }}>
                <Typography component="div" variant="h2" sx={{
                    fontSize: '20px',
                    fontWeight: 'bold',
                    fontFamily: theme.typography.fontFamily,
                }}>
                    Attendees ({attendees!.length})
                </Typography>
                <Button
                    variant="text"
                    onClick={handleSeeAllClick}
                    sx={{
                        color: '#00798A',
                        fontFamily: theme.typography.fontFamily,
                        fontSize: '16px',
                        textTransform: 'none',
                        textDecoration: 'none',
                    }}
                >
                    See all
                </Button>
            </Box>
            <Paper sx={{ flexGrow: 1, padding: theme.spacing(3) }}>
                <DetailAttendeeList attendees={attendees!} />
            </Paper>
        </Box>
    );
};

export default DetailAttendees;
