import Button from "@mui/material/Button";
import Box from "@mui/material/Box";

export function LoginButton() {
    return (
        <Box>
            <Button color="inherit" sx={{
                width: 120,
                height: 50,
                fontSize: 16,
                transition: 'none', // 去除动画效果
                '&:hover': {
                    color: '#3e8da0', // 悬停时的字体颜色
                    backgroundColor: 'transparent', // 保持背景透明或设置您希望的颜色
                },
            }}>
                Login
            </Button>
        </Box>
    );
}
