import {Box, Button, Grid, Typography, Link, Divider} from '@mui/material';
import FacebookIcon from '@mui/icons-material/Facebook';
import TwitterIcon from '@mui/icons-material/Twitter';
import YouTubeIcon from '@mui/icons-material/YouTube';
import InstagramIcon from '@mui/icons-material/Instagram';
import {grey} from "@mui/material/colors";
import Container from "@mui/material/Container";

function Footer() {
    return (
        <Box sx={{
            backgroundColor: grey[900],
            color: 'white',
            padding: '2rem',
        }}>
            <Container maxWidth={"xl"}>
                <Box sx={{textAlign: 'center', paddingBottom: '2rem'}}>
                    <Button variant="outlined" color="primary" sx={{
                        borderRadius: 2.5,
                        border: 2,
                        fontWeight: 800,
                        borderColor: grey[50],
                        height: 48,
                        width: 150,
                        color: grey[50],
                        '&:hover': {
                            backgroundColor: grey[50],
                            color: grey[900],
                        },
                    }}>
                        Get Started
                    </Button>
                </Box>


                <Divider sx={{borderColor: grey[700], marginBottom: '1rem'}}/>

                <Grid container spacing={5} justifyContent="space-around" direction={{ xs: 'column', sm: 'row' }}>
                    <Grid item>
                        <Typography variant="h6" gutterBottom>Your Account</Typography>
                        <Link href="#" color="inherit" underline="none">Sign up</Link>
                        <br/>
                        <Link href="#" color="inherit" underline="none">Log in</Link>
                        <br/>
                        <Link href="#" color="inherit" underline="none">Help</Link>
                        <br/>
                        <Link href="#" color="inherit" underline="none">Become an
                            Affiliate</Link>
                    </Grid>
                    <Grid item>
                        <Typography variant="h6" gutterBottom>Discover</Typography>
                        <Link href="#" color="inherit" underline="none">Groups</Link>
                        <br/>
                        <Link href="#" color="inherit" underline="none">Calendar</Link>
                        <br/>
                        <Link href="#" color="inherit" underline="none">Topics</Link>
                        <br/>
                        <Link href="#" color="inherit" underline="none">Cities</Link>
                        <br/>
                        <Link href="#" color="inherit" underline="none">Online
                            Events</Link>
                        <br/>
                        <Link href="#" color="inherit" underline="none">Local
                            Guides</Link>
                        <br/>
                        <Link href="#" color="inherit" underline="none">Make
                            Friends</Link>
                    </Grid>
                    <Grid item>
                        <Typography variant="h6" gutterBottom>Meetup</Typography>
                        <Link href="#" color="inherit" underline="none">About</Link>
                        <br/>
                        <Link href="#" color="inherit" underline="none">Blog</Link>
                        <br/>
                        <Link href="#" color="inherit" underline="none">Meetup Pro</Link>
                        <br/>
                        <Link href="#" color="inherit" underline="none">Careers</Link>
                        <br/>
                        <Link href="#" color="inherit" underline="none">Apps</Link>
                        <br/>
                        <Link href="#" color="inherit" underline="none">Podcast</Link>
                    </Grid>
                </Grid>

                <Box sx={{
                    display: 'flex',
                    justifyContent: 'center',
                    gap: 2,
                    paddingBottom: '2rem',
                    paddingTop: "2rem"
                }}>
                    <Typography variant="h6" gutterBottom>Follow us</Typography>
                    <Link href="#" color="inherit"><FacebookIcon/></Link>
                    <Link href="#" color="inherit"><TwitterIcon/></Link>
                    <Link href="#" color="inherit"><YouTubeIcon/></Link>
                    <Link href="#" color="inherit"><InstagramIcon/></Link>
                </Box>

                <Typography variant="caption" display="block" textAlign="center"
                            paddingBottom="2rem">
                    © 2023 Events Terms of Service Privacy Policy Cookie Policy Help
                </Typography>
            </Container>
        </Box>
    );
};

export default Footer;
