import Button from '@mui/material/Button';
import {useAppDispatch} from "@store/store.ts";
import {setSignUp} from "@features/commonSlice.ts";

function SignupButton() {
    const dispatch = useAppDispatch()

    function handleClick(): void {
        dispatch(setSignUp())
    }
    return (
        <Button variant="contained" color="primary" sx={{
            width: 120,
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
