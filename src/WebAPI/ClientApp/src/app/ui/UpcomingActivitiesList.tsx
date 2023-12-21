import {Grid, useTheme} from "@mui/material";
import Box from "@mui/material/Box";
import ActivityCard from "@ui/ActivityCard.tsx";
import Typography from "@mui/material/Typography";

export function UpcomingActivitiesList() {
    const theme = useTheme();

    return (
        <Box sx={{
            padding: theme.spacing(5),
        }}>
            <Typography variant={"h4"} gutterBottom sx={{
                fontWeight: 700,
            }}>
                Upcoming online events
            </Typography>
           <Grid container>
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
        </Box>
    );
}