import Box from "@mui/material/Box";
import {Grid, Paper, useTheme} from "@mui/material";
import Typography from "@mui/material/Typography";
import {ActivityItem} from "@features/activity/ActivitiyItem.tsx";
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

const HomePage = () => {
    const theme = useTheme();
    const navigate = useNavigate();
    const location = useLocation();

    useEffect(() => {
        const searchParams = new URLSearchParams(location.search);
        if (!searchParams.get('page')) {
            searchParams.set('page', '1');
        }
        if (!searchParams.get('pageSize')) {
            searchParams.set('pageSize', '10');
        }
        if (!searchParams.get('category')) {
            searchParams.set('category', '');
        }
        if (!searchParams.get('startDate')) {
            searchParams.set('startDate', '');
        }
        navigate({ search: searchParams.toString() }, { replace: true });
    }, [navigate]);


    const {isActivitiesLoading, activities,pageCount} = useGetPaginatedActivitiesQuery();

    if (isActivitiesLoading) return <LoadingComponent/>;

    return (
        <>
            <Box sx={{width: '100%'}}>
                <HomePageTitle/>
                <Box sx={{flexGrow: 1}}>
                    <Grid container spacing={2}>
                        <Grid item md={4.5} sx={{display: {sm: 'none', md: 'block'}}}>
                            <ActivitiesCalendar/>
                            <Paper sx={{
                                marginTop: theme.spacing(2),
                                padding: theme.spacing(2)
                            }}>
                                <Typography variant='h6'>Your next events</Typography>
                            </Paper>
                        </Grid>

                        <Grid item
                              xs={12}
                              md={7.5}
                              justifyContent='center'
                              alignItems='center'>
                            <Paper sx={{
                                padding: theme.spacing(2),
                                marginTop: theme.spacing(2),
                                marginBottom: theme.spacing(4)
                            }}>
                                <ActivitiesList>
                                    {
                                        activities?.map((activity: Item) => (
                                            <ActivityItem key={activity.id} {...activity}/>
                                        ))
                                    }
                                </ActivitiesList>
                                <PaginationComponent pageCount={pageCount}/>
                            </Paper>
                        </Grid>
                    </Grid>
                </Box>

            </Box>
        </>
    );
}

export default HomePage;