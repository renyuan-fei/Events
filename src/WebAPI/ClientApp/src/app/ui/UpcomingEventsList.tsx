import {Grid, useTheme} from "@mui/material";
import Box from "@mui/material/Box";
import ActivityCard from "@ui/ActivityCard.tsx";
import Typography from "@mui/material/Typography";

export function UpcomingEventsList() {
    const theme = useTheme();

    return (
        <Box sx={{
            padding: theme.spacing(5),
        }}>
            <Typography variant={"h4"} gutterBottom sx={{
                fontWeight: 800,
            }}>
                Upcoming online events
            </Typography>
           <Grid container>
               <Grid item xs={3}>
                   <ActivityCard/>
               </Grid>
               <Grid item xs={3}>
                   <ActivityCard/>
               </Grid>
               <Grid item xs={3}>
                   <ActivityCard/>
               </Grid>
               <Grid item xs={3}>
                   <ActivityCard/>
               </Grid>


               <Grid item xs={3}>
                   <ActivityCard/>
               </Grid>
               <Grid item xs={3}>
                   <ActivityCard/>
               </Grid>
               <Grid item xs={3}>
                   <ActivityCard/>
               </Grid>
               <Grid item xs={3}>
                   <ActivityCard/>
               </Grid>

           </Grid>
        </Box>
    );
}