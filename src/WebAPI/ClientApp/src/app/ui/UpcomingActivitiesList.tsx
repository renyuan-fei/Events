import { GetActivities } from '@apis/Activities.ts';
import ActivityCard from '@features/activity/ActivityCard.tsx';
import { Grid, Typography, Box, useTheme } from '@mui/material';
import {useQuery} from "react-query";

export function UpcomingActivitiesList() {
    const theme = useTheme();

    // 使用 useQuery 并指定返回数据的类型
    const { data, isLoading, error } = useQuery<paginatedResponse, Error>(['upcomingActivities', 1, 8], () => GetActivities({ page: 1, pageSize: 8 }));

    if (isLoading) return <div>Loading...</div>;
    if (error) return <div>Error: {error.message}</div>;

    return (
        <Box sx={{ padding: theme.spacing(5) }}>
            <Typography variant={"h4"} gutterBottom sx={{ fontWeight: 700 }}>
                Upcoming events
            </Typography>
            <Grid container>
                {data?.items.map((activity) => (
                    <Grid item xs={12} sm={6} md={4} lg={3} key={activity.id}>
                        <ActivityCard {...activity} />
                    </Grid>
                ))}
            </Grid>
        </Box>
    );
}
