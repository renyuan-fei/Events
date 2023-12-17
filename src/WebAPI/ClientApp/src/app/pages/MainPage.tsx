import {Box, Button, Typography, useTheme} from '@mui/material';
import {ImageComp} from "@ui/Image.tsx";
import mainPageImg from "@assets/mainPageImg.png";
export default function MainPage() {
    const theme = useTheme();
    // const isMobile = useMediaQuery(theme.breakpoints.down('sm'));

    return (
        <Box
            sx={{
                display: 'flex',
                justifyContent: 'space-between',
                alignItems: 'center',
                padding: theme.spacing(3),
                backgroundColor: 'white', // 根据您的需求修改背景色
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
                    fontWeight: 800,
                }}>
                    The people platform—Where interests become friendships
                </Typography>
                <Typography variant="body1" gutterBottom sx={{
                    fontSize: 20,
                }}>
                    Whatever your interest, from hiking and reading to networking and
                    skill sharing,
                    there are thousands of people who share it on Meetup. Events are
                    happening
                    every day—sign up to join the fun.
                </Typography>

                <Button variant="contained" size="large"
                        sx={{
                            marginTop: theme.spacing(2),
                            backgroundColor: '#3e8da0',
                            borderRadius: 2,
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
                <ImageComp Src={mainPageImg} Alt={"mainPage"} Sx={{
                    maxWidth: 600, maxHeight: 400
                }}/>
            </Box>
        </Box>
    );
}
