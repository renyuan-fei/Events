import Box from "@mui/material/Box";
import Grid from "@mui/material/Grid";
import SignUpButton from "@ui/SignUpButton";
import {LoginButton} from "@ui/LoginButton.tsx";

export function AuthLanguageControl() {
    return (
        <Box sx={{
            width: { xs: '100%', sm: '200px', md: '200px', lg: '250px' },
            height: '100%', // 设置高度为100%，或者可以是具体的数值，比如`height: 120`
            display: 'flex', // 使用flex布局
            alignItems: 'center', // 上下居中
            justifyContent: 'center' // 左右居中
        }}>
            <Grid container spacing={2} alignItems="center" style={{ height: '100%' }}>
                <Grid item xs={6}>
                    <LoginButton />
                </Grid>
                <Grid item xs={6}>
                    <SignUpButton />
                </Grid>
            </Grid>
        </Box>
    );
}
