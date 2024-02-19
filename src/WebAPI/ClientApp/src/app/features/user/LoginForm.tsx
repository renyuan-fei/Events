import {
    Button,
    Dialog,
    DialogContent,
    DialogTitle,
    Divider,
    IconButton,
    Link, Typography,
    useTheme
} from '@mui/material';
import CloseIcon from '@mui/icons-material/Close';

import LogoImg from "@assets/logo.png";
import {useSelector} from "react-redux";
import {RootState, useAppDispatch} from "@store/store.ts";
import Box from "@mui/material/Box";
import {
    setLoginForm,
    setSignUpForm
} from "@features/commonSlice.ts";
import {z} from 'zod';
import {useForm} from "react-hook-form";
import {zodResolver} from "@hookform/resolvers/zod";
import useLoginMutation from "@features/user/hooks/useLoginMutation.ts";
import FormField from "@ui/FormField.tsx";
import FormCheckbox from "@ui/FormCheckbox.tsx";
import ImageComponent from "@ui/Image.tsx";
import LoadingComponent from "@ui/LoadingComponent.tsx";
import useDynamicFormHeight from "@hooks/useDynamicFormHeight.ts";

interface FormValues {
    email: string;
    password: string;
    keepSignedIn: boolean;
}

const schema = z.object({
    email: z.string().email('Invalid email address').min(1, 'Email is required'),
    password: z.string().min(1, 'Password is required'),
});

const LoginForm = () => {
    const theme = useTheme();
    const dispatch = useAppDispatch()
    const open = useSelector((state: RootState) => state.common.LoginOpen);
    const {mutateAsync: loginMutate, isLoading} = useLoginMutation();
    const {control, reset, handleSubmit, formState: {errors}} = useForm<FormValues>({
        resolver: zodResolver(schema),
        defaultValues: {
            // email: '',
            // password: '',
            email: 'TestEmail@example.com',
            password: 'TestPassword123456789',
            keepSignedIn: false,
        }
    });

    function handleClose(): void {
        dispatch(setLoginForm(false))
        reset();
    }

    function handleOpenSignUp() {
        dispatch(setSignUpForm(true))
        dispatch(setLoginForm(false))
    }

    const onSubmit = async (data: FormValues) => {
        await loginMutate(data, {
            onSuccess: () => {
                reset();
            }
        });
    };

    const Height = useDynamicFormHeight(errors, 580, 20);

    return (
        <Dialog open={open} maxWidth='xs' fullWidth sx={{
            '& .MuiDialog-paper': {
                width: 400,
                height: {Height},
                borderRadius: theme.shape.borderRadius,
            },
        }}
        >
            {isLoading && <LoadingComponent/>}

            <DialogTitle sx={{textAlign: 'center', m: 0, pt: theme.spacing(5)}}>
                <IconButton
                    aria-label='close'
                    onClick={handleClose}
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
                }}/>

                <Typography component='div' variant='h6' sx={{
                    flex: 1,
                    textAlign: 'center',
                    fontWeight: theme.typography.fontWeightBold,
                    fontSize: 30
                }}>
                    Log in
                </Typography>

                <Typography component='div' sx={{mb: 2}}>
                    Not a member yet?
                    <Link href='#' sx={{
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

                    <FormField name='email'
                               control={control}
                               errors={errors}
                               label='Email'
                               type='email'
                               required/>

                    <FormField name='password'
                               control={control}
                               errors={errors}
                               label='Password'
                               type='password'
                               required/>

                    <FormCheckbox
                        label={'Keep me signed in'}
                        name={'keepSignedIn'}
                        control={control}
                    />

                    <Button
                        type='submit'
                        variant='contained'
                        color='secondary'
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

                <Typography component='div' align='center' variant='body2'>
                    Issues with log in?
                    <Link href='#' sx={{
                        color: '#00798A',
                        textDecoration: 'none',
                        '&:hover': {
                            textDecoration: '#3e8da0',
                        }
                    }}>
                        Help
                    </Link>
                </Typography>

            </DialogContent>
        </Dialog>
    );
}

export default LoginForm;