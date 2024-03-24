import React, { useState } from 'react';
import { TextField, Avatar, InputAdornment, IconButton, Box } from '@mui/material';
import SendIcon from '@mui/icons-material/Send';
import {RootState, useAppDispatch} from "@store/store.ts";
import {sendMessage} from "@config/ChatHubConnection.ts";
import {useSelector} from "react-redux";
import {queryClient} from "@apis/queryClient.ts";
import {userInfo} from "@type/UserInfo.ts";
import {Activity} from "@type/Activity.ts";
import Button from "@mui/material/Button";
import useAddAttendeeMutation from "@features/activity/hooks/useAddAttendeeMutation.ts";
import {LoadingButton} from "@mui/lab";
import {setLoginForm} from "@features/commonSlice.ts";

interface CommentInputProps {
    activityId: string;
}

const CommentInput:React.FC<CommentInputProps> = ({activityId}) => {
    const image = queryClient.getQueryData<userInfo>("userInfo")?.image;
    const [comment, setComment] = useState<string>('');
    const isLogin = useSelector((state: RootState) => state.user.isLogin);
    const currentUserId = queryClient.getQueryData<userInfo>("userInfo")?.id;
    const activity = queryClient.getQueryData<Activity>(['activity', activityId]);
    const isCurrentUserInActivity = activity?.attendees?.some((attendee) => attendee.userId === currentUserId) ?? false;
    const dispatch = useAppDispatch()

    const handleCommentChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        setComment(event.target.value);
    };

    const handleSendComment = () => {
        if (comment.trim() !== '') {
            dispatch(sendMessage(comment));
            setComment(''); // Clear the input after sending
        }
    };

    const handleOpenLogin = () => {
        dispatch(setLoginForm(true));
    }

    const {
        isAddingAttendee,
        addAttendee
    } = useAddAttendeeMutation(activityId);

    const handleAddAttendeeClick = async () => {
        await addAttendee();
    };
    const handleKeyDown = (event: React.KeyboardEvent<HTMLDivElement>) => {
        if (event.key === 'Enter') {
            handleSendComment();
            event.preventDefault();
        }
    };

    return (
        <Box sx={{ display: 'flex', alignItems: 'center', gap: 1 }}>
            <Avatar sx={{ bgcolor: 'salmon' }} src={image} />
            <TextField
                fullWidth
                value={comment}
                onKeyDown={handleKeyDown}
                onChange={handleCommentChange}
                placeholder={!isLogin ? "Login to comment" : (!isCurrentUserInActivity ? "Join the activity to comment" : "Add a comment...")}
                variant="outlined"
                sx={{ ml: 1, flex: 1 }}
                disabled={!isLogin || !isCurrentUserInActivity}
                InputProps={{
                    endAdornment: (
                        <InputAdornment position="end">
                            {!isLogin ? (
                                <Button color="primary" size="small" onClick={handleOpenLogin}>
                                    Login
                                </Button>
                            ) : (!isCurrentUserInActivity ? (
                                <LoadingButton loading={isAddingAttendee} onClick={handleAddAttendeeClick} color="secondary" size="small">
                                    Attend
                                </LoadingButton>
                            ) : <IconButton
                                onClick={handleSendComment}
                                color="primary"
                                size="small"
                                disabled={!isLogin || !isCurrentUserInActivity}>
                                <SendIcon />
                            </IconButton>)}
                        </InputAdornment>
                    ),
                }}
            />
        </Box>
    );
};

export default CommentInput;
