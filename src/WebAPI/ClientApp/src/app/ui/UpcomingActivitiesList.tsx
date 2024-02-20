import ActivityCard from '@features/activity/ActivityCard.tsx';
import { Grid, Typography, Box, useTheme } from '@mui/material';
import {
    useGetActivitiesQuery
} from "@features/activity/hooks/useGetPaginatedActivitiesQuery.ts";

const UpcomingActivitiesList = () => {
    const theme = useTheme();

    // 使用 useQuery 并指定返回数据的类型
    const getActivitiesQuery = useGetActivitiesQuery();

    if (getActivitiesQuery.isLoading) return <div>Loading...</div>;

    return (
        <Box sx={{ padding: theme.spacing(5) }}>
            <Typography variant={"h4"} gutterBottom sx={{ fontWeight: 700 }}>
                Upcoming events
            </Typography>
            <Grid container>
                {getActivitiesQuery.data?.items.map((activity) => (
                    <Grid item xs={12} sm={6} md={4} lg={3} key={activity.id}>
                        <ActivityCard {...activity} />
                    </Grid>
                ))}
            </Grid>
        </Box>
    );
}

export default UpcomingActivitiesList;
