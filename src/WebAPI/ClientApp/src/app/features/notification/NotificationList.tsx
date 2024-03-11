import {ListContainer} from "@features/follow/ListContainer.tsx";
import React from "react";
import {NotificationItem} from "@features/notification/NotificationItem.tsx";
import {Divider, Typography} from "@mui/material";
import {NotificationMessage} from "@type/NotificationMessage.ts";

interface NotificationListProps {
    notifications: NotificationMessage[];
}

export const NotificationList: React.FC<NotificationListProps> = ({notifications}) => {

    if (notifications.length === 0) {
        return (
            <Typography sx={{ margin: 2 }} variant="subtitle1">
                No any notifications yet
            </Typography>
        );
    }

    return (
        <ListContainer>
            {notifications?.map((notification: NotificationMessage, index) => (
                <React.Fragment key={notification.id}>
                    <NotificationItem notification={notification}/>
                    {index < notifications.length - 1 && <Divider variant="inset" component="li" />}
                </React.Fragment>
            ))}
        </ListContainer>
    );
};