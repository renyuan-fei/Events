import { Paper, Typography, Stack, useTheme } from '@mui/material';
import EventIcon from '@mui/icons-material/Event';
import LocationOnIcon from '@mui/icons-material/LocationOn';
import CheckCircleIcon from '@mui/icons-material/CheckCircle';
import Button from "@mui/material/Button";

export function DetailSidebar() {
    const theme = useTheme();
    return (
        <Paper sx={{
            padding: theme.spacing(2),
            marginTop: theme.spacing(2),
            position: 'relative',
        }}>
            <Stack spacing={2}>
                <Stack direction="row" alignItems="flex-start" spacing={2}>
                    <CheckCircleIcon color="success" />
                    <Typography component="div" variant="h6" sx={{ fontFamily: '"Graphik Meetup",' +
                            ' -apple-system', fontSize: '16px', fontWeight: theme.typography.fontWeightBold }}>
                        Respond by Wednesday, 24 January 2024 12:00 pm
                    </Typography>
                </Stack>

                <Stack direction="row" alignItems="flex-start" spacing={2}>
                    <EventIcon color="action" />
                    <Typography component="div" variant="body1" sx={{ ml: 1, fontFamily: '"Graphik' +
                            ' Meetup", -apple-system', fontSize: '16px', fontWeight: theme.typography.fontWeightBold }}>
                        Friday, 26 January 2024 at 12:00 pm to Friday, 26 January 2024 at 4:00 pm ACDT
                    </Typography>
                </Stack>

                <Stack direction="row" alignItems="flex-start" spacing={2}>
                    <LocationOnIcon color="action" />
                    <Typography component="div" variant="body1" sx={{ ml: 1, fontFamily: '"Graphik' +
                            ' Meetup", -apple-system', fontSize: '16px', fontWeight: theme.typography.fontWeightBold }}>
                        Eastwood Community Centre 95 Glen Osmond Rd - Eastwood, SA
                    </Typography>
                </Stack>

                <Stack direction="row" alignItems="flex-start" spacing={2} justifyContent={"center"}>
                    <Button variant={"contained"} color={"secondary"}>
                        Attend
                    </Button>
                </Stack>
            </Stack>
        </Paper>
    );
}
