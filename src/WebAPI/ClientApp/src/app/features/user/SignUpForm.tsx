import {useEffect, useRef, useState} from 'react';
import {
    Button, Dialog,
    DialogContent,
    DialogTitle, Divider,
    // FormControlLabel, Grid,
    IconButton,
    Link,
    // Link, Radio, RadioGroup,
    Typography,
    useTheme
} from '@mui/material';
import CloseIcon from '@mui/icons-material/Close';
import Box from "@mui/material/Box";
import CustomTextField from "@ui/Custom/CustomTextField.tsx";
import {useSelector, useDispatch} from "react-redux";
import {RootState} from "@store/store.ts";
import { setLoginForm, setSignUpForm} from "@features/commonSlice.ts";
import {
    checkEmailRegistered,
} from "@apis/Account.ts";
import {z} from "zod";
import {Controller, FieldErrors, useForm} from "react-hook-form";
import {zodResolver} from "@hookform/resolvers/zod";
import LogoImg from "@assets/logo.png";
import useRegisterMutation from "@features/user/hooks/useRegisterMutation.ts";
import CustomPasswordTextField from "@ui/Custom/CustomPasswordTextField.tsx";
import ImageComp from "@ui/Image.tsx";
import LoadingComponent from "@ui/LoadingComponent.tsx";


const schema = z.object({
    displayName: z.string().optional().refine((val) => val === undefined || val === '' || val.length >= 1, {
        message: "This field is required"
    }),
    email: z.string().optional().refine((val) => val === undefined || val === '' || /^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(val), {
        message: "Email address is invalid"
    }),
    password: z.string().optional().refine((val) => {
        // 当密码字段不为空时，应用所有的密码规则
        if(val === undefined || val === '') return true;
        return val.length >= 10 && /[a-z]/.test(val) && /\d/.test(val);
    }, {
        message: "Password must be at least 10 characters long, contain at least one lowercase letter, and one digit"
    }),
    confirmPassword: z.string(),
    phoneNumber: z.string().optional().refine((val) => val === undefined || val === '' || /^04\d{8}$/.test(val), {
        message: "Phone number must be 04xxxxxxx"
    }),
}).refine(data => data.password === data.confirmPassword || data.confirmPassword === undefined || data.confirmPassword === '', {
    message: "Passwords must match",
    path: ["confirmPassword"],
}).refine(async (data) => {

    if(data.email === undefined || data.email === '') return true;

    const isRegistered = await checkEmailRegistered(data.email);

    console.log(isRegistered);

    return !isRegistered;
}, {
    message: "Email is already registered",
    path: ["email"],
});


interface FormValues {
    displayName: string;
    email: string;
    password: string;
    confirmPassword: string;
    phoneNumber: string;
}

const SignUpForm = () => {
    const abortControllerRef = useRef(new AbortController());
    const theme = useTheme();
    const open = useSelector((state: RootState) => state.common.signUpOpen);
    const dispatch = useDispatch();

    const { control,reset, watch, trigger, handleSubmit,formState: { errors } } = useForm<FormValues>({
        resolver: zodResolver(schema),
        // defaultValues: {
        //     displayName: 'TestEmail@example.com',
        //     email: 'TestEmail@example.com',
        //     password: 'TestPassword123456789',
        //     confirmPassword: 'TestPassword123456789',
        //     phoneNumber: '0422937524',
        // }
        defaultValues: {
            displayName: '',
            email: '',
            password: '',
            confirmPassword: '',
            phoneNumber: '',
        }
    });

    const email = watch("email");
    const confirmPassword = watch("confirmPassword");
    const phoneNumber = watch("phoneNumber");

    useEffect(() => {
        return () => {
            // 组件卸载时取消所有挂起的请求
            abortControllerRef.current.abort();
        };
    }, []);

    useEffect(() => {
        // 当 email 变化时，触发验证
        trigger("email");
    }, [email, trigger]);

    useEffect(() => {
        trigger("confirmPassword");
    }, [confirmPassword, trigger]);

    useEffect(() => {
        trigger("phoneNumber");
    }, [phoneNumber,trigger]);


    const [Height, setHeight] = useState(780)

    const { mutateAsync: registerMutate, isLoading} = useRegisterMutation();

    function handleClose(): void {
        dispatch(setSignUpForm(false));

        reset()
    }

    function handleOpenLogin() {
        dispatch(setLoginForm(true));
        dispatch(setSignUpForm(false))
    }

    async function onSubmit(data: FormValues) {
        await registerMutate(data,{
            onSuccess: () => {
                reset()
            },
            onError: () => {
                reset()
            }
        });
    }


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
        }}>
            {isLoading && <LoadingComponent/>}

            <DialogTitle sx={{textAlign: 'center' , m: 0, pt: theme.spacing(6)}}>
                <IconButton
                    onClick={handleClose}
                    aria-label="close"
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
                <Typography component={"div"} variant="h6" sx={{
                    flex: 1,
                    textAlign: 'center',
                    fontWeight: theme.typography.fontWeightBold,
                    fontSize: 30
                }}>
                    Finish signing up
                </Typography>
            </DialogTitle>

            <DialogContent dividers>
                <Box component="form" noValidate onSubmit={handleSubmit(onSubmit)} sx={{mt: 1}}>
                    <Controller
                        name="displayName"
                        control={control}
                        render={({ field }) => (
                            <CustomTextField
                                {...field}
                                label="Your name"
                                required
                                autoFocus
                                error={!!errors.displayName}
                                helperText={errors.displayName?.message || ''}
                            />
                        )}
                    />

                    <Controller
                        name="email"
                        control={control}
                        render={({ field }) => (
                            <CustomTextField
                                {...field}
                                label="Email address"
                                type="email"
                                required
                                autoComplete="email"
                                error={!!field.value && !!errors.email}
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
                                type="password"
                                required
                                autoComplete="new-password"
                                error={!!errors.password}
                                helperText={errors.password?.message || ''}
                            />
                        )}
                    />

                    <Controller
                        name="confirmPassword"
                        control={control}
                        render={({ field }) => (
                            <CustomPasswordTextField
                                {...field}
                                label="Confirm Password"
                                type="password"
                                required
                                error={!!errors.confirmPassword}
                                helperText={errors.confirmPassword?.message || ''}
                            />
                        )}
                    />

                    <Controller
                        name="phoneNumber"
                        control={control}
                        render={({ field }) => (
                            <CustomTextField
                                {...field}
                                label="Phone Number"
                                type="text"
                                required
                                error={!!errors.phoneNumber}
                                helperText={errors.phoneNumber?.message || ''}
                            />
                        )}
                    />

                    {/* Implement reCAPTCHA here */}
                    <Button
                        type="submit"
                        variant="contained"
                        color="secondary"
                        fullWidth
                        sx={{
                            mb: 2,
                            fontSize: 18,
                            fontWeight: 700,
                        }}
                    >
                        sign Up
                    </Button>

                    <Typography component={"div"} variant="body2" align="center">
                        By signing up, you agree to our
                        <Link href="#" underline="none">Terms of Service</Link>,
                        <Link href="#" underline="none">Privacy Policy</Link>, and
                        <Link href="#" underline="none">Cookie Policy</Link>.
                    </Typography>
                </Box>

                <Divider sx={{mt: 2, mb: 2}}>or</Divider>

                <Typography component={"div"} variant={"body2"} align="center"
                            sx={{mt: 2}}>
                    Not a member yet? <Link href="#" sx={{
                    color: '#00798A',
                    textDecoration: 'none',
                }} onClick={handleOpenLogin}>Sign
                    in</Link>
                </Typography>
            </DialogContent>
        </Dialog>
    );
}

export default SignUpForm;
