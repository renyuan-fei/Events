import {
    Avatar,
    Box,
    Typography,
    List,
    ListItem,
    ListItemAvatar,
    ListItemText,
    Divider,
    useTheme, Paper
} from '@mui/material';
import CommentInput from "@ui/CommentInput.tsx";
import {useDispatch, useSelector} from "react-redux";
import {RootState} from "@store/store.ts";
import React, {useEffect} from "react";
import {startConnection, stopConnection} from "@config/HubConnection.ts";
import {useNavigate} from "react-router";
import {queryClient} from "@apis/queryClient.ts";
import {userInfo} from "@type/UserInfo.ts";

interface DetailCommentsProps {
    activityId: string | undefined;
}

const DetailComments: React.FC<DetailCommentsProps> = ({activityId}) => {
    const navigate = useNavigate();
    const comments = useSelector((state: RootState) => state.comment.comments);
    const dispatch = useDispatch();
    const image = queryClient.getQueryData<userInfo>("userInfo")?.image;

    // TODO implement pagination
    // TODO delete comment (only for comment owner)
    const handleNavigateToUserProfile = (id: string) => {
        navigate(`/user/${id}`);
    }

    useEffect(() => {
        if (activityId) {
            dispatch(startConnection(activityId))
        }
        return () => {
            dispatch(stopConnection())
        }
    }, [dispatch, activityId]);

    const theme = useTheme();

    return (
        <Paper sx={{width: '100%', padding: theme.spacing(2)}}>
            <Typography component='div' variant='h2' sx={{
                fontSize: '20px',
                fontWeight: theme.typography.fontWeightBold,
                fontFamily: '"Graphik Meetup", -apple-system, sans-serif',
            }}>
                Comments
            </Typography>

            <CommentInput image={image!}/>

            <List>
                {comments.map((comment, index) => (
                    <Box key={comment.id}>
                        <ListItem alignItems='flex-start'>
                            <ListItemAvatar>
                                <Avatar
                                    sx={{
                                        cursor: 'pointer'
                                    }}
                                    onClick={() => handleNavigateToUserProfile(comment.userId)}
                                    alt={comment.displayName}
                                    src={comment.image}/>
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
                                            &bull; {new Date(comment.createdAt).toLocaleDateString('en-US', {
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
                                            display: 'block', // Or 'inline-block' if needed
                                            overflow: 'hidden',
                                            textOverflow: 'ellipsis',
                                            wordWrap: 'break-word', // Ensures that long words will break and wrap onto the next line
                                        }}

                                    >
                                        {comment.body}
                                    </Typography>
                                }
                            />

                        </ListItem>
                        {index < comments.length - 1 &&
                            <Divider variant='inset' component='li'/>}
                    </Box>
                ))}
            </List>
        </Paper>
    );
}

export default DetailComments;
