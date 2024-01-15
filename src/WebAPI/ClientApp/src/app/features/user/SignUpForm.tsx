import React, {FormEvent, useEffect, useState} from 'react';
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
import {useRegisterQuery} from "@apis/Account.ts";
import {setLoginForm, setSignUpForm} from "@features/commonSlice.ts";
import {LoadingComponent} from "@ui/LoadingComponent.tsx";


interface FormValues {
    displayName: string;
    email: string;
    password: string;
    confirmPassword: string;
    phoneNumber: string;
}

interface FormErrors {
    [key: string]: string;
}

function SignUpForm() {
    const theme = useTheme();
    const open = useSelector((state: RootState) => state.common.signUpOpen);
    const dispatch = useDispatch();

    const [formValues, setFormValues] = useState<FormValues>({
        displayName: 'TestEmail@example.com',
        email: 'TestEmail@example.com',
        password: 'TestPassword123456789',
        confirmPassword: 'TestPassword123456789',
        phoneNumber: '719159880',
    });

    const registerQuery = useRegisterQuery(formValues);

    const [formErrors, setFormErrors] = useState<FormErrors>({});

    const [Height, setHeight] = useState(780)

    function handleClick(): void {
        dispatch(setSignUpForm());
        setFormValues({
            displayName: '',
            email: '',
            password: '',
            confirmPassword: '',
            phoneNumber: ''
        })
        setFormErrors({})
    }

    function handleOpenLogin() {
        dispatch(setLoginForm());
    }

    function validate(name: keyof FormValues, value: string): FormErrors {
        let errors: FormErrors = {};

        switch (name) {
            case 'displayName':
                errors.name = value ? '' : 'This field is required';
                break;
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
            case 'confirmPassword':
                if (!value) {
                    errors.confirmPassword = 'This field is required';
                } else {
                    errors.confirmPassword = value === formValues.password ? '' : 'Passwords must match';
                }
                break;
            case 'phoneNumber':
                if (!value) {
                    errors.confirmPassword = 'This field is required';
                } else if (!/\d{10}/.test(value)) {
                    errors.confirmPassword = value === formValues.phoneNumber ? '' : 'The mobile phone number must be 10 digits';
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

    function handleSignUp(event: FormEvent) {
        event.preventDefault();

        // Validate each field
        const newErrors = Object.keys(formValues).reduce((errors: FormErrors, fieldName: string) => {
            return {...errors, ...validate(fieldName as keyof FormValues, formValues[fieldName as keyof FormValues])};
        }, {});

        setFormErrors(newErrors);

        // Check if there are any errors or empty fields
        const isFormValid = Object.keys(newErrors).every(fieldName => !newErrors[fieldName]) &&
            Object.keys(formValues).every(fieldName => formValues[fieldName as keyof FormValues] !== '');

        if (isFormValid) {
            console.log('Form is valid! Submitting...', formValues);
            // Insert submit logic here
            registerQuery.refetch();

            if (registerQuery.isLoading)
            {
                return <LoadingComponent/>;
            }

        } else {
            console.log('Form is invalid or incomplete! Not submitting.');
        }
    }

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
                    onClick={handleClick}
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
                <Box component="form" noValidate onSubmit={handleSignUp} sx={{mt: 1}}>
                    <CustomTextField
                        name="name"
                        label="Your name"
                        required
                        autoFocus
                        value={formValues.displayName}
                        onChange={handleChange}
                        error={Boolean(formErrors.name)}
                        helperText={formErrors.name || ''}
                    />
                    <CustomTextField
                        name="email"
                        label="Email address"
                        type={"email"}
                        required
                        autoComplete="email"
                        value={formValues.email}
                        onChange={handleChange}
                        error={Boolean(formErrors.email)}
                        helperText={formErrors.email || ''}
                    />
                    <CustomTextField
                        name="password"
                        label="Password"
                        type="password"
                        required
                        autoComplete="new-password"
                        value={formValues.password}
                        onChange={handleChange}
                        error={Boolean(formErrors.password)}
                        helperText={formErrors.password || ''}
                    />
                    <CustomTextField
                        name="confirmPassword"
                        label="confirm Password"
                        type="password"
                        required
                        value={formValues.confirmPassword}
                        onChange={handleChange}
                        error={Boolean(formErrors.confirmPassword)}
                        helperText={formErrors.confirmPassword || ''}
                    />
                    <CustomTextField
                        name="phoneNumber"
                        label="phone number"
                        type="text"
                        required
                        value={formValues.phoneNumber}
                        onChange={handleChange}
                        error={Boolean(formErrors.phoneNumber)}
                        helperText={formErrors.phoneNumber || ''}
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
