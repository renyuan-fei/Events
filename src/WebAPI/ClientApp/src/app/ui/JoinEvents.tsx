import {Box, Button, Typography, useTheme} from '@mui/material';
import JoinEventsImg from "@assets/JoinEventsImg.png";
import {ImageComp} from "@ui/Image.tsx";

export default function JoinEvents() {
    const theme = useTheme();

    return (
        <Box
            sx={{
                display: 'flex',
                flexDirection: { xs: 'column', sm: 'row' }, // 响应式布局方向
                justifyContent: 'space-between',
                alignItems: 'center',
                padding: theme.spacing(5),
                my: theme.spacing(10),
                backgroundColor: '#97CAD114',
                borderRadius: 2.5,
                [theme.breakpoints.down('xs')]: {
                    padding: theme.spacing(3), // 小屏幕上减小内边距
                },
            }}
        >
            <Box sx={{
                [theme.breakpoints.down('xs')]: {
                    textAlign: 'center', // 小屏幕上文本居中
                },
            }}>
                <Typography variant="h4" component="h2" gutterBottom sx={{
                    fontWeight: 700,
                    [theme.breakpoints.down('xs')]: {
                        fontSize: '1.5rem', // 小屏幕上减小标题字体大小
                    },
                }}>
                    Join Events
                </Typography>
                <Typography variant="body1" sx={{
                    [theme.breakpoints.down('xs')]: {
                        fontSize: '0.875rem', // 小屏幕上减小正文字体大小
                    },
                }}>
                    People use Events to meet new people, learn new things, find support, get out of their
                    comfort zones, and pursue their passions, together. Membership is free.
                </Typography>
                <Button variant="contained" color="secondary" sx={{
                    marginTop: theme.spacing(2),
                    backgroundColor: '#e32359',
                    width: { xs: '100%', sm: 192 },
                    height: 48,
                    fontWeight: 700,
                    borderRadius: 3,
                    [theme.breakpoints.down('xs')]: {
                        width: '100%', // 小屏幕上按钮宽度100%
                    },
                }}>
                    Sign up
                </Button>
            </Box>

            {/* 使用 display 属性在小屏幕上隐藏图片 */}
            <Box>
                <ImageComp Src={JoinEventsImg} Alt={"JoinEvents"} Sx={{
                    width: 500,
                    height: 250,
                }}/>
            </Box>
        </Box>
    );
}