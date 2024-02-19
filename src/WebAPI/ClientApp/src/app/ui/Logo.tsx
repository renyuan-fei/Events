import LogoImg from "@assets/logo.png";
import Typography from "@mui/material/Typography";
import {Box, Grid, useMediaQuery} from "@mui/material";
import {createTheme, ThemeProvider} from '@mui/material/styles';
import {useNavigate} from "react-router";
import {useCallback} from "react";

// 创建一个主题，其中包括自定义的字体
const theme = createTheme({
    typography: {
        fontFamily: [
            'Marker Felt', // 这里可以替换成您喜欢的字体
            'sans-serif',
        ].join(','),
        fontSize: 25,
    },
});

export function Logo() {
    const isMobile = useMediaQuery(theme.breakpoints.down('sm'));
    const navigate = useNavigate();

    const onBoxClick = useCallback((event: React.MouseEvent<HTMLDivElement>) => {
        event.preventDefault();
        navigate('/');
    }, [navigate]);


    return (
        <ThemeProvider theme={theme}>
            {isMobile ?
                <Box sx={{
                    flexGrow: 1,
                    display: 'flex',
                    alignItems: 'center',
                    width: 80,
                    cursor: 'pointer',
                }}>
                    <Grid container spacing={6} alignItems='center'>
                        <Grid item xs={6}>
                            <img src={LogoImg} alt={"Logo"}
                                 style={{maxWidth: 50, maxHeight: 50}}/>
                        </Grid>
                        <Grid item xs={6}>
                            <Typography variant='h6' component='div'>
                                Events
                            </Typography>
                        </Grid>
                    </Grid>
                </Box> :
                <Box sx={{
                    flexGrow: 1,
                    display: 'flex',
                    alignItems: 'center',
                    width: 150,
                    cursor: 'pointer',
                }} onClick={onBoxClick}>
                    <Grid container spacing={6} alignItems='center'>
                        <Grid item xs={6}>
                            <img src={LogoImg} alt={"Logo"}
                                 style={{maxWidth: 70, maxHeight: 70}}/>
                        </Grid>
                        <Grid item xs={6}>
                            <Typography variant='h6' component='div'>
                                Events
                            </Typography>
                        </Grid>
                    </Grid>
                </Box>}
        </ThemeProvider>
    );
}
