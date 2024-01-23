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
                        <Grid item md={4.5} sx={{ display: { sm: 'none', md: 'block' } }}>
                            <ActivitiesCalendar/>
                            <Paper sx={{ marginTop: theme.spacing(2), padding: theme.spacing(2) }}>
                                <Typography variant="h6">Your next events</Typography>
                            </Paper>
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
                        </Grid>
                    </Grid>
                </Box>

            </Box>
        </>
    );
}