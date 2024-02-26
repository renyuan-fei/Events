import {Paper, Typography, Stack, useTheme} from '@mui/material';
import EventIcon from '@mui/icons-material/Event';
import LocationOnIcon from '@mui/icons-material/LocationOn';
import CheckCircleIcon from '@mui/icons-material/CheckCircle';
import useAddAttendeeMutation from "@features/activity/hooks/useAddAttendeeMutation.ts";
import React from "react";
import useRemoveAttendeeMutation
    from "@features/activity/hooks/useRemoveAttendeeMutation.ts";
import {LoadingButton} from '@mui/lab';
import {queryClient} from "@apis/queryClient.ts";
import {userInfo} from "@type/UserInfo.ts";
import {Activity} from "@type/Activity.ts";
import LoadingComponent from "@ui/LoadingComponent.tsx";

interface DetailSidebarProps {
    activityId: string;
}

const DetailSidebar: React.FC<DetailSidebarProps> = ({activityId}) => {
    const theme = useTheme();
    const currentUserId = queryClient.getQueryData<userInfo>("userInfo")?.id;
    const activity = queryClient.getQueryData<Activity>(['activity', activityId]);
    const isCurrentUserInActivity = activity?.attendees?.some((attendee) => attendee.userId === currentUserId) ?? false;
    const isHost = activity?.hostUser?.id === currentUserId;

    const {
        isAddingAttendee,
        addAttendee
    } = useAddAttendeeMutation(activityId);
    const {
        isRemovingAttendee,
        removeAttendee
    } = useRemoveAttendeeMutation(activityId);

    // TODO Refactor this component
    // TODO implement hook useAttendEvent
    // TODO according to different status of event and user, show different button
    const handleAddAttendeeClick = async () => {
        await addAttendee();
    };
    const handleRemoveAttendeeClick = async () => {
        await removeAttendee();
    };

    if (isAddingAttendee || isRemovingAttendee) {
        return <LoadingComponent/>
    }

    return (
        <Paper sx={{
            padding: theme.spacing(2),
            marginTop: theme.spacing(2),
            position: 'relative',
        }}>
            <Stack spacing={2}>
                <Stack direction='row' alignItems='flex-start' spacing={2}>
                    <CheckCircleIcon color='success'/>
                    <Typography component='div' variant='h6' sx={{
                        fontFamily: '"Graphik Meetup",' +
                            ' -apple-system',
                        fontSize: '16px',
                        fontWeight: theme.typography.fontWeightBold
                    }}>
                        Respond by Wednesday, 24 January 2024 12:00 pm
                    </Typography>
                </Stack>

                <Stack direction='row' alignItems='flex-start' spacing={2}>
                    <EventIcon color='action'/>
                    <Typography component='div' variant='body1' sx={{
                        ml: 1,
                        fontFamily: '"Graphik' +
                            ' Meetup", -apple-system',
                        fontSize: '16px',
                        fontWeight: theme.typography.fontWeightBold
                    }}>
                        Friday, 26 January 2024 at 12:00 pm to Friday, 26 January 2024 at
                        4:00 pm ACDT
                    </Typography>
                </Stack>

                <Stack direction='row' alignItems='flex-start' spacing={2}>
                    <LocationOnIcon color='action'/>
                    <Typography component='div' variant='body1' sx={{
                        ml: 1,
                        fontFamily: '"Graphik' +
                            ' Meetup", -apple-system',
                        fontSize: '16px',
                        fontWeight: theme.typography.fontWeightBold
                    }}>
                        Eastwood Community Centre 95 Glen Osmond Rd - Eastwood, SA
                    </Typography>
                </Stack>

                <Stack direction='row'
                       alignItems='flex-start'
                       spacing={2}
                       justifyContent={"center"}>

                    {isHost ?
                        <LoadingButton
                            loading={isAddingAttendee}
                            variant={"outlined"}
                            color={"primary"}>
                            Cancel
                        </LoadingButton> :
                        isCurrentUserInActivity ?
                            <LoadingButton
                                loading={isRemovingAttendee}
                                variant={"outlined"}
                                color={"primary"}
                                onClick={handleRemoveAttendeeClick}>
                                Leave
                            </LoadingButton> :
                            <LoadingButton
                                loading={isAddingAttendee}
                                variant={"contained"}
                                color={"secondary"}
                                onClick={handleAddAttendeeClick}>
                                Attend
                            </LoadingButton>
                    }
                </Stack>
            </Stack>
        </Paper>
    )
        ;
}

export default DetailSidebar;
