import {
    Dialog,
    DialogContent,
    DialogTitle,
    Button,
    Typography,
    IconButton, useTheme,
} from '@mui/material';
import CloseIcon from '@mui/icons-material/Close';
import FacebookIcon from '@mui/icons-material/Facebook'; // These are placeholder icons
import GoogleIcon from '@mui/icons-material/Google'; // Replace with the correct ones
import AppleIcon from '@mui/icons-material/Apple'; // Replace with the correct ones
import EmailIcon from '@mui/icons-material/Email';
import {RootState, useAppDispatch} from "@store/store.ts";
import {useSelector} from "react-redux";
import {setLogin, setSignUp} from "@features/commonSlice.ts";
import Box from "@mui/material/Box";
import {ImageComp} from "@ui/Image.tsx"; // Replace with the correct ones
import LogoImg from '@assets/logo.png';
import {grey} from "@mui/material/colors";

const brandColors = {
    facebook: '#1877F2',
    google: '#DB4437',
    apple: '#A3AAAE',
    email: '#767676', // 电子邮件图标的颜色通常为灰色，您可以根据需要调整
};


function SignUpModal() {
    const theme = useTheme();

    const open = useSelector((state: RootState) => state.common.signUpOpen);
    const dispatch = useAppDispatch()

    function handleClick(): void {
        dispatch(setSignUp())
    }

    function handleOpenLogin() {
        dispatch(setLogin())
    }

    return (
        <Dialog open={open} onClose={handleClick} sx={{
            '& .MuiDialog-paper': { // 直接设置Paper组件的宽度
                width: 400, // 你可以使用固定的宽度或者百分比
                height: 480,
                borderRadius: 4,
            },
        }}>
            <DialogTitle sx={{textAlign: 'center', m: 0, pt: 6}}>
                <ImageComp Src={LogoImg} Alt={"logo"} Sx={{
                    width: 40,
                    height: 40,
                    position: 'absolute',
                    top: theme.spacing(2),
                    left: '50%',
                    transform: 'translateX(-50%)',
                }}></ImageComp>
                <IconButton
                    aria-label="close"
                    onClick={handleClick}
                    sx={{
                        position: 'absolute',
                        right: theme.spacing(1),
                        top: theme.spacing(1),
                        color: theme.palette.grey[500],
                    }}>
                    <CloseIcon/>
                </IconButton>

                <Typography variant="h6" component="div" sx={{
                    flex: 1,
                    textAlign: 'center',
                    fontWeight: 700,
                    fontSize: 30
                }}>
                    Sign up
                </Typography>
            </DialogTitle>

            <DialogContent dividers>

                <Box
                    component={"form"}
                    sx={{
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
                        Continue with Facebook
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
                        Continue with Google
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
                        Continue with Apple
                    </Button>

                    <Button
                        startIcon={<EmailIcon style={{color: brandColors.email}}/>}
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
                        Sign up with email
                    </Button>

                </Box>

                <Typography variant="body2" color="textSecondary" align="center">
                    Already a member?{' '}
                    <Button onClick={handleOpenLogin} sx={{
                        color: '#00798A',
                    }}>
                        Log in
                    </Button>
                </Typography>

            </DialogContent>
        </Dialog>
    );
}

export default SignUpModal;
