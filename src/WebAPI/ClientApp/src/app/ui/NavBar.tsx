import AppBar from '@mui/material/AppBar';
import Toolbar from '@mui/material/Toolbar';
import {Grid, useMediaQuery, useTheme} from "@mui/material";
// 确保导入正确的路径
import SearchComponent from "@ui/Search.tsx";
import {RootState} from "@store/store.ts";
import {useSelector} from "react-redux";
import {queryClient} from "@apis/queryClient.ts";
import UserBar from "@ui/UserBar.tsx";
import AuthLanguageControl from "@ui/AuthLanguageControl.tsx";
import {userInfo} from "@type/UserInfo.ts";
import Logo from "@ui/Logo.tsx";



export function NavBar() {
    // TODO response design for each size

    const theme = useTheme();
    const isMobile = useMediaQuery(theme.breakpoints.down('sm'));
    const {isLogin} = useSelector((state: RootState) => state.user)

    const userInfo = queryClient.getQueryData<userInfo>("userInfo");

    const {id, displayName, image} = userInfo || {id: '', displayName: '', image: ''};

    return (
        <AppBar position='fixed' color='inherit'
                sx={{
                    height: 78,
                    padding: {
                        padding: theme.spacing(1),
                        backgroundColor: 'white',
                        backgroundImage: 'none',
                        boxShadow: 'none',
                    }
                    , border: {
                        borderWidth: 1,
                        borderBottom: `1px solid ${theme.palette.divider}`,
                    },
                }}>
            <Toolbar>
                {isMobile ? (
                    <Grid container alignItems='center'
                          justifyContent='space-between'>

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
                ) : (
                    isLogin ? (
                            <Grid container alignItems='center' justifyContent='flex-end'>
                                <Grid item xs={6} sm={3} md={2.5} lg={2.2} xl={2.4}>
                                    <Logo/>
                                </Grid>

                                <Grid item xs={12} sm={5} md={5} lg={6.2} xl={7.6}
                                      order={{xs: 3, sm: 2}}>
                                    <SearchComponent/>
                                </Grid>

                                <Grid item xs={6} sm={4} md={4.5} lg={3.6} xl={2}
                                      order={{xs: 2, sm: 3}}>
                                    <UserBar id={id} displayName={displayName} image={image}/>
                                </Grid>
                            </Grid>
                        ) :
                        (
                            <Grid container alignItems='center' justifyContent='flex-end'>
                                <Grid item xs={6} sm={4} md={2.5} lg={3.2}>
                                    <Logo/>
                                </Grid>

                                <Grid item xs={12} sm={5} md={6.5} lg={6.6}
                                      order={{xs: 3, sm: 2}}>
                                    <SearchComponent/>
                                </Grid>


                                <Grid item xs={6} sm={3} md={3} lg={2.2}
                                      order={{xs: 2, sm: 3}}>
                                    {<AuthLanguageControl/>}
                                </Grid>
                            </Grid>
                        ))}
            </Toolbar>
        </AppBar>
    );
}
