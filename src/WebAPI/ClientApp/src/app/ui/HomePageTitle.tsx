import Box from "@mui/material/Box";
import Typography from "@mui/material/Typography";
import {RootState} from "@store/store.ts";
import {useSelector} from "react-redux";
import {theme} from "@config/CustomTheme.ts";

export function HomePageTitle() {

    const username = useSelector((state: RootState) => state.user.userName);
    return (
        <Box>
            <Typography variant="h1" component="h1" sx={{mb: theme.spacing(4), font: '42px', fontWeight: theme.typography.fontWeightBold}}>
                Welcome, {username}👋
            </Typography>

            <Typography variant="h2" component={"h2"} sx={{font:'24px', fontWeight: theme.typography.fontWeightBold}}>
                Explore the events!
            </Typography>
        </Box>
    );
}