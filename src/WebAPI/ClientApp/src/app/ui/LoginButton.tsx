import Button from "@mui/material/Button";
import Box from "@mui/material/Box";
import {useAppDispatch} from "@store/store.ts";
import {setLoginForm, setSignUpForm} from "@features/commonSlice.ts";

const LoginButton = () => {
    const dispatch = useAppDispatch()

    function handleOpen(): void {
        dispatch(setLoginForm(true))
        dispatch(setSignUpForm(false))
    }

    return (
        <Box>
            <Button color="inherit" sx={{
                width: 110,
                height: 50,
                fontSize: 16,
                transition: 'none', // 去除动画效果
                '&:hover': {
                    color: '#3e8da0', // 悬停时的字体颜色
                    backgroundColor: 'transparent', // 保持背景透明或设置您希望的颜色
                },
            }}
                    onClick={handleOpen}>
                Login
            </Button>
        </Box>
    );
}

export default LoginButton;
