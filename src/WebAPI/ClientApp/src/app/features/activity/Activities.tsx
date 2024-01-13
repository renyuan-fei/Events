// import {useQuery} from 'react-query';
// import {GetActivities} from "@apis/Activities.ts";
// import {Loading} from "@ui/Loading.tsx";
// import {ErrorMessage} from "@ui/ErrorMessage.tsx";
// import {Activity} from "@models/Activity.ts";

import {Grid} from "@mui/material";
import ActivityCard from "@features/activity/ActivityCard.tsx";

export function Activities() {
    // const {
    //     isLoading,
    //     isError,
    //     // data,
    //     error
    // } = useQuery<Activity[], Error>({
    //     queryKey : "activities",
    //     queryFn : () => GetActivities()
    // });
    //
    // if (isLoading) {
    //     return <Loading/>;
    // }
    //
    // if (isError) {
    //     return <ErrorMessage Error={error as Error}/>;
    // }

    return (
        // <div>
        //     {data?.map((activity: Activity) => (
        //         <div key={activity.Id}>
        //             <h2>{activity.Title}</h2>
        //             <p>{activity.Description}</p>
        //         </div>
        //     ))}
        // </div>
        <Grid container spacing={4}>
            <Grid item xs={12} sm={6} md={4} lg={3}>
                <ActivityCard/>
            </Grid>
            <Grid item xs={12} sm={6} md={4} lg={3}>
                <ActivityCard/>
            </Grid>
            <Grid item xs={12} sm={6} md={4} lg={3}>
                <ActivityCard/>
            </Grid>
            <Grid item xs={12} sm={6} md={4} lg={3}>
                <ActivityCard/>
            </Grid>
            <Grid item xs={12} sm={6} md={4} lg={3}>
                <ActivityCard/>
            </Grid>
            <Grid item xs={12} sm={6} md={4} lg={3}>
                <ActivityCard/>
            </Grid>
            <Grid item xs={12} sm={6} md={4} lg={3}>
                <ActivityCard/>
            </Grid>
            <Grid item xs={12} sm={6} md={4} lg={3}>
                <ActivityCard/>
            </Grid>
            <Grid item xs={12} sm={6} md={4} lg={3}>
                <ActivityCard/>
            </Grid>
            <Grid item xs={12} sm={6} md={4} lg={3}>
                <ActivityCard/>
            </Grid>
            <Grid item xs={12} sm={6} md={4} lg={3}>
                <ActivityCard/>
            </Grid>
            <Grid item xs={12} sm={6} md={4} lg={3}>
                <ActivityCard/>
            </Grid>
            <Grid item xs={12} sm={6} md={4} lg={3}>
                <ActivityCard/>
            </Grid>
            <Grid item xs={12} sm={6} md={4} lg={3}>
                <ActivityCard/>
            </Grid>
            <Grid item xs={12} sm={6} md={4} lg={3}>
                <ActivityCard/>
            </Grid>
            <Grid item xs={12} sm={6} md={4} lg={3}>
                <ActivityCard/>
            </Grid>
            <Grid item xs={12} sm={6} md={4} lg={3}>
                <ActivityCard/>
            </Grid>
            <Grid item xs={12} sm={6} md={4} lg={3}>
                <ActivityCard/>
            </Grid>
            <Grid item xs={12} sm={6} md={4} lg={3}>
                <ActivityCard/>
            </Grid>
            <Grid item xs={12} sm={6} md={4} lg={3}>
                <ActivityCard/>
            </Grid>
            <Grid item xs={12} sm={6} md={4} lg={3}>
                <ActivityCard/>
            </Grid>
            <Grid item xs={12} sm={6} md={4} lg={3}>
                <ActivityCard/>
            </Grid>
            <Grid item xs={12} sm={6} md={4} lg={3}>
                <ActivityCard/>
            </Grid>
            <Grid item xs={12} sm={6} md={4} lg={3}>
                <ActivityCard/>
            </Grid>
            <Grid item xs={12} sm={6} md={4} lg={3}>
                <ActivityCard/>
            </Grid>
            <Grid item xs={12} sm={6} md={4} lg={3}>
                <ActivityCard/>
            </Grid>
            <Grid item xs={12} sm={6} md={4} lg={3}>
                <ActivityCard/>
            </Grid>
            <Grid item xs={12} sm={6} md={4} lg={3}>
                <ActivityCard/>
            </Grid>
            <Grid item xs={12} sm={6} md={4} lg={3}>
                <ActivityCard/>
            </Grid>
            <Grid item xs={12} sm={6} md={4} lg={3}>
                <ActivityCard/>
            </Grid>
            <Grid item xs={12} sm={6} md={4} lg={3}>
                <ActivityCard/>
            </Grid>
            <Grid item xs={12} sm={6} md={4} lg={3}>
                <ActivityCard/>
            </Grid>
            <Grid item xs={12} sm={6} md={4} lg={3}>
                <ActivityCard/>
            </Grid>
            <Grid item xs={12} sm={6} md={4} lg={3}>
                <ActivityCard/>
            </Grid>
            <Grid item xs={12} sm={6} md={4} lg={3}>
                <ActivityCard/>
            </Grid>
            <Grid item xs={12} sm={6} md={4} lg={3}>
                <ActivityCard/>
            </Grid>
            <Grid item xs={12} sm={6} md={4} lg={3}>
                <ActivityCard/>
            </Grid>
        </Grid>
    );
}
