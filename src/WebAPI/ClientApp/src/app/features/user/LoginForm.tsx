import {
    Button,
    Checkbox,
    Dialog,
    DialogContent,
    DialogTitle,
    Divider,
    FormControlLabel,
    IconButton,
    Link, Typography,
    useTheme
} from '@mui/material';
import CloseIcon from '@mui/icons-material/Close';

import LogoImg from "@assets/logo.png";
import {useSelector} from "react-redux";
import {RootState, useAppDispatch} from "@store/store.ts";
import Box from "@mui/material/Box";
import {ImageComp} from "@ui/Image.tsx";
import CustomTextField from "@ui/Custom/CustomTextField.tsx";
import {useEffect, useState} from "react";
import {CustomPasswordTextField} from "@ui/Custom/CustomPasswordTextField.tsx";
import {
    setAlertInfo,
    setLoginForm,
    setSignUpForm
} from "@features/commonSlice.ts";
import {useLoginMutation} from "@apis/Account.ts";
import { z } from 'zod';
import {Controller, FieldErrors, useForm} from "react-hook-form";
import {zodResolver} from "@hookform/resolvers/zod";
import {LoadingComponent} from "@ui/LoadingComponent.tsx";
import {useNavigate} from "react-router";

interface FormValues {
    email: string;
    password: string;
}

const schema = z.object({
    email: z.string().email('Invalid email address').min(1, 'Email is required'),
    password: z.string().min(1, 'Password is required'),
});

function LoginForm() {
    const navigate = useNavigate();
    const dispatch = useAppDispatch()
    const theme = useTheme();
    const open = useSelector((state: RootState) => state.common.LoginOpen);
    const [Height, setHeight] = useState(580)
    const { control,reset, handleSubmit,formState: { errors } } = useForm<FormValues>({
        resolver: zodResolver(schema),
        defaultValues: {
            email: 'TestEmail@example.com',
            password: 'TestPassword123456789',
        }
    });

    const { mutate: loginMutate, isLoading } = useLoginMutation(()=>{
        dispatch(setAlertInfo({
            open: true,
            message: 'You have been logged in',
            severity: 'success',
        }));
        dispatch(setLoginForm(false));

        navigate('/home');
    },(error)=>{
        dispatch(setAlertInfo({
            open: true,
            message: error.message,
            severity: 'error',
        }));

        reset({
            email: '',
            password: '',
        });
    });

    function handleClose(): void {
        dispatch(setLoginForm(false))

        reset({
            email: '',
            password: '',
        });
    }

    function handleOpenSignUp() {
        dispatch(setSignUpForm(true))
        dispatch(setLoginForm(false))
    }

    const onSubmit = (data: FormValues) => {
        loginMutate(data);
    };

    useEffect(() => {
        const countErrors = (errors : FieldErrors<FormValues>) => {
            // 计算具有实际错误信息的字段数量
            return Object.values(errors).filter(error => error).length;
        };

        const baseDialogHeight = 580; // 基础高度
        const errorHeightIncrement = 20; // 每个错误增加的高度
        const errorCount = countErrors(errors);
        setHeight(baseDialogHeight + errorHeightIncrement * errorCount);
    }, [errors]); // 监听 errors 对象


    return (
        <Dialog open={open} maxWidth="xs" fullWidth sx={{
            '& .MuiDialog-paper': {
                width: 400,
                height: {Height},
                borderRadius: theme.shape.borderRadius,
            },
        }}
        >
            {isLoading && <LoadingComponent/>}

            <DialogTitle sx={{textAlign: 'center', m: 0, pt: 6}}>
                <IconButton
                    aria-label="close"
                    onClick={handleClose}
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
                <Typography component="div" variant="h6" sx={{
                    flex: 1,
                    textAlign: 'center',
                    fontWeight: 700,
                    fontSize: 30
                }}>
                    Log in
                </Typography>
                <Typography component="div" sx={{mb: 2}}>
                    Not a member yet?
                    <Link href="#" sx={{
                        color: '#00798A',
                        textDecoration: 'none',
                        '&:hover': {
                            textDecoration: '#3e8da0',
                        }
                    }} onClick={handleOpenSignUp}>
                        Sign up
                    </Link>
                </Typography>
            </DialogTitle>

            <DialogContent dividers>

                <Box component={"form"} noValidate onSubmit={handleSubmit(onSubmit)}>

                    <Controller
                        name="email"
                        control={control}
                        render={({ field }) => (
                            <CustomTextField
                                {...field}
                                label="Email"
                                type="email"
                                required
                                autoComplete="email"
                                error={Boolean(errors.email)}
                                helperText={errors.email?.message || ''}
                            />
                        )}
                    />
                    <Controller
                        name="password"
                        control={control}
                        render={({ field }) => (
                            <CustomPasswordTextField
                                {...field}
                                label="Password"
                                autoComplete="current-password"
                                required
                                error={Boolean(errors.password)}
                                helperText={errors.password?.message || ''}
                            />
                        )}
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
                        type="submit"
                        variant="contained"
                        color="secondary"
                        fullWidth
                        sx={{
                            mb: 2,
                            fontSize: 18,
                            fontWeight: 700,
                            borderRadius: 2,
                        }}
                    >
                        Log in
                    </Button>

                </Box>

                <Divider sx={{mt: 2, mb: 2}}>or</Divider>

                <Typography component="div" align="center" variant="body2">
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

export default LoginForm;