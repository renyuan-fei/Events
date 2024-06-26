import Intro from "@ui/Intro.tsx";
import JoinEvents from "@ui/JoinEvents.tsx";
import Box from "@mui/material/Box";
import ExploreCategories from "@ui/ExploreCategories.tsx";
import UpcomingActivitiesList from "@ui/UpcomingActivitiesList.tsx";

const MainPage = () => {

    return (
        <Box sx={{width: '100%'}}>
            <Intro/>
            <UpcomingActivitiesList/>
            <JoinEvents/>
            <ExploreCategories/>
        </Box>
    );
}

export default MainPage;
