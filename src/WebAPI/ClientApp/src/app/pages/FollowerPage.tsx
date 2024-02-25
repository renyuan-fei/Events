import {FollowerList} from "@features/follow/FollowerList.tsx";
import {useParams} from "react-router";
import {useSearchParams} from "react-router-dom";
import {Box, Typography, useTheme} from "@mui/material";
import Card from "@mui/material/Card";

export const FollowerPage = () => {
    const theme = useTheme();
    const [searchParams] = useSearchParams();
    const {userId} = useParams<{ userId: string }>();
    const page = Number(searchParams.get("page")) || 1;
    const pageSize = Number(searchParams.get("pageSize")) || 10;

    return (
        <Box sx={{
            display: 'flex', // 使用flex布局
            justifyContent: 'center', // 水平居中
            alignItems: 'center', // 垂直居中
            p: theme.spacing(2) // 主题间距作为padding
        }}>
            <Card sx={{
                borderRadius: theme.shape.borderRadius,
                width: '36%',
                height: 'auto', // 高度根据内容自适应
                marginY: 4, // 上下外边距
                padding: 2, // 内边距
                display: 'flex', // 开启flex布局
                flexDirection: 'column', // 垂直方向布局
            }}>
                <Typography variant="h4" component="h1" sx={{ textAlign: 'center', mb: 4 }}>
                    Followers
                </Typography>
                <Box sx={{ flex: 1, display: 'flex', justifyContent: 'center' }}>
                    <FollowerList userId={userId!} page={page} pageSize={pageSize}/>
                </Box>
            </Card>
        </Box>
    );

};