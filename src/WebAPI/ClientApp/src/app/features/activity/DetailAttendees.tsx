import Box from "@mui/material/Box";
import Typography from "@mui/material/Typography";
import {Attendee} from "@type/Attendee.ts";
import {Avatar, Paper, Stack, useTheme} from "@mui/material";
import Button from "@mui/material/Button";
import {useNavigate} from "react-router";

// @ts-ignore
interface DetailAttendeesProps {
    attendees: Array<Attendee>;
}

// @ts-ignore
const DetailAttendees = (props: DetailAttendeesProps) => {
    const theme = useTheme();
    const navigate = useNavigate();

    const {attendees} = props;

    const handleSeeAllClick = () => {
        navigate('/all-attendees/${id}');
    };

    return (
        <Box sx={{ marginTop: theme.spacing(2), marginBottom:theme.spacing(3)}}>
            <Box sx={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', marginBottom: theme.spacing(1) }}>
            <Typography component="div" variant="h2" sx={{
                fontSize: '20px',
                fontWeight: theme.typography.fontWeightBold,
                fontFamily: '"Graphik Meetup", -apple-system, sans-serif',
            }}>
                Attendees ({attendees.length})
            </Typography>
            <Button
                variant={"text"}
                onClick={handleSeeAllClick}
                sx={{
                    color: '#00798A', // The color from the image
                    fontFamily: '"Graphik Meetup", -apple-system, sans-serif', // The font from the image
                    fontSize: '16px', // The font size from the image
                    textTransform: 'none', // Prevents uppercase transformation
                    textDecoration: 'none', // Removes underline
                }}
            >
                See all
            </Button>

        </Box>
            <Paper sx={{flexGrow: 1, padding: theme.spacing(3)}}>
                <Stack direction="row" spacing={2} alignItems="center" sx={{}}>
                    {attendees.map((attendee) => (
                        <Box key={attendee.useId} sx={{
                            textAlign: 'center',
                            backgroundColor: 'rgb(246,247,248)',
                            height: '182px',
                            width: '140px',
                            padding: '20px 8px',
                            borderRadius: theme.shape.borderRadius,
                            display: 'flex', // Enables flexbox
                            flexDirection: 'column', // Stack children vertically
                            justifyContent: 'center', // Center children vertically
                            alignItems: 'center', // Center children horizontally
                            borderColor: 'rgb(217 217 217)',
                            borderStyle:'solid',
                            borderWidth: '0.2px'
                        }}>
                            <Avatar
                                alt={attendee.displayName}
                                src={attendee.image}
                                sx={{
                                    width: 56,
                                    height: 56,
                                    border: attendee.isHost ? '2px solid #3e8da0' : '',
                                    marginBottom: '8px', // Add space between the avatar and the text
                                }}
                            />
                            <Typography component="div" variant="caption" display="block" gutterBottom>
                                {attendee.displayName}
                            </Typography>
                            <Typography component="div" variant="caption" display="block">
                                {attendee.isHost ? 'Host' : 'Member'}
                            </Typography>
                        </Box>

                    ))}
                    {/* If you have more attendees that are not shown, add a "+X more" item */}
                    {attendees.length > 3 && (
                        <Box sx={{textAlign: 'center'}}>
                            <Avatar>+{attendees.length - 3} more</Avatar>
                        </Box>
                    )}
                </Stack>
            </Paper>
        </Box>
    );
}

export default DetailAttendees;