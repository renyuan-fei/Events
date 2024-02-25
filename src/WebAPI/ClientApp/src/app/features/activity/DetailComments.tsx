import {
    Avatar,
    Box,
    IconButton,
    Typography,
    List,
    ListItem,
    ListItemAvatar,
    ListItemText,
    Divider,
    useTheme, Paper
} from '@mui/material';
import FavoriteBorderIcon from '@mui/icons-material/FavoriteBorder';
import MoreHorizIcon from '@mui/icons-material/MoreHoriz';
import CommentInput from "@ui/CommentInput.tsx";
import {useDispatch, useSelector} from "react-redux";
import {RootState} from "@store/store.ts";
import {useEffect} from "react";
import {startConnection, stopConnection} from "@config/HubConnection.ts";
import {useNavigate} from "react-router";
import {queryClient} from "@apis/queryClient.ts";
import {userInfo} from "@type/UserInfo.ts";


const DetailComments = ({activityId}: { activityId: string | undefined }) => {
    const navigate = useNavigate();
    const comments = useSelector((state: RootState) => state.comment.comments);
    const dispatch = useDispatch();
    const image = queryClient.getQueryData<userInfo>("userInfo")?.image;

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
    }, [dispatch,activityId]);

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
                                primary={comment.displayName}
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
                            <IconButton aria-label='like'>
                                <FavoriteBorderIcon/>
                            </IconButton>
                            <IconButton aria-label='more'>
                                <MoreHorizIcon/>
                            </IconButton>
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
