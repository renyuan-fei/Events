import React from "react";
import {ChatComments} from "@type/ChatComments.ts";
import {
    Avatar,
    Box,
    ListItem,
    ListItemAvatar,
    ListItemText,
    Typography
} from "@mui/material";
import {useNavigate} from "react-router";

interface CommentItemProps{
    comment: ChatComments;

}
const CommentItem:React.FC<CommentItemProps> = ({comment}) => {
    const navigate = useNavigate();

    const handleNavigateToUserProfile = (id: string) => {
        navigate(`/user/${id}`);
    }
    return (
        <Box>
            <ListItem alignItems='flex-start'>
                <ListItemAvatar>
                    <Avatar
                        sx={{ cursor: 'pointer' }}
                        onClick={() => handleNavigateToUserProfile(comment.userId)}
                        alt={comment.displayName}
                        src={comment.image}
                    />
                </ListItemAvatar>
                <ListItemText
                    primary={
                        <>
                            <Typography variant='h6' component='span' sx={{
                                fontWeight: 'bold',
                                fontSize: '16px',
                                display: 'inline',
                                whiteSpace: 'nowrap',
                                overflow: 'hidden',
                                textOverflow: 'ellipsis',
                                width: '30%',
                            }}>
                                {comment.displayName}
                            </Typography>
                            <Typography variant="body2" component="span" sx={{ color: 'text.secondary', marginLeft: '8px' }}>
                                &bull; {new Date(comment.created).toLocaleDateString('en-US', {
                                year: 'numeric',
                                month: 'short',
                                day: 'numeric',
                            })}
                            </Typography>
                        </>
                    }
                    secondary={
                        <Typography
                            variant='body2'
                            color='text.primary'
                            sx={{
                                display: 'block',
                                overflow: 'hidden',
                                textOverflow: 'ellipsis',
                                wordWrap: 'break-word',
                            }}
                        >
                            {comment.body}
                        </Typography>
                    }
                />
            </ListItem>
        </Box>
    );
};

export default CommentItem;