import {
    Dialog,
    DialogContent,
    DialogTitle,
    Button,
    IconButton,
    Typography,
    TextField,
    Checkbox,
    FormControlLabel,
    Link,
    Divider, useTheme
} from '@mui/material';
import CloseIcon from '@mui/icons-material/Close';
import VisibilityOffIcon from '@mui/icons-material/VisibilityOff';
import FacebookIcon from '@mui/icons-material/Facebook'; // 替换为正确的图标
import GoogleIcon from '@mui/icons-material/Google'; // 替换为正确的图标
import AppleIcon from '@mui/icons-material/Apple'; // 替换为正确的图标
import LogoImg from "@assets/Logo.png";
import {useSelector} from "react-redux";
import {RootState, useAppDispatch} from "@store/store.ts";
import {setLogin, setSignUp} from "@features/commonSlice.ts";
import Box from "@mui/material/Box";
import {grey} from "@mui/material/colors";
import {ImageComp} from "@ui/Image.tsx";

const brandColors = {
    facebook: '#1877F2',
    google: '#DB4437',
    apple: '#A3AAAE',
};

function SignInModal() {
    const theme = useTheme();

    const open = useSelector((state: RootState) => state.common.LoginOpen);
    const dispatch = useAppDispatch()

    function handleClick(): void {
        dispatch(setLogin())
    }

    function handleOpenSignUp() {
        dispatch(setSignUp())
    }

    return (
        <Dialog open={open} onClose={handleClick} maxWidth="xs" fullWidth sx={{
            '& .MuiDialog-paper': { // 直接设置Paper组件的宽度
                width: 400, // 你可以使用固定的宽度或者百分比
                height: 740,
                borderRadius: 4,
            },
        }}
        >
            <DialogTitle sx={{textAlign: 'center', m: 0, pt: 6}}>
                <IconButton
                    aria-label="close"
                    onClick={handleClick}
                    sx={{
                        position: 'absolute',
                        right: theme.spacing(2),
                        top: theme.spacing(2),
                    }}
                >
                    <CloseIcon/>
                </IconButton>
                <ImageComp Src={LogoImg} Alt={"logo"} Sx={{
                    width: 40,
                    height: 40,
                    position: 'absolute',
                    top: theme.spacing(2),
                    left: '50%',
                    transform: 'translateX(-50%)',
                }}></ImageComp>
                <Typography variant="h6" sx={{
                    flex: 1,
                    textAlign: 'center',
                    fontWeight: 700,
                    fontSize: 30
                }}>
                    Log in
                </Typography>
                <Typography sx={{mb: 2}}>
                    Not a member yet? <Link href="#" sx={{
                    color: '#00798A',
                    textDecoration: 'none',
                    '&:hover': {
                        textDecoration: '#3e8da0',
                    }
                }} onClick={handleOpenSignUp}>Sign
                    up</Link>
                </Typography>
            </DialogTitle>

            <DialogContent dividers>

                <Box component={"form"}>
                    <TextField
                        label="Email"
                        type="email"
                        fullWidth
                        variant="outlined"
                        margin="dense"
                        autoComplete="email"
                        sx={{
                            '& label.Mui-focused': {
                                color: '#00798A',
                            },
                            '& .MuiOutlinedInput-root': {
                                '&.Mui-focused fieldset': {
                                    borderColor: '#00798A',
                                },
                            },
                        }}
                    />
                    <TextField
                        label="Password"
                        type="password"
                        fullWidth
                        variant="outlined"
                        margin="dense"
                        autoComplete="current-password"
                        InputProps={{
                            endAdornment: <VisibilityOffIcon/>,
                        }}
                        sx={{
                            '& label.Mui-focused': {
                                color: '#00798A',
                            },
                            '& .MuiOutlinedInput-root': {
                                '&.Mui-focused fieldset': {
                                    borderColor: '#00798A',
                                },
                            },
                        }}
                    />
                    <FormControlLabel
                        control={
                            <Checkbox
                                sx={{
                                    padding: theme.spacing(2),
                                    color: '#00798A', // 未选中时的颜色
                                    '&.Mui-checked': {
                                        color: '#00798A', // 选中时的颜色
                                    },
                                }}
                            />
                        }
                        label="Keep me signed in"
                    />
                    <Button
                        variant="contained"
                        color="primary"
                        fullWidth
                        sx={{
                            mb: 2,
                            backgroundColor: '#e32359',
                            fontSize: 18,
                            fontWeight: 700,
                            borderRadius: 2,
                            '&:hover': {
                                backgroundColor: '#e32359',
                            }
                        }}
                    >
                        Log in
                    </Button>

                </Box>
                <Divider sx={{mt: 2, mb: 2}}>or</Divider>
                <Box sx={{
                    pt: 2,
                    display: 'flex',
                    flexDirection: 'column',
                    justifyContent: 'center',
                    alignItems: 'center',
                    width: '100%',
                }}>
                    <Button
                        startIcon={<FacebookIcon style={{color: brandColors.facebook}}/>}
                        fullWidth
                        variant="outlined"
                        sx={{
                            marginBottom: 2,
                            width: 320,
                            height: 50,
                            borderRadius: 2,
                            fontSize: 15,
                            color: grey[900],
                            borderColor: grey[300],
                        }}
                    >
                        Log in with Facebook
                    </Button>
                    <Button
                        startIcon={<GoogleIcon style={{color: brandColors.google}}/>}
                        fullWidth
                        variant="outlined"
                        sx={{
                            marginBottom: 2,
                            width: 320,
                            height: 50,
                            borderRadius: 2,
                            fontSize: 15,
                            color: grey[900],
                            borderColor: grey[300],
                        }}
                    >
                        Log in with Google
                    </Button>
                    <Button
                        startIcon={<AppleIcon style={{color: brandColors.apple}}/>}
                        fullWidth
                        variant="outlined"
                        sx={{
                            marginBottom: 2,
                            width: 320,
                            height: 50,
                            borderRadius: 2,
                            fontSize: 15,
                            color: grey[900],
                            borderColor: grey[300],
                        }}
                    >
                        Log in with Apple
                    </Button>
                </Box>
                <Typography align="center" variant="body2">
                    Issues with log in? <Link href="#" sx={{
                    color: '#00798A',
                    textDecoration: 'none',
                    '&:hover': {
                        textDecoration: '#3e8da0',
                    }
                }}>Help</Link>
                </Typography>
            </DialogContent>
        </Dialog>
    );
}

export default SignInModal;