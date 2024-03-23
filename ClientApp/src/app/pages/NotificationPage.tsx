import {NotificationList} from "@features/notification/NotificationList.tsx";
import {RootState} from "@store/store.ts";
import {useDispatch, useSelector} from "react-redux";
import {Box, CircularProgress, Typography} from "@mui/material";
import React, {useEffect} from "react";
import {loadPaginatedNotifications} from "@config/NotificationHubConnection.ts";
import {resetNotifications} from "@features/notification/NotificationSlice.ts";
import useInfiniteScroll from "@hooks/useInfiniteScroll.ts";

export const NotificationPage:React.FC = () => {
    const dispatch = useDispatch();
    const {notifications,isConnection,hasNextPage,isLoading} = useSelector((state: RootState) => state.notification);


    useEffect(() => {
        if (isConnection) {
            dispatch(loadPaginatedNotifications())
        }
        return () => {
            dispatch(resetNotifications())
        }
    },[dispatch,isConnection])

    const loadMoreRef = useInfiniteScroll(() => {
        if (!isLoading) {
            dispatch(loadPaginatedNotifications());
        }
    }, hasNextPage, isLoading);

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
            <Box sx={{ display: 'flex', justifyContent: 'center', my: 4 }} ref={loadMoreRef}>
                {isLoading && <CircularProgress />}
                {!hasNextPage && !isLoading && (
                    <Typography variant="h6">No More</Typography>
                )}
            </Box>
        </>
    );
};