import React from "react";
import {NotificationMessage} from "@type/NotificationMessage.ts";


interface NotificationItemProps {
    notification: NotificationMessage
}
export const NotificationItem:React.FC<NotificationItemProps> = ({notification}) => {
    console.log(notification)
    return (
        <></>
    );
};