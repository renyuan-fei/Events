import {useEffect, useState} from 'react';
import {
    Button,
    Dialog,
    DialogContent,
    DialogTitle,
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
import {setAlertInfo, setLoginForm, setSignUpForm} from "@features/commonSlice.ts";
import {
    checkEmailRegistered,
    useRegisterMutation
} from "@apis/Account.ts";
import {z} from "zod";
import {Controller, FieldErrors, useForm} from "react-hook-form";
import {zodResolver} from "@hookform/resolvers/zod";
import {CustomPasswordTextField} from "@ui/Custom/CustomPasswordTextField.tsx";


const schema = z.object({
    displayName: z.string().nonempty({ message: "This field is required" }),
    email: z.string().email({ message: "Email address is invalid" }),
    password: z.string().min(10, { message: "Password must be at least 10 characters long" })
        .regex(/[a-z]/, { message: "Password must contain at least one lowercase letter" })
        .regex(/\d/, { message: "Password must contain at least one digit" }),
    confirmPassword: z.string(),
    phoneNumber: z.string()
        .regex(/^04\d{8}$/, {message: "Phone number must be 04xxx-xxxx-xxxx"})
}).refine(data => data.password === data.confirmPassword, {
    message: "Passwords must match",
    path: ["confirmPassword"],
}).refine(async (data) => {
    const isRegistered = await checkEmailRegistered(data.email);
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

function SignUpForm() {
    const theme = useTheme();
    const open = useSelector((state: RootState) => state.common.signUpOpen);
    const dispatch = useDispatch();

    const { control,reset, watch, trigger, handleSubmit,getValues, formState: { errors } } = useForm<FormValues>({
        resolver: zodResolver(schema),
        defaultValues: {
            displayName: 'TestEmail@example.com',
            email: 'TestEmail@example.com',
            password: 'TestPassword123456789',
            confirmPassword: 'TestPassword123456789',
            phoneNumber: '719159880',
        }
    });

    const email = watch("email");

    useEffect(() => {
        // 当 email 变化时，触发验证
        trigger("email");
    }, [email, trigger]);


    const [Height, setHeight] = useState(780)

    let values = getValues();
    const { mutate: registerMutate} = useRegisterMutation(values, ()=>{
        dispatch(setAlertInfo({
            open: true,
            message: 'Registration successful',
            severity: 'success',
        }));
        dispatch(setSignUpForm(false))
    },(error: any)=>{
        dispatch(setAlertInfo({
            open: true,
            message: error.message,
            severity: 'error',
        }));
    });

    function handleClose(): void {
        dispatch(setSignUpForm(false));

        reset({
            displayName: '',
            email: '',
            password: '',
            confirmPassword: '',
            phoneNumber: ''
        })
    }

    function handleOpenLogin() {
        dispatch(setLoginForm(true));
        dispatch(setSignUpForm(false))
    }

    function onSubmit() {
        registerMutate();
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
            <DialogTitle sx={{textAlign: 'center'}}>
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
                <Typography component={"div"} variant="h6" sx={{fontWeight: 700}}>
                    Finish signing up
                </Typography>
            </DialogTitle>

            <DialogContent>
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
                                error={!!errors.email}
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
