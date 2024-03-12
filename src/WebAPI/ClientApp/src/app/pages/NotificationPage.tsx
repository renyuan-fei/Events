import {NotificationList} from "@features/notification/NotificationList.tsx";
import {RootState} from "@store/store.ts";
import {useDispatch, useSelector} from "react-redux";
import {Box, Typography} from "@mui/material";
import React, {useEffect} from "react";
import {loadPaginatedNotifications} from "@config/NotificationHubConnection.ts";
import {LoadingButton} from "@mui/lab";
import {reset} from "@features/notification/NotificationSlice.ts";

export const NotificationPage:React.FC = () => {
    const dispatch = useDispatch();
    const {notifications,isConnection,hasNextPage} = useSelector((state: RootState) => state.notification);


    useEffect(() => {
        if (isConnection) {
            dispatch(loadPaginatedNotifications())
        }
        return () => {
            dispatch(reset())
        }
    },[dispatch,isConnection])

    const handleLoadMoreNotifications = () => {
        dispatch(loadPaginatedNotifications())
    }

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
            <Box sx={{ display: 'flex', justifyContent: 'center', my: 4 }}>
                <Typography variant="h4" component="h1">
                    {hasNextPage? <LoadingButton onClick={handleLoadMoreNotifications}>Load More</LoadingButton> : 'No More'}
                </Typography>
            </Box>
        </>
    );
};