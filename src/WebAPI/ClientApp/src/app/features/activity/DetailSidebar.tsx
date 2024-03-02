import {Paper, Typography, Stack, useTheme} from '@mui/material';
import EventIcon from '@mui/icons-material/Event';
import LocationOnIcon from '@mui/icons-material/LocationOn';
import CheckCircleIcon from '@mui/icons-material/CheckCircle';
import CancelIcon from '@mui/icons-material/Cancel';
import useAddAttendeeMutation from "@features/activity/hooks/useAddAttendeeMutation.ts";
import React from "react";
import useRemoveAttendeeMutation
    from "@features/activity/hooks/useRemoveAttendeeMutation.ts";
import {LoadingButton} from '@mui/lab';
import {queryClient} from "@apis/queryClient.ts";
import {userInfo} from "@type/UserInfo.ts";
import {Activity} from "@type/Activity.ts";
import LoadingComponent from "@ui/LoadingComponent.tsx";
import useFormatToLocalTimezone from "../../utils/useFormatToLocalTimezone.ts";
import useCancelActivity from "@features/activity/hooks/useCancelActivity.ts";
import {useNavigate} from "react-router";

interface DetailSidebarProps {
    activityId: string;
}

const DetailSidebar: React.FC<DetailSidebarProps> = ({activityId}) => {
    const theme = useTheme();
    const navigate = useNavigate();
    const currentUserId = queryClient.getQueryData<userInfo>("userInfo")?.id;
    const activity = queryClient.getQueryData<Activity>(['activity', activityId]);
    const isCurrentUserInActivity = activity?.attendees?.some((attendee) => attendee.userId === currentUserId) ?? false;
    const isCanceled = activity?.isCancelled;
    const isHost = activity?.hostUser?.id === currentUserId;

    const {
        isCanceling,
        cancelActivity
    } = useCancelActivity(activityId);

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

    const handelNavigateToManagePage = () => {
        navigate(`/activity/new/${activityId}`);
    }
    const handleCancelActivityClick = async () => {
        await cancelActivity();
    }
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
                    {isCanceled ? <CancelIcon color={'error'}/> :
                        <CheckCircleIcon color='success'/>}
                    <Typography component='div' variant='h6' sx={{
                        fontFamily: '"Graphik Meetup",' +
                            ' -apple-system',
                        fontSize: '16px',
                        fontWeight: theme.typography.fontWeightBold
                    }}>
                        {
                            isCanceled ? 'This event has been canceled' : 'This event is active'
                        }
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
                        {useFormatToLocalTimezone(activity?.date!)}
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
                        {activity?.venue} - {} {activity?.city}
                    </Typography>
                </Stack>

                <Stack direction='row'
                       alignItems='flex-start'
                       spacing={2}
                       justifyContent={"center"}>
                    {isHost ?
                        isCanceled ?
                            <LoadingButton
                                loading={isAddingAttendee}
                                variant={"contained"}
                                color={"primary"}>
                                Reactivate
                            </LoadingButton> :
                            <>
                                <LoadingButton
                                    loading={isCanceling}
                                    variant={"contained"}
                                    color={"primary"}
                                    onClick={handelNavigateToManagePage}>
                                    Manage your activity
                                </LoadingButton>
                                <LoadingButton
                                    loading={isCanceling}
                                    variant={"outlined"}
                                    color={"secondary"}
                                    onClick={handleCancelActivityClick}>
                                    Cancel
                                </LoadingButton>
                            </> :
                        isCurrentUserInActivity ?
                            <LoadingButton
                                loading={isRemovingAttendee}
                                disabled={isCanceled}
                                variant={"outlined"}
                                color={"primary"}
                                onClick={handleRemoveAttendeeClick}>
                                Leave
                            </LoadingButton> :
                            <LoadingButton
                                loading={isAddingAttendee}
                                disabled={isCanceled}
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
