import Box from "@mui/material/Box";
import {Grid, Paper, useTheme} from "@mui/material";
import Typography from "@mui/material/Typography";
import {ActivityItem} from "@features/activity/ActivitiyItem.tsx";
import HomePageTitle from "@ui/HomePageTitle.tsx";
import ActivitiesCalendar from "@features/activity/ActivitiesCalendar.tsx";
import ActivitiesList from "@features/activity/ActivitiesList.tsx";
import {
    useGetActivitiesQuery
} from "@features/activity/hooks/useGetPaginatedActivitiesQuery.ts";
import LoadingComponent from "@ui/LoadingComponent.tsx";

const HomePage = () => {
    const theme = useTheme();

    const {isLoading,data} = useGetActivitiesQuery();

    if (isLoading) return <LoadingComponent/>;

    return (
        <>
            <Box sx={{width: '100%'}}>
                <HomePageTitle/>
                <Box sx={{ flexGrow: 1 }}>
                    <Grid container spacing={2}>
                        <Grid item md={4.5} sx={{ display: { sm: 'none', md: 'block' } }}>
                            <ActivitiesCalendar/>
                            <Paper sx={{ marginTop: theme.spacing(2), padding: theme.spacing(2) }}>
                                <Typography variant="h6">Your next events</Typography>
                            </Paper>
                        </Grid>

                        <Grid item xs={12} md={7.5} justifyContent="center" alignItems="center">
                            <Paper sx={{ padding: theme.spacing(2) , marginTop: theme.spacing(2), marginBottom: theme.spacing(4)}}>
                                <ActivitiesList>
                                    {
                                        data?.items.map((activity) => (
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

export default HomePage;