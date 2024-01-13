import Card from '@mui/material/Card';
import CardMedia from '@mui/material/CardMedia';
import CardContent from '@mui/material/CardContent';
import Typography from '@mui/material/Typography';
import EventIcon from '@mui/icons-material/Event';
import PeopleIcon from '@mui/icons-material/People';
import Box from '@mui/material/Box';
import {CardActionArea, useTheme} from "@mui/material";

// Correct the function signature to accept a prop of type Item
export default function ActivityCard({
                                         title,
                                         imageUrl,
                                         date,
                                         category,
                                         goingCount,
                                         hostUser
                                     }: Item) {
    const theme = useTheme();

    return (
        <Card sx={{
            maxWidth: {xs: '100%', sm: 345},
            margin: theme.spacing(2),
            borderRadius: 5,
            border: 'none'
        }}
              variant="outlined">
            <CardActionArea>
                <CardMedia
                    component="img"
                    image={imageUrl}
                    alt="Group picture"
                    sx={{borderRadius: 5, height: 153}}
                />
                <CardContent>
                    <Box>
                        <Typography
                            gutterBottom
                            variant="h6"
                            component="div"
                            sx={{
                                wordWrap: 'break-word',
                                overflow: 'hidden',
                                fontWeight: 'fontWeightBold',
                                lineHeight: '1.1',
                                '&:hover': {}}}>
                            {title}
                        </Typography>
                    </Box>

                    <Box display="flex" alignItems="center" mb={1}>
                        <Typography variant="caption" sx={{
                            fontWeight: 600,
                            fontSize: 14,
                            color: theme.palette.text.secondary,
                            mr: 1
                        }}>Hosted by: {hostUser.username}</Typography>
                    </Box>

                    <Box display="flex" alignItems="center" mb={1}>
                        <EventIcon color="action" sx={{marginRight: '5px'}}/>
                        <Typography variant="caption">{date}</Typography>
                    </Box>

                    <Box display="flex" alignItems="center">
                        <PeopleIcon color="action" sx={{marginRight: '5px'}}/>
                        <Typography variant="caption">{goingCount} going</Typography>
                        <Box flexGrow={1}/>
                        <Typography variant="caption" component="span" sx={{
                            backgroundColor: 'lightgrey',
                            borderRadius: '4px',
                            padding: '2px 5px'
                        }}>
                            {category}
                        </Typography>
                    </Box>

                </CardContent>
            </CardActionArea>
        </Card>
    );
}
