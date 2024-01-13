import Button from '@mui/material/Button';
import {useAppDispatch} from "@store/store.ts";
import {useMediaQuery, useTheme} from "@mui/material";
import {setSignUpForm} from "@features/commonSlice.ts";

function SignupButton() {
    const theme = useTheme();
    const isMobile = useMediaQuery(theme.breakpoints.down('sm'));
    const dispatch = useAppDispatch()

    function handleClick(): void {
        dispatch(setSignUpForm())
    }

    if (isMobile) {
        return (
            <Button variant="contained" color="primary" sx={{
                width: 90,
                height: 35,
                fontSize: 14,
            }}
                    onClick={handleClick}
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
                onClick={handleClick}
        >
            Sign up
        </Button>
    );
}

export default SignupButton;
