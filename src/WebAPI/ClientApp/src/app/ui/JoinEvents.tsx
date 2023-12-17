import {Box, Button, Typography, useTheme} from '@mui/material';
import JoinEventsImg from "@assets/JoinEventsImg.png";
import {ImageComp} from "@ui/Image.tsx";

export default function JoinEvents() {
    const theme = useTheme();

    return (
        <Box
            sx={{
                display: 'flex',
                justifyContent: 'space-between',
                alignItems: 'center',
                padding: theme.spacing(5),
                my: theme.spacing(10),
                backgroundColor: '#97CAD114',
                borderRadius: 2.5,
            }}
        >
            <Box>
                <Typography variant="h4" component="h2" gutterBottom sx={{
                    fontWeight: 700
                }}>
                    Join Events
                </Typography>
                <Typography variant="body1">
                    People use Events to meet new people, learn new things, find support,
                    get out of their
                    comfort zones, and pursue their passions, together. Membership is
                    free.
                </Typography>
                <Button variant="contained" color="secondary" sx={{
                    marginTop: theme.spacing(2),
                    backgroundColor: '#e32359',
                    width: 192,
                    height: 48,
                    fontWeight: 700,
                    borderRadius: 3,
                }}>
                    Sign up
                </Button>
            </Box>

            <ImageComp Src={JoinEventsImg} Alt={"JoinEvents"} Sx={{
                width: 500,
                height: 250,
            }}/>
        </Box>
    );
}
