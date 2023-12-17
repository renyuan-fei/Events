import {Box, Button, Typography, useTheme} from '@mui/material';
import {ImageComp} from "@ui/Image.tsx";
import IntroImg from "@assets/introImg.png";
export default function Intro() {
    const theme = useTheme();
    // const isMobile = useMediaQuery(theme.breakpoints.down('sm'));

    return (
        <Box
            sx={{
                display: 'flex',
                justifyContent: 'space-between',
                alignItems: 'center',
                my: theme.spacing(10),
                [theme.breakpoints.down('sm')]: {
                    flexDirection: 'column',
                },
            }}
        >
            <Box
                sx={{
                    maxWidth: '60%',
                    [theme.breakpoints.down('sm')]: {
                        maxWidth: '100%',
                        textAlign: 'center',
                        marginBottom: theme.spacing(2),
                    },
                }}
            >
                <Typography variant="h3" component="h1" gutterBottom sx={{
                    fontWeight: 700,
                    fontSize: 60,
                }}>
                    The people platform—Where interests become friendships
                </Typography>
                <Typography variant="body1" gutterBottom sx={{
                    fontSize: 23,
                }}>
                    Whatever your interest, from hiking and reading to networking and
                    skill sharing,
                    there are thousands of people who share it on Meetup. Events are
                    happening
                    every day—sign up to join the fun.
                </Typography>

                <Button variant="contained"
                        sx={{
                            fontSize: 16,
                            marginTop: theme.spacing(2),
                            backgroundColor: '#3e8da0',
                            borderRadius: 2,
                            width: 150,
                            height: 45,
                        }}>
                    Join Events
                </Button>

            </Box>

            <Box
                sx={{
                    flexShrink: 0,
                    [theme.breakpoints.down('sm')]: {
                        width: '80%',
                    },
                }}
            >
                <ImageComp Src={IntroImg} Alt={"mainPage"} Sx={{
                    maxWidth: 600, maxHeight: 400
                }}/>
            </Box>
        </Box>
    );
}
