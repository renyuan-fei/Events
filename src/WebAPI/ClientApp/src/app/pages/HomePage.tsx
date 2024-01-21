import Box from "@mui/material/Box";
import {HomePageTitle} from "@ui/HomePageTitle.tsx";
import {Grid, Paper, useTheme} from "@mui/material";
import Typography from "@mui/material/Typography";
import {ActivitiesCalendar} from "@features/activity/ActivitiesCalendar.tsx";
import {ActivitiesList} from "@features/activity/ActivitiesList.tsx";
import {useGetActivitiesQuery} from "@apis/Activities.ts";
import {ActivityItem} from "@features/activity/ActivitiyItem.tsx";

export function HomePage() {
    const theme = useTheme();

    const getActivitiesQuery = useGetActivitiesQuery();

    if (getActivitiesQuery.isLoading) return <div>Loading...</div>;

    return (
        <>
            <Box sx={{width: '100%'}}>
                <HomePageTitle/>
                <Box sx={{ flexGrow: 1 }}>
                    <Grid container spacing={2}>
                        {/* 左侧栏 */}
                        <Grid item md={4.5} sx={{ display: { xs: 'none', md: 'block' } }}>
                            <ActivitiesCalendar/>
                            {/* 其他左侧内容 */}
                            <Paper sx={{ marginTop: theme.spacing(2), padding: theme.spacing(2) }}>
                                {/* 这里放置其他组件，如 "Your next events" */}
                                <Typography variant="h6">Your next events</Typography>
                                {/* ... */}
                            </Paper>
                            {/* ...更多左侧内容... */}
                        </Grid>

                        {/* 右侧栏 */}
                        <Grid item xs={12} md={7.5} justifyContent="center" alignItems="center">
                            <Paper sx={{ padding: theme.spacing(2) , marginTop: theme.spacing(2), marginBottom: theme.spacing(4)}}>
                                <ActivitiesList>
                                    {
                                        getActivitiesQuery.data?.items.map((activity) => (
                                            <ActivityItem key={activity.id} {...activity}/>
                                        ))
                                    }
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