import AppBar from '@mui/material/AppBar';
import Toolbar from '@mui/material/Toolbar';
import {createTheme, Grid, ThemeProvider, useMediaQuery, useTheme} from "@mui/material";
// 确保导入正确的路径
import {AuthLanguageControl} from "./AuthLanguageControl";
import {Logo} from "./Logo";
import SearchComponent from "@ui/Search.tsx";

const APP_BAR_THEME = createTheme({
    components: {
        MuiAppBar: {
            styleOverrides: {
                root: {
                    backgroundColor: 'white',
                    backgroundImage: 'none',
                    boxShadow: 'none',
                },
            },
        },
    },
});

export function NavBar() {
    const theme = useTheme();
    const isMobile = useMediaQuery(theme.breakpoints.down('sm'));

    return (
        <ThemeProvider theme={APP_BAR_THEME}>
            <AppBar position="fixed" color="inherit"
                    sx={{
                        padding: {
                            xs: 2,
                            sm: 2,
                            md: 2,
                            lg: 2,
                        }
                    }}>
                <Toolbar>
                    {isMobile ? (
                        <Grid container alignItems="center"
                              justifyContent="space-between">
                            <Grid item xs={6} sm={4} md={2.5} lg={2.1}>
                                <Logo/>
                            </Grid>
                            <Grid item xs={12} sm={5} md={6.5} lg={7.7}
                                  order={{xs: 3, sm: 2}}>
                                <SearchComponent/>
                            </Grid>
                            <Grid item xs={6} sm={3} md={3} lg={1.2}
                                  order={{xs: 2, sm: 3}}>
                                <AuthLanguageControl/>
                            </Grid>
                        </Grid>
                    ) : (<Grid container alignItems="center"
                               justifyContent="space-between">
                        <Grid item xs={6} sm={4} md={2.5} lg={1.7}>
                            <Logo/>
                        </Grid>
                        <Grid item xs={12} sm={5} md={6.5} lg={8.8}
                              order={{xs: 3, sm: 2}}>
                            <SearchComponent/>
                        </Grid>
                        <Grid item xs={6} sm={3} md={3} lg={1.5} order={{xs: 2, sm: 3}}>
                            <AuthLanguageControl/>
                        </Grid>
                    </Grid>)}
                </Toolbar>
            </AppBar>
        </ThemeProvider>
    );
}
