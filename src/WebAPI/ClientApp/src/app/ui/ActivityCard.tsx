import Card from '@mui/material/Card';
import CardMedia from '@mui/material/CardMedia';
import CardContent from '@mui/material/CardContent';
import Typography from '@mui/material/Typography';
import EventIcon from '@mui/icons-material/Event';
import PeopleIcon from '@mui/icons-material/People';
import Box from '@mui/material/Box';
import {CardActionArea, useTheme} from "@mui/material";


// @ts-ignore
export default function ActivityCard() {
    const theme = useTheme();

    return (
        <Card sx={{
            maxWidth: {xs: '100%', sm: 345},
            margin: theme.spacing(2),
            borderRadius: 5
        }}
              variant="outlined">
            <CardActionArea>
                <CardMedia
                    component="img"
                    height="220"
                    image="https://res.cloudinary.com/dxwtrnpqi/image/upload/v1702382852/cld-sample-3.jpg"
                    alt="Group picture"
                    sx={{borderRadius: 5}}
                />
                <CardContent>
                    <Box>
                        <Typography
                            gutterBottom
                            variant="h5"
                            component="div"
                            sx={{
                                wordWrap: 'break-word',
                                overflow: 'hidden',
                                '&:hover': {}
                            }}
                        >
                            {"Lizardfffffffffffffffffffffffffffff"}
                        </Typography>

                    </Box>
                    <Box display="flex" alignItems="center" mb={1}>
                        <Typography variant="caption" sx={{
                            fontWeight: 600,
                            fontSize: 14,
                            color: theme.palette.text.secondary,
                            mr: 1
                        }}>Hosted by: {"international" +
                            " student" +
                            " support"}</Typography>
                    </Box>
                    <Box display="flex" alignItems="center" mb={1}>
                        <EventIcon color="action" sx={{marginRight: '5px'}}/>
                        <Typography variant="caption">THU, DEC 21 • 18:30
                            GMT+10</Typography>
                    </Box>
                    <Box display="flex" alignItems="center">
                        <PeopleIcon color="action" sx={{marginRight: '5px'}}/>
                        <Typography variant="caption">113 going</Typography>
                        <Box flexGrow={1}/>
                        <Typography variant="caption" component="span" sx={{
                            bgcolor: 'lightgrey',
                            borderRadius: '4px',
                            padding: '2px 5px'
                        }}>
                            Free
                        </Typography>
                    </Box>
                </CardContent>
            </CardActionArea>
        </Card>
    );
}
