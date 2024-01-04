import {createTheme} from '@mui/material/styles';
import {blue, green, red} from '@mui/material/colors';

declare module '@mui/material/styles' {
    interface Theme {
        status: {
            danger: string;
            success: string;
            info: string;
        };
    }

    interface ThemeOptions {
        status?: {
            danger?: string;
            success?: string;
            info?: string;
        };
    }
}

export const theme = createTheme({
    palette: {
        primary: {
            main: blue[500],
            light: blue[300],
            dark: blue[700],
        },
        secondary: {
            main: green[500],
            light: green[300],
            dark: green[700],
        },
        error: {
            main: red[500],
            light: red[300],
            dark: red[700],
        },
    },
    typography: {
        fontFamily: 'Arial, sans-serif',
        h1: {
            fontSize: '2.5rem',
        },
        h2: {
            fontSize: '2rem',
        },
        // ...其他排版设置
    },
    breakpoints: {
        values: {
            xs: 0,
            sm: 600,
            md: 960,
            lg: 1280,
            xl: 1920,
        },
    },
    components: {
        MuiButton: {
            styleOverrides: {
                root: {
                    borderRadius: '8px',
                    // ...其他按钮样式
                },
            },
        },
        // ...其他组件样式覆盖
    },
    status: {
        danger: red[800],
        success: green[600],
        info: blue[400],
    },
});
