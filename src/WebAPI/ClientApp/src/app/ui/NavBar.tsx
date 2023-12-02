import AppBar from '@mui/material/AppBar';
import Toolbar from '@mui/material/Toolbar';
import Typography from '@mui/material/Typography';
import Button from '@mui/material/Button';
import IconButton from '@mui/material/IconButton';
import MenuIcon from '@mui/icons-material/Menu';
import Box from "@mui/material/Box";
import Divider from "@mui/material/Divider";
import {createTheme, ThemeProvider} from "@mui/material";

const theme = createTheme({});

export function NavBar() {
    return (
        <ThemeProvider theme={theme}><Box sx={{flexGrow: 1}}>
            <AppBar position="fixed">
                <Toolbar>

                    <IconButton
                        size="large"
                        edge="start"
                        color="inherit"
                        aria-label="menu"
                        sx={{mr: 2}}
                    >
                        <MenuIcon/>
                    </IconButton>

                    <Typography variant="h6" component="div" sx={{flexGrow: 0}}>
                        Events
                    </Typography>

                    <Divider orientation="vertical" variant={"middle"} flexItem
                             sx={{mx: 2, display: {xs: 'none', md: 'flex'}}}/>

                    <Box sx={{flexGrow: 0, display: 'flex', alignItems: 'center'}}>
                        <Button sx={{my: 2, color: 'white', display: 'block'}}>
                            Activities
                        </Button>

                        <Divider orientation="vertical" flexItem sx={{mx: 2}}
                                 variant={"middle"}/>

                        <Button variant={"contained"}
                                sx={{my: 2, color: 'white', display: 'block'}}>
                            Create Activity
                        </Button>
                    </Box>

                    <Box sx={{flexGrow: 1}}/>

                    <Button color="inherit">Login</Button>
                </Toolbar>
            </AppBar>
        </Box></ThemeProvider>
    );
}
