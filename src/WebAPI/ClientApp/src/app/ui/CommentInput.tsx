import React, { useState } from 'react';
import { TextField, Avatar, InputAdornment, IconButton, Box } from '@mui/material';
import SendIcon from '@mui/icons-material/Send';
import {useAppDispatch} from "@store/store.ts";
import {sendMessage} from "@config/HubConnection.ts";

const CommentInput: React.FC = () => {
    const [comment, setComment] = useState<string>('');
    const dispatch = useAppDispatch()

    const handleCommentChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        setComment(event.target.value);
    };

    const handleSendComment = () => {
        console.log(comment);
        dispatch(sendMessage(comment));
        setComment(''); // Clear the input after sending
    };

    return (
        <Box sx={{ display: 'flex', alignItems: 'center', gap: 1 }}>
            <Avatar sx={{ bgcolor: 'salmon' }}>U</Avatar>
            <TextField
                fullWidth
                value={comment}
                onChange={handleCommentChange}
                placeholder="Add a comment..."
                variant="outlined"
                sx={{ ml: 1, flex: 1 }}
                InputProps={{
                    endAdornment: (
                        <InputAdornment position="end">
                            <IconButton onClick={handleSendComment} edge="end">
                                <SendIcon />
                            </IconButton>
                        </InputAdornment>
                    ),
                }}
            />
        </Box>
    );
};

export default CommentInput;
