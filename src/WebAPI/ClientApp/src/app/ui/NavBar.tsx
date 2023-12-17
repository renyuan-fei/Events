// 在 NavBar.jsx 文件中
import AppBar from '@mui/material/AppBar';
import Toolbar from '@mui/material/Toolbar';
import {createTheme, ThemeProvider} from "@mui/material";
import SearchComponent from "@ui/Search.tsx";
import Box from "@mui/material/Box";
import {AuthLanguageControl} from "@ui/AuthLanguageControl.tsx";
import {Logo} from "@ui/Logo.tsx";

const APP_BAR_THEME = createTheme({
    components: {
        MuiAppBar: {
            styleOverrides: {
                root: {
                    backgroundColor: 'white',
                    backgroundImage: 'none',
                    boxShadow: 'none'
                },
            },
        },
    },
});

export function NavBar() {
    return (
        <ThemeProvider theme={APP_BAR_THEME}>
            <AppBar position="fixed" color="inherit"
                    sx={{
                        padding: {
                            xs: 1,
                            sm: 1,
                            md: 1,
                            lg: 1,
                        }
                    }}>
                <Toolbar>

                    <Logo/>

                    <Box sx={{flexGrow:2}}/>

                    <SearchComponent/>

                    <Box sx={{flexGrow: 80}}/>

                    <AuthLanguageControl/>
                </Toolbar>
            </AppBar>
        </ThemeProvider>
    );
}
