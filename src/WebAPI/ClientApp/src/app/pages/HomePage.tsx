import Box from "@mui/material/Box";
import {HomePageTitle} from "@ui/HomePageTitle.tsx";
import {Grid, Paper} from "@mui/material";
import Typography from "@mui/material/Typography";
import {ActivitiesCalendar} from "@features/activity/ActivitiesCalendar.tsx";
import {ActivitiesList} from "@features/activity/ActivitiesList.tsx";
import {ActivitiyItem} from "@features/activity/ActivitiyItem.tsx";

export function HomePage() {
    return (
        <>
            <Box sx={{width: '100%'}}>
                <HomePageTitle/>
                <Box sx={{ flexGrow: 1 }}>
                    <Grid container spacing={2}>
                        {/* 左侧栏 */}
                        <Grid item xs={12} md={4}>
                            <ActivitiesCalendar/>
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
                                <ActivitiesList>
                                    <ActivitiyItem/>
                                </ActivitiesList>
                            </Paper>
                            {/* ...更多右侧内容... */}
                        </Grid>
                    </Grid>
                </Box>

            </Box>
        </>
    );
}