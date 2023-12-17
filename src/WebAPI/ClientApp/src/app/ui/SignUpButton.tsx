import Button from '@mui/material/Button';

function SignupButton() {
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
        }}>
            Sign up
        </Button>
    );
}

export default SignupButton;
