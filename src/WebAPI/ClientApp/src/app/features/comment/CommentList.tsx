import React, {useEffect} from "react";
import {Box, Divider, List, Typography} from "@mui/material";
import CommentItem from "@features/comment/CommentItem.tsx";
import {
    loadPaginatedComments,
    startConnection,
    stopConnection
} from "@config/ChatHubConnection.ts";
import {useDispatch, useSelector} from "react-redux";
import {RootState} from "@store/store.ts";
import {LoadingButton} from "@mui/lab";

interface CommentListProps {
    activityId: string;
}

const CommentList: React.FC<CommentListProps> = ({activityId}) => {
    const {comments, hasNextPage} = useSelector((state: RootState) => state.comment);
    const dispatch = useDispatch();

    useEffect(() => {
        if (activityId) {
            dispatch(startConnection(activityId))
        }
        return () => {
            dispatch(stopConnection())
        }
    }, [dispatch, activityId]);

    const handleLoadMoreComments = () => {
        dispatch(loadPaginatedComments())
    }

    return (
        <>
            <List>
                {comments.map((comment, index) => (
                    <React.Fragment key={comment.id}>
                        <CommentItem comment={comment}/>
                        {index !== comments.length - 1 && <Divider variant='inset'/>}
                    </React.Fragment>
                ))}
            </List>
            <Box sx={{ display: 'flex', justifyContent: 'center', my: 4 }}>
                <Typography variant="h4" component="h1">
                    {hasNextPage? <LoadingButton onClick={handleLoadMoreComments}>Load More</LoadingButton> : 'No More'}
                </Typography>
            </Box>
        </>
    );
};

export default CommentList;