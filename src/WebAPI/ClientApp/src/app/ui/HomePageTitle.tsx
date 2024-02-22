import Box from "@mui/material/Box";
import Typography from "@mui/material/Typography";
import {theme} from "@config/CustomTheme.ts";
import {queryClient} from "@apis/queryClient.ts";
import {userInfo} from "@type/UserInfo.ts";

const HomePageTitle = () => {

    const username = queryClient.getQueryData<userInfo>('userInfo')?.displayName;

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

export default HomePageTitle;