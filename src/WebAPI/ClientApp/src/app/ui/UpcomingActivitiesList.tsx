import ActivityCard from '@features/activity/ActivityCard.tsx';
import { Grid, Typography, Box, useTheme } from '@mui/material';
import {Item} from "@type/PaginatedResponse.ts";
import useGetPaginatedActivitiesQuery
    from "@features/activity/hooks/useGetPaginatedActivitiesQuery.ts";
import LoadingComponent from "@ui/LoadingComponent.tsx";
import {format} from "date-fns";

const UpcomingActivitiesList = () => {
    const theme = useTheme();

    const formattedDate = format(new Date(), 'yyyy-MM-dd');
    const {isActivitiesLoading, activities} = useGetPaginatedActivitiesQuery(1,8,{"isCancelled":"false","startDate":formattedDate});

    if (isActivitiesLoading) return <LoadingComponent/>;

    return (
        <Box sx={{ padding: theme.spacing(5) }}>
            <Typography variant={"h4"} gutterBottom sx={{ fontWeight: 700 }}>
                Upcoming events
            </Typography>
            <Grid container>
                {activities?.map((activity: Item) => (
                    <Grid item xs={12} sm={6} md={4} lg={3} key={activity.id}>
                        <ActivityCard {...activity} />
                    </Grid>
                ))}
            </Grid>
        </Box>
    );
}

export default UpcomingActivitiesList;
