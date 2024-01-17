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
import React, {FormEvent, useEffect, useState} from "react";
import {CustomPasswordTextField} from "@ui/Custom/CustomPasswordTextField.tsx";
import {
    setAlertInfo,
    setLoginForm,
    setSignUpForm
} from "@features/commonSlice.ts";
import {useLoginMutation} from "@apis/Account.ts";

interface FormValues {
    email: string;
    password: string;
}

interface FormErrors {
    [key: string]: string;
}

function LoginForm() {
    const dispatch = useAppDispatch()
    const theme = useTheme();
    const open = useSelector((state: RootState) => state.common.LoginOpen);
    const [Height, setHeight] = useState(580)
    const [formErrors, setFormErrors] = useState<FormErrors>({});
    const [formValues, setFormValues] = useState<FormValues>({
        email: 'TestEmail@example.com',
        password: 'TestPassword123456789',
    });

    const { mutate: loginMutate} = useLoginMutation(formValues, ()=>{
        dispatch(setAlertInfo({
            open: true,
            message: 'You have been logged in',
            severity: 'success',
        }));
        dispatch(setLoginForm(false))
    },(error: any)=>{
        dispatch(setAlertInfo({
            open: true,
            message: error.message,
            severity: 'error',
        }));
    });

    function handleClose(): void {
        dispatch(setLoginForm(false))
        setFormValues({
            email: '',
            password: '',
        })
        setFormErrors({})
    }

    function handleOpenSignUp() {
        dispatch(setSignUpForm(true))
        dispatch(setLoginForm(false))
    }

    function validate(name: keyof FormValues, value: string): FormErrors {
        let errors: FormErrors = {};

        switch (name) {
            case 'email':
                errors.email = /\S+@\S+\.\S+/.test(value) ? '' : 'Email address is invalid';
                break;
            case 'password':
                if (!value) {
                    errors.password = 'This field is required';
                } else if (value.length < 10) {
                    errors.password = 'Password must be at least 10 characters long';
                } else if (!/[a-z]/.test(value)) {
                    errors.password = 'Password must contain at least one lowercase letter';
                } else if (!/\d/.test(value)) {
                    errors.password = 'Password must contain at least one digit';
                } else {
                    errors.password = '';
                }
                break;
            default:
                break;
        }
        return errors;
    }

    function handleChange(event: React.ChangeEvent<HTMLInputElement>) {
        const {name, value} = event.target;
        setFormValues(prevState => ({...prevState, [name]: value}));
        setFormErrors(validate(name as keyof FormValues, value));
    }

    useEffect(() => {
        function countErrors(errors : FormErrors): number {
            return Object.values(errors).filter(error => error !== '').length;
        }
        const baseDialogHeight = 580; // 基础高度
        const errorHeightIncrement = 20; // 每个错误增加的高度
        const errorCount = countErrors(formErrors);
        setHeight(baseDialogHeight + errorHeightIncrement * errorCount);
    }, [formErrors]);

    const handleLogin = (event: FormEvent) => {
        event.preventDefault();

        const newErrors = Object.keys(formValues).reduce((errors: FormErrors, fieldName: string) => {
            return {...errors, ...validate(fieldName as keyof FormValues, formValues[fieldName as keyof FormValues])};
        }, {});

        setFormErrors(newErrors);

        const isFormValid = Object.keys(newErrors).every(fieldName => !newErrors[fieldName]) &&
            Object.keys(formValues).every(fieldName => formValues[fieldName as keyof FormValues] !== '');

        if (isFormValid) {
            console.log('Form is valid! Submitting...', formValues);
            loginMutate();
        } else {
            console.log('Form is invalid or incomplete! Not submitting.');
        }
    };

    return (
        <Dialog open={open} maxWidth="xs" fullWidth sx={{
            '& .MuiDialog-paper': {
                width: 400,
                height: {Height},
                borderRadius: theme.shape.borderRadius,
            },
        }}
        >

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

                <Box component={"form"} noValidate onSubmit={handleLogin}>

                    <CustomTextField
                        name="email"
                        label="Email"
                        type="email"
                        required
                        autoComplete="email"
                        value={formValues.email}
                        onChange={handleChange}
                        error={Boolean(formErrors.email)}
                        helperText={formErrors.email || ''}
                    />

                    <CustomPasswordTextField
                        name="password"
                        label="Password"
                        autoComplete="password"
                        required
                        value={formValues.password}
                        onChange={handleChange}
                        error={Boolean(formErrors.password)}
                        helperText={formErrors.password || ''}
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