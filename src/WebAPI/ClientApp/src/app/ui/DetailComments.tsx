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

const mockComments = [
    {
        id: '1',
        name: 'Fiona',
        avatar: '/path/to/fiona-avatar.jpg',
        date: '9 days ago',
        content: 'Hello 👋quick question? I love live music however I’m a vegetarian ... I can bring some lunch & buy my drinks ? Thnx for reply 🍻 cheers',
    }
    // ... More comments
];

export default function DetailComments() {
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
                {mockComments.map((comment, index) => (
                    <Box key={comment.id}>
                        <ListItem alignItems="flex-start">
                            <ListItemAvatar>
                                <Avatar alt={comment.name} src={comment.avatar}/>
                            </ListItemAvatar>
                            <ListItemText
                                primary={comment.name}
                                secondary={
                                    <Typography
                                        variant="body2"
                                        color="text.primary"
                                    >
                                        {comment.content}
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
                        {index < mockComments.length - 1 &&
                            <Divider variant="inset" component="li"/>}
                    </Box>
                ))}
            </List>
        </Box>
    )
        ;
}
