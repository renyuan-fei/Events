import React from "react";
import {NotificationMessage} from "@type/NotificationMessage.ts";
import {Badge, ListItem, ListItemText, Typography} from "@mui/material";


interface NotificationItemProps {
    notification: NotificationMessage
}
export const NotificationItem:React.FC<NotificationItemProps> = ({notification}) => {
    const {
        content,
        type,
        status
    } = notification;

    return (
        <ListItem alignItems='flex-start'>
            <Badge variant="dot" badgeContent={status ? 0 : 1} color={'secondary'} anchorOrigin={{
                vertical: 'top',
                horizontal: 'left',
            }}>
                <ListItemText
                primary={<Typography variant='h6' component='span' sx={{
                    fontWeight: 'bold',
                    display: 'block', // 或者 'inline-block' 都可以，取决于布局需求
                    whiteSpace: 'nowrap',
                    overflow: 'hidden',
                    textOverflow: 'ellipsis',
                    width: '100%', // 可以调整为实际需要的宽度
                    marginTop: 0.5
                }}>{type}</Typography>}
                secondary={
                    <>
                        <Typography
                            component='span'
                            variant='body1'
                            color='text.primary'
                            sx={{
                                display: 'inline',
                                overflow: 'hidden',
                                textOverflow: 'ellipsis',
                                wordWrap: 'break-word', // Ensures that long words will break and wrap onto the next line
                            }}
                        >
                            {content}
                        </Typography>
                    </>
                }
            /></Badge>
        </ListItem>
    );
};