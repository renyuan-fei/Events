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
    useTheme
} from '@mui/material';
import FavoriteBorderIcon from '@mui/icons-material/FavoriteBorder';
import MoreHorizIcon from '@mui/icons-material/MoreHoriz';
import CommentInput from "@ui/CommentInput.tsx";
import {useDispatch, useSelector} from "react-redux";
import {RootState} from "@store/store.ts";
import {useEffect} from "react";
import {startConnection, stopConnection} from "@config/HubConnection.ts";


export default function DetailComments({activityId} : { activityId : string | undefined}) {
    const comments = useSelector((state : RootState) => state.comment.comments);
    const dispatch = useDispatch();

    useEffect(() => {
        if (activityId)
        {
            dispatch(startConnection(activityId))
        }
        return () => {
            dispatch(stopConnection())
        }
    }, [activityId]);

    const theme = useTheme();

    return (
        <Box sx={{width: '100%', bgcolor: 'background.paper'}}>
            <Typography component="div" variant="h2" sx={{
                fontSize: '20px',
                fontWeight: theme.typography.fontWeightBold,
                fontFamily: '"Graphik Meetup", -apple-system, sans-serif',
            }}>
                Comments
            </Typography>

            <CommentInput/>

            <List>
                {comments.map((comment, index) => (
                    <Box key={comment.id}>
                        <ListItem alignItems="flex-start">
                            <ListItemAvatar>
                                <Avatar alt={comment.userName} src={comment.image}/>
                            </ListItemAvatar>
                            <ListItemText
                                primary={comment.userName}
                                secondary={
                                    <Typography
                                        variant="body2"
                                        color="text.primary"
                                    >
                                        {comment.body}
                                    </Typography>
                                }
                            />
                            <IconButton aria-label="like">
                                <FavoriteBorderIcon/>
                            </IconButton>
                            <IconButton aria-label="more">
                                <MoreHorizIcon/>
                            </IconButton>
                        </ListItem>
                        {index < comments.length - 1 &&
                            <Divider variant="inset" component="li"/>}
                    </Box>
                ))}
            </List>
        </Box>
    )
        ;
}
