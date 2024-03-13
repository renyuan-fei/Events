import React from "react";
import {NotificationMessage} from "@type/NotificationMessage.ts";
import {Badge, Box, ListItem, ListItemText, Typography, useTheme} from "@mui/material";
import useFormatToLocalTimezone from "@utils/useFormatToLocalTimezone.ts";
import IconButton from "@mui/material/IconButton";
import MarkAsReadIcon from '@mui/icons-material/Done';
import useSplitCamelCase from "@utils/useSplitCamelCase.ts";
import {useDispatch} from "react-redux";
import {updateNotificationStatus} from "@config/NotificationHubConnection.ts";
import {useNavigate} from "react-router";

interface NotificationItemProps {
    notification: NotificationMessage
}

export const NotificationItem: React.FC<NotificationItemProps> = ({notification}) => {
    const navigate = useNavigate();
    const theme = useTheme();

    const {
        id,
        content,
        type,
        created,
        status,
        relatedId
    } = notification;

    const dispatch = useDispatch();
    const formattedTime = useFormatToLocalTimezone(new Date(created));
    const splitType = useSplitCamelCase(type);

    const navigateToRelated = () => {
        const activityTypes = ['ActivityCreated', 'ActivityCanceled', 'ActivityJoined', 'ActivityUpdated'];
        const dest = activityTypes.includes(type) ? 'activity' : 'user';

        navigate(`/${dest}/${relatedId}`)
    }

    const handleMarkAsRead = () => {
        dispatch(updateNotificationStatus(id))
    }

    return (
        <ListItem alignItems='flex-start'
                  sx={{
                      marginY: theme.spacing(1),
                  }}
                  secondaryAction={
                      <IconButton edge='end'
                                  color={'secondary'}
                                  onClick={handleMarkAsRead}
                                  disabled={status}>
                          <MarkAsReadIcon/>
                      </IconButton>
                  }>
            <ListItemText
                primary={
                    <Badge variant='dot'
                           badgeContent={status ? 0 : 1}
                           onClick={handleMarkAsRead}
                           color={'secondary'}
                           anchorOrigin={{
                               vertical: 'top',
                               horizontal: 'left',
                           }}>
                        <Box sx={{
                            display: 'flex',
                            justifyContent: 'space-between',
                            alignItems: 'center'
                        }}>
                            <Typography variant='h6'
                                        onClick={navigateToRelated}
                                        sx={{
                                            fontWeight: 'bold', marginRight: 2,
                                            '&:hover': {color: theme.palette.primary.main},
                                            cursor: 'pointer'
                                        }}>
                                {splitType}
                            </Typography>
                            <Typography
                                variant='body2'
                                color='text.secondary'
                                sx={{flexShrink: 0}}
                            >
                                {formattedTime}
                            </Typography>
                        </Box>
                    </Badge>

                }
                secondary={
                    <Typography variant='body1' color='text.primary'>
                        {content}
                    </Typography>
                }
            />
        </ListItem>
    );
};
