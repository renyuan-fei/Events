import LogoImg from "@assets/logo.png";
import Typography from "@mui/material/Typography";
import { Grid, Box } from "@mui/material";
import { createTheme, ThemeProvider } from '@mui/material/styles';

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
    return (
        <ThemeProvider theme={theme}>
            <Box sx={{ flexGrow: 1, display: 'flex', alignItems: 'center' }}>
                <Grid container spacing={2} alignItems="center">
                    <Grid item xs={6}>
                        <img src={LogoImg} alt={"Logo"} style={{ maxWidth: 80, maxHeight: 60 }} />
                    </Grid>
                    <Grid item xs={6}>
                        <Typography variant="h6" component="div">
                            Events
                        </Typography>
                    </Grid>
                </Grid>
            </Box>
        </ThemeProvider>
    );
}
