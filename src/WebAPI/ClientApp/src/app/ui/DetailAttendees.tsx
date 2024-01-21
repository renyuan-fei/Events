import Box from "@mui/material/Box";
import Typography from "@mui/material/Typography";
import {Attendee} from "@type/Attendee.ts";
import {Avatar, Paper, Stack, useTheme} from "@mui/material";

// @ts-ignore
interface DetailAttendeesProps {
    countGoing: number;
    attendees: Array<Attendee>;
}

// @ts-ignore
export function DetailAttendees() {
    const theme = useTheme();

    // @ts-ignore
    // const {countGoing, attendees} = props;
    const countGoing = 25;

    const attendees: Array<Attendee> = [
        {
            "userId": "c0feebb9-bc2a-446b-876c-0cd8aa9a913d",
            "isHost": true,
            "displayName": "TestEmail@example.com",
            "userName": "TestEmail@example.com",
            "bio": "Hello World!",
            "image": "https://res.cloudinary.com/dxwtrnpqi/image/upload/v1705591941/e9y3v0k6tbkrmcfwr63m.png"
        },
        {
            "userId": "245c20b7-249a-46de-b7f8-0d3cbd13a289",
            "isHost": false,
            "displayName": "TestEmail@example.com",
            "userName": "TestEm111ail@example.com",
            "bio": "Hello World!",
            "image": "https://res.cloudinary.com/dxwtrnpqi/image/upload/v1702453913/gyzjw6xpz9pzl0xg7de4.jpg"
        },
        {
            "userId": "245c20b7-249a-46de-b7f8-0d3cbd13a289",
            "isHost": false,
            "displayName": "TestEmail@example.com",
            "userName": "TestEm111ail@example.com",
            "bio": "Hello World!",
            "image": "https://res.cloudinary.com/dxwtrnpqi/image/upload/v1702453913/gyzjw6xpz9pzl0xg7de4.jpg"
        },
        {
            "userId": "245c20b7-249a-46de-b7f8-0d3cbd13a289",
            "isHost": false,
            "displayName": "TestEmail@example.com",
            "userName": "TestEm111ail@example.com",
            "bio": "Hello World!",
            "image": "https://res.cloudinary.com/dxwtrnpqi/image/upload/v1702453913/gyzjw6xpz9pzl0xg7de4.jpg"
        }
    ];

    return (
        <Box sx={{}}>
            <Typography variant="h2" // The variant closest to your requirement
                        component="h2"
                        sx={{
                            fontSize: '20px', // 20px font size
                            fontWeight: theme.typography.fontWeightBold,
                            fontFamily: '"Graphik Meetup", -apple-system, sans-serif', // Custom font family
                            marginBottom: theme.spacing(2.5),
                        }}>
                Attendees ({attendees.length})
            </Typography>
            <Paper sx={{flexGrow: 1, padding: theme.spacing(3)}}>
                <Stack direction="row" spacing={2} alignItems="center" sx={{}}>
                    {attendees.map((attendee) => (
                        <Box key={attendee.userId} sx={{
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
                            <Typography variant="caption" display="block" gutterBottom>
                                {attendee.displayName}
                            </Typography>
                            <Typography variant="caption" display="block">
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