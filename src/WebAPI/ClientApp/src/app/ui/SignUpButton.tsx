import Button from '@mui/material/Button';
import { createTheme, ThemeProvider } from '@mui/material/styles';

// 创建一个主题，将颜色代码应用于主要（primary）颜色变量
const theme = createTheme({
    palette: {
        primary: {
            main: '#3e8da0',
        },
    },
});

function SignupButton() {
    return (
        <ThemeProvider theme={theme}>
            <Button variant="contained" color="primary" sx={{
                width: 120,
                height: 50,
                borderRadius: 2,
                fontSize: 16,
            }}>
                Sign up
            </Button>
        </ThemeProvider>
    );
}

export default SignupButton;
