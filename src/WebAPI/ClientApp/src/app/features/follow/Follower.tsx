import {
    Avatar,
    ListItem,
    ListItemAvatar,
    ListItemText,
    Typography
} from "@mui/material";
import React from "react";
import {UserDetailBase} from "@type/UserDetailBase.ts";

export const Follower: React.FC<UserDetailBase> = ({
                                                       image,
                                                       displayName,
                                                       bio
                                                   }) => {



    return (
        <ListItem alignItems='flex-start' sx={{
            cursor: 'pointer',
            '&:hover': {
                backgroundColor: 'rgba(0, 0, 0, 0.04)'
            }
        }}>
            <ListItemAvatar>
                <Avatar alt='Remy Sharp' src={image}/>
            </ListItemAvatar>
            <ListItemText
                primary={displayName}
                secondary={
                    <>
                        <Typography
                            sx={{display: 'inline'}}
                            component='span'
                            variant='body2'
                            color='text.primary'
                        >
                            {bio}
                        </Typography>
                    </>
                }
            />
        </ListItem>
    );
};