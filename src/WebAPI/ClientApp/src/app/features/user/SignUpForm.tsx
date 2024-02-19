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
import {useSelector, useDispatch} from "react-redux";
import {RootState} from "@store/store.ts";
import {setLoginForm, setSignUpForm} from "@features/commonSlice.ts";
import {
    checkEmailRegistered,
} from "@apis/Account.ts";
import {z} from "zod";
import {useForm} from "react-hook-form";
import {zodResolver} from "@hookform/resolvers/zod";
import LogoImg from "@assets/logo.png";
import useRegisterMutation from "@features/user/hooks/useRegisterMutation.ts";
import ImageComponent from "@ui/Image.tsx";
import LoadingComponent from "@ui/LoadingComponent.tsx";
import FormField from "@ui/FormField.tsx";
import useDynamicFormHeight from "@hooks/useDynamicFormHeight.ts";

const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;

const schema = z.object({
    displayName: z.string().min(1, "This field is required"),
    email: z.string().min(1, "This field is required").email("Email address is invalid"),
    password: z.string().min(10, "Password must be at least 10 characters long")
        .regex(/[a-z]/, "Password must contain at least one lowercase letter")
        .regex(/\d/, "Password must contain at least one digit"),
    confirmPassword: z.string().min(1, "This field is required"),
    phoneNumber: z.string().min(1, "This field is required").regex(/^04\d{8}$/, "Phone number must be 04xxxxxxx"),
}).refine(data => data.password === data.confirmPassword || data.confirmPassword === '' || data.confirmPassword === undefined, {
    message: "Passwords must match",
    path: ["confirmPassword"],
}).refine(async (data) => {
    if (!emailRegex.test(data.email)) {return true;}

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

const SignUpForm = () => {
    const theme = useTheme();
    const dispatch = useDispatch();
    const open = useSelector((state: RootState) => state.common.signUpOpen);
    const {mutateAsync: registerMutate, isLoading} = useRegisterMutation();

    const {
        control,
        reset,
        trigger,
        handleSubmit,
        formState: {errors}
    } = useForm<FormValues>({
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


    function handleClose(): void {
        dispatch(setSignUpForm(false));
        reset()
    }

    function handleOpenLogin() {
        dispatch(setLoginForm(true));
        dispatch(setSignUpForm(false))
    }

    async function onSubmit(data: FormValues) {
        const isValid = await trigger(); // 手动触发验证
        if (!isValid) {
            return; // 如果验证失败，则不继续执行
        }
        await registerMutate(data, {
            onSuccess: () => {
                reset()
            },
            onError: () => {
                reset()
            }
        });
    }

    const Height = useDynamicFormHeight(errors, 580, 20);

    return (
        <Dialog open={open} maxWidth='xs' fullWidth sx={{
            '& .MuiDialog-paper': {
                width: 400,
                height: {Height},
                borderRadius: theme.shape.borderRadius,
            },
        }}>
            {isLoading && <LoadingComponent/>}

            <DialogTitle sx={{textAlign: 'center', m: 0, pt: theme.spacing(6)}}>
                <IconButton
                    onClick={handleClose}
                    aria-label='close'
                    sx={{
                        position: 'absolute',
                        right: theme.spacing(2),
                        top: theme.spacing(2),
                    }}
                >
                    <CloseIcon/>
                </IconButton>
                <ImageComponent Src={LogoImg} Alt={"logo"} Sx={{
                    width: 40,
                    height: 40,
                    position: 'absolute',
                    top: theme.spacing(2),
                    left: '50%',
                    transform: 'translateX(-50%)',
                }}></ImageComponent>
                <Typography component={"div"} variant='h6' sx={{
                    flex: 1,
                    textAlign: 'center',
                    fontWeight: theme.typography.fontWeightBold,
                    fontSize: 30
                }}>
                    Finish signing up
                </Typography>
            </DialogTitle>

            <DialogContent dividers>
                <Box component='form'
                     noValidate
                     onSubmit={handleSubmit(onSubmit)}
                     sx={{mt: 1}}>

                    <FormField name='displayName'
                               control={control}
                               errors={errors}
                               label='Your name'
                               required/>

                    <FormField name='email'
                               control={control}
                               errors={errors}
                               label='Email address'
                               required
                               type='email'/>

                    <FormField name='password'
                               control={control}
                               errors={errors}
                               label='Password'
                               required
                               type='password'/>

                    <FormField name='confirmPassword'
                               control={control}
                               errors={errors}
                               label='Confirm Password'
                               required
                               type='password'/>

                    <FormField name='phoneNumber'
                               control={control}
                               errors={errors}
                               label='Phone Number'
                               required/>

                    <Button
                        type='submit'
                        variant='contained'
                        color='secondary'
                        fullWidth
                        sx={{
                            mb: 2,
                            fontSize: 18,
                            fontWeight: 700,
                        }}
                    >
                        sign Up
                    </Button>

                    <Typography component={"div"} variant='body2' align='center'>
                        By signing up, you agree to our
                        <Link href='#' underline='none'>Terms of Service</Link>,
                        <Link href='#' underline='none'>Privacy Policy</Link>, and
                        <Link href='#' underline='none'>Cookie Policy</Link>.
                    </Typography>
                </Box>

                <Divider sx={{mt: 2, mb: 2}}>or</Divider>

                <Typography component={"div"} variant={"body2"} align='center'
                            sx={{mt: 2}}>
                    Not a member yet? <Link href='#' sx={{
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
