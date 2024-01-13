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

        props?: {}
    }
}

const margin = '10px 5px';

export const theme = createTheme({
    palette: {
        primary: {
            main: '#00798a',
            light: 'rgb(51, 147, 161)',
            dark: 'rgb(0, 84, 96)',
        },
        secondary: {
            main: '#e32359',
            light: 'rgb(232, 79, 122)',
            dark: 'rgb(158, 24, 62)',
        },
        error: {
            main: red[500],
            light: red[300],
            dark: red[700],
        },
    },
    components: {
        MuiButton: {
            styleOverrides: {
                root:({ ownerState }) => ({
                    // 用条件逻辑判断按钮颜色是否为primary
                    ...(ownerState.color === 'primary' && {
                        backgroundColor: '#00798a',
                        '&:hover': {
                            backgroundColor: '#3e8da0',
                        }
                    }),
                    ...(ownerState.color === 'secondary' && {
                        backgroundColor: '#e32359',
                        '&:hover': {
                            backgroundColor: '#e63a6a',
                        }
                    }),
                    borderRadius: 8,
                    margin: margin,
                }),
            }
        },
        MuiButtonGroup: {
            styleOverrides: {
                root:{
                    size: 'small',
                }
            }
        },
        MuiCheckbox: {
            styleOverrides: {
                root:{
                    size: 'small',
                }
            }
        },
        MuiFab: {

        },
        MuiFormControl: {
            styleOverrides: {
                root:{
                    size: 'small',
                    margin: 'dense',

                }
            }
        },
        MuiFormHelperText: {
            styleOverrides: {
                root:{
                    margin: 'dense',
                }
            }
        },
        MuiIconButton: {
            styleOverrides: {
                root:{
                    size: 'small',
                }
            }
        },
        MuiInputBase: {
            styleOverrides: {
                root:{
                    margin: 'dense',
                }
            }
        },
        MuiInputLabel: {
            styleOverrides: {
                root:{
                    margin: 'dense',
                }
            }
        },
        MuiRadio: {
            styleOverrides: {
                root:{
                    size: 'small',
                }
            }
        },
        MuiSwitch: {
            styleOverrides: {
                root:{
                    size: 'small',
                }
            }
        },
        MuiTextField: {
            styleOverrides: {
                root:{
                    size: 'small',
                    margin: margin,
                    borderRadius: 8
                }
            }
        },
        MuiAppBar: {
            styleOverrides: {
                root:{
                    color: 'default',
                }
            }
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
        button: {
            fontWeight: 600,
        },
    },
    breakpoints: {
        values: {
            xs: 0,
            sm: 600,
            md: 900,
            lg: 1200,
            xl: 1536,
        },
    },
    status: {
        danger: red[800],
        success: green[600],
        info: blue[400],
    },
    shape: {
        borderRadius: 4,
    },
    spacing: 8,
    props: {
        MuiAppBar: {
            color: 'default',
        },
    },
});
