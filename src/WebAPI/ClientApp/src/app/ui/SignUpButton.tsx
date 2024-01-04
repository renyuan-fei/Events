import Button from '@mui/material/Button';
import {useAppDispatch} from "@store/store.ts";
import {setSignUp} from "@features/commonSlice.ts";
import {useMediaQuery, useTheme} from "@mui/material";

function SignupButton() {
    const theme = useTheme();
    const isMobile = useMediaQuery(theme.breakpoints.down('sm'));
    const dispatch = useAppDispatch()

    function handleClick(): void {
        dispatch(setSignUp())
    }

    if (isMobile) {
        return (
            <Button variant="contained" color="primary" sx={{
                width: 90,
                height: 35,
                borderRadius: 2,
                fontSize: 14,
                transition: 'none',
                backgroundColor: '#00798A',
                '&:hover': {
                    backgroundColor: '#3e8da0'
                }
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
            borderRadius: 2,
            fontSize: 16,
            transition: 'none',
            backgroundColor: '#00798A',
            '&:hover': {
                backgroundColor: '#3e8da0'
            }
        }}
                onClick={handleClick}
        >
            Sign up
        </Button>
    );
}

export default SignupButton;
