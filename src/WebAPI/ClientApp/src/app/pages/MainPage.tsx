import Intro from "@ui/Intro.tsx";
import JoinEvents from "@ui/JoinEvents.tsx";
import Box from "@mui/material/Box";
import ExploreCategories from "@ui/ExploreCategories.tsx";
import {UpcomingEventsList} from "@ui/UpcomingEventsList.tsx";
export default function MainPage() {

    return (
        <Box sx={{
        }}>
            <Intro/>
            <UpcomingEventsList/>
            <JoinEvents/>
            <ExploreCategories/>
        </Box>
    );
}
