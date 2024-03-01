import Box from "@mui/material/Box";
import {Grid, Paper, useTheme} from "@mui/material";
import Typography from "@mui/material/Typography";
import HomePageTitle from "@ui/HomePageTitle.tsx";
import ActivitiesCalendar from "@features/activity/ActivitiesCalendar.tsx";
import ActivitiesList from "@features/activity/ActivitiesList.tsx";
import LoadingComponent from "@ui/LoadingComponent.tsx";
import {Item} from "@type/PaginatedResponse.ts";
import PaginationComponent from "@ui/PaginationComponent.tsx";
import useGetPaginatedActivitiesQuery
    from "@features/activity/hooks/useGetPaginatedActivitiesQuery.ts";
import {useEffect} from "react";
import {useNavigate} from "react-router";
import {useLocation} from "react-router-dom";
import {useFilters} from "@hooks/useFilters.ts";
import ActivityItem from "@features/activity/ActivitiyItem.tsx";

const HomePage = () => {
    const theme = useTheme();
    const navigate = useNavigate();
    const location = useLocation();
    const pageNumber = parseInt(new URLSearchParams(location.search).get('page') || '1');
    const pageSize = parseInt(new URLSearchParams(location.search).get('pageSize') || '8');
    const filters = useFilters();

    console.log(filters);
    useEffect(() => {
        const searchParams = new URLSearchParams(location.search);
        if (!searchParams.get('page')) {
            searchParams.set('page', '1');
        }
        if (!searchParams.get('pageSize')) {
            searchParams.set('pageSize', '8');
        }
        navigate({search: searchParams.toString()}, {replace: true});
    }, [navigate]);

    const {
        isActivitiesLoading,
        activities,
        pageCount
    } = useGetPaginatedActivitiesQuery(pageNumber, pageSize, filters);

    if (isActivitiesLoading) return <LoadingComponent/>;

    const hasActivities = activities && activities.length > 0;

    return (
        <>
            <Box sx={{width: '100%'}}>
                <HomePageTitle/>
                <Box sx={{flexGrow: 1,marginTop:theme.spacing(2)}}>
                    <Grid container spacing={2}>
                        <Grid item md={4.5} sx={{
                            display: 'flex',
                            flexDirection: 'column',
                            alignItems: 'center',
                            justifyContent: 'center', // This centers vertically if there is height to work with
                            height: '100%' // Make sure the grid item takes full height
                        }}>
                            <ActivitiesCalendar/>

                        </Grid>

                        <Grid item
                              xs={12}
                              md={7.5}
                              justifyContent='center'
                              alignItems='center'>
                            <Paper sx={{padding: theme.spacing(2), marginTop: theme.spacing(2), marginBottom: theme.spacing(4)}}>
                                <ActivitiesList>
                                    {hasActivities ? (
                                        activities.map((activity: Item) => (
                                            <ActivityItem key={activity.id} {...activity} />
                                        ))
                                        ) : (
                                        <Typography sx={{ textAlign: 'center', mt: 4 }}>
                                            No activities found. Try adjusting your filters or check back later!
                                        </Typography>
                                        )}
                                </ActivitiesList>

                                {hasActivities && <PaginationComponent pageCount={pageCount} />}
                            </Paper>
                        </Grid>
                    </Grid>
                </Box>
            </Box>
        </>
    )
        ;
}

export default HomePage;