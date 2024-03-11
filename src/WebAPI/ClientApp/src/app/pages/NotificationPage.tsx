import React, {useEffect} from "react";
import {NotificationList} from "@features/notification/NotificationList.tsx";
import {RootState} from "@store/store.ts";
import {useSelector} from "react-redux";
import {Box, Typography} from "@mui/material";

export const NotificationPage:React.FC = () => {
    // const dispatch = useDispatch();
    const notifications = useSelector((state: RootState) => state.notification.notifications);

    useEffect(() => {
        // dispatch()
    }, []);

    return (
        <>
            <Box sx={{ textAlign: 'center', my: 4 }}>
                <Typography variant="h4" component="h1">
                    Notifications
                </Typography>
            </Box>
            <Box sx={{ display: 'flex', justifyContent: 'center' }}>
                <NotificationList notifications={notifications}/>
            </Box>
        </>
    );
};