import Box from "@mui/material/Box";
import {HomePageTitle} from "@ui/HomePageTitle.tsx";
import {Grid, Paper} from "@mui/material";
import Typography from "@mui/material/Typography";

export function HomePage() {
    return (
        <>
            <Box sx={{width: '100%'}}>
                <HomePageTitle/>
                <Box sx={{ flexGrow: 1 }}>
                    <Grid container spacing={2}>
                        {/* 左侧栏 */}
                        <Grid item xs={12} md={4}>
                            <Paper sx={{ padding: 2 }}>
                                {/* 这里放置日历组件 */}
                                <Typography variant="h6">Calendar</Typography>
                                {/* ...其他控件... */}
                            </Paper>
                            {/* 其他左侧内容 */}
                            <Paper sx={{ marginTop: 2, padding: 2 }}>
                                {/* 这里放置其他组件，如 "Your next events" */}
                                <Typography variant="h6">Your next events</Typography>
                                {/* ... */}
                            </Paper>
                            {/* ...更多左侧内容... */}
                        </Grid>

                        {/* 右侧栏 */}
                        <Grid item xs={12} md={8}>
                            <Paper sx={{ padding: 2 }}>
                                {/* 这里放置事件列表 */}
                                <Typography variant="h6">Today</Typography>
                                {/* ...事件列表项... */}
                            </Paper>
                            {/* ...更多右侧内容... */}
                        </Grid>
                    </Grid>
                </Box>

            </Box>
        </>
    );
}