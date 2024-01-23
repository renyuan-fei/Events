import {Grid, useTheme} from "@mui/material";
import Box from "@mui/material/Box";
import Typography from "@mui/material/Typography";
import EventIcon from "@mui/icons-material/Event";
import PeopleIcon from "@mui/icons-material/People";
import Divider from "@mui/material/Divider";
import {  } from 'react-router-dom';
import {useNavigate} from "react-router";

function formatToLocalTimezone(dateString: string): string {
    const timeZone = Intl.DateTimeFormat().resolvedOptions().timeZone;
    const date = new Date(dateString + 'Z'); // Parse the date string as UTC

    // Set up the options for formatting
    const options: Intl.DateTimeFormatOptions = {
        weekday: 'short', // e.g., "Fri"
        month: 'short',   // e.g., "Jan"
        day: 'numeric',   // e.g., "19"
        hour: 'numeric',  // e.g., "7"
        minute: '2-digit', // e.g., "30"
        timeZoneName: 'short', // e.g., "ACDT"
        hour12: true,     // 12-hour time
        timeZone: timeZone, // Set the desired timezone
    };

    // Format the date in the given timezone
    return new Intl.DateTimeFormat('en-AU', options).format(date);
}


interface ActivityItemProps {
    id: string;
    title: string;
    imageUrl: string;
    date: string;
    category: string;
    city: string;
    venue: string;
    goingCount: number;
    hostUser: { username: string; id: string; };
    key: string;
}

export function ActivityItem(props: ActivityItemProps) {
    const navigate = useNavigate();

    const {id, title, imageUrl, date, category, goingCount, hostUser} = props;

    // const imageUrl = "https://res.cloudinary.com/dxwtrnpqi/image/upload/v1702382811/sample.jpg"
    // const date = "2024-01-17 00:00:00.0000000"
    // const category = "Category"
    // const goingCount = 24
    // const hostUser = {username: "test"}
    // const title = "Friday Night drinks / language exchange"

    const handleClick = () => {
        // Navigate to the route with the activity's ID
        navigate(`/activity/${id}`);
    };

    const theme = useTheme();
    return (
        <Box component={"div"}
             sx={{
                 // height: '162px',
                 // width: '604px',
                 height: '162px',
                 width: '90%',
                 paddingTop: theme.spacing(2),
                 paddingBottom: theme.spacing(2),
                 cursor: 'pointer',
             }}
             onClick={handleClick}>
            <Grid container>
                <Grid
                    item
                    xs={3.35}
                    sx={{
                        height: '125px',
                    }}>
                    <Box component={"img"}
                         src={imageUrl}
                         sx={{
                             height: '80%',
                             width: 'auto',
                             borderRadius: theme.shape.borderRadius,
                         }}>
                    </Box>
                </Grid>
                <Grid item xs={0.3}/>
                <Grid item xs={8.35}>
                    <Box>
                        <Box display="flex" alignItems="center" mb={1}>
                            <EventIcon color="action" sx={{marginRight: '5px'}}/>
                            <Typography variant="caption" sx={{
                                color: '#7c6f50',
                                fontSize: theme.typography.fontSize,
                                fontWeight: theme.typography.fontWeightBold
                            }}>{formatToLocalTimezone(date)}</Typography>
                        </Box>

                        <Box>
                            <Typography
                                gutterBottom
                                component="div"
                                sx={{
                                    wordWrap: 'break-word',
                                    overflow: 'hidden',
                                    fontWeight: 'fontWeightBold',
                                    lineHeight: '1.1',
                                    '&:hover': {}
                                }}>
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

                        <Box display="flex" alignItems="center">
                            <PeopleIcon color="action" sx={{marginRight: '5px'}}/>
                            <Typography
                                variant="caption">{goingCount} attendees</Typography>
                            <Box flexGrow={1}/>
                            <Typography variant="caption" component="span" sx={{
                                backgroundColor: 'lightgrey',
                                borderRadius: '4px',
                                padding: '2px 5px'
                            }}>
                                {category}
                            </Typography>
                        </Box>

                    </Box>
                </Grid>
            </Grid>

            <Divider sx={{
                marginTop: theme.spacing(2),
            }}/>
        </Box>
    );
}