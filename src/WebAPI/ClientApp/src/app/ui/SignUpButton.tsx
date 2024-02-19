import Button from '@mui/material/Button';
import {useAppDispatch} from "@store/store.ts";
import {useMediaQuery, useTheme} from "@mui/material";
import {setLoginForm, setSignUpForm} from "@features/commonSlice.ts";

const SignupButton = () => {
    const theme = useTheme();
    const isMobile = useMediaQuery(theme.breakpoints.down('sm'));
    const dispatch = useAppDispatch()

    function handleOpen(): void {
        dispatch(setSignUpForm(true))
        dispatch(setLoginForm(false))
    }

    if (isMobile) {
        return (
            <Button variant="contained" color="primary" sx={{
                width: 90,
                height: 35,
                fontSize: 14,
            }}
                    onClick={handleOpen}
            >
                Sign up
            </Button>
        )
    }

    return (
        <Button variant="contained" color="primary" sx={{
            width: 110,
            height: 50,
            fontSize: 16,
        }}
                onClick={handleOpen}
        >
            Sign up
        </Button>
    );
}

export default SignupButton;
