import {Box, Button, Grid, Typography, useTheme} from '@mui/material';
import IntroImg from '@assets/IntroImg.png';
import {useDispatch, useSelector} from "react-redux";
import {RootState} from "@store/store.ts";
import ImageComponent from "@ui/Image.tsx";
import {setSignUpForm} from "@features/commonSlice.ts";

const Intro = () => {
    const theme = useTheme();
    const dispatch = useDispatch();
    const isMobile = useSelector((state: RootState) => state.common.isMobile);

    const handleOpenSignUpModal = () => {
        dispatch(setSignUpForm(true));
    }

    return (
        <Box sx={{padding: isMobile ? theme.spacing(2) : theme.spacing(5)}}>
            <Grid container alignItems='center'
                  justifyContent='space-between'>
                <Grid item xs={12} sm={6} sx={{textAlign: {xs: 'center', sm: 'left'}}}>
                    <Typography variant='h3' component='h1' gutterBottom sx={{
                        fontWeight: 700,
                        fontSize: {xs: '1.5rem', sm: '2.125rem', md: '3rem'},
                    }}>
                        The people platform—Where interests become friendships
                    </Typography>
                    <Typography variant='body1' sx={{
                        fontSize: {xs: '0.875rem', sm: '1rem'},
                    }}>
                        Whatever your interest, from hiking and reading to networking and
                        skill sharing, there are thousands of people who share it on
                        Events Harbor.
                        Events Harbor are happening every day—sign up to join the fun.
                    </Typography>
                    <Button variant='contained'
                            onClick={handleOpenSignUpModal}
                            sx={{
                                fontSize: 16,
                                margin: theme.spacing(2),
                                backgroundColor: '#00798A',
                                borderRadius: 2,
                                width: 150,
                                height: 45,
                                '&:hover': {
                                    backgroundColor: '#3e8da0',
                                },
                                '&:active': {
                                    transform: 'scale(0.9)',
                                },
                            }}>
                        Join us
                    </Button>
                </Grid>

                <Grid item xs={12} sm={6} sx={{
                    display: 'flex',
                    justifyContent: {xs: 'center', sm: 'flex-end'},
                }}>
                    <ImageComponent Src={IntroImg} Alt={"mainPage"} Sx={{
                        maxWidth: '100%', maxHeight: 650,
                        paddingTop: isMobile && theme.spacing(2),
                    }}/>
                </Grid>
            </Grid>
        </Box>
    );
}

export default Intro;
