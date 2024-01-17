import AppBar from '@mui/material/AppBar';
import Toolbar from '@mui/material/Toolbar';
import {Grid, useMediaQuery, useTheme} from "@mui/material";
// 确保导入正确的路径
import {AuthLanguageControl} from "./AuthLanguageControl";
import {Logo} from "./Logo";
import SearchComponent from "@ui/Search.tsx";
import {RootState} from "@store/store.ts";
import {useSelector} from "react-redux";
import {UserBar} from "@ui/User/UserBar.tsx";

export function NavBar() {
    const theme = useTheme();
    const isMobile = useMediaQuery(theme.breakpoints.down('sm'));
    const {isLogin, userName, imageUrl} = useSelector((state : RootState) => state.user)

    return (
        <AppBar position="fixed" color="inherit"
                sx={{
                    padding: {
                        padding: theme.spacing(1),
                        backgroundColor: 'white',
                        backgroundImage: 'none',
                        boxShadow: 'none',
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
                           justifyContent="flex-end">
                    <Grid item xs={6} sm={4} md={2.5} lg={3.2}>
                        <Logo/>
                    </Grid>
                    <Grid item xs={12} sm={5} md={6.5} lg={6.6}
                          order={{xs: 3, sm: 2}}>
                        <SearchComponent/>
                    </Grid>

                    <Grid item xs={6} sm={3} md={3} lg={2.2} order={{xs: 2, sm: 3}}>
                        {isLogin ? <UserBar displayName={userName}  image={imageUrl}/> : <AuthLanguageControl/>}
                    </Grid>
                </Grid>)}
            </Toolbar>
        </AppBar>
    );
}
