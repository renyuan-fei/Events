import {Grid, Typography, useTheme} from '@mui/material';
import Icon from '@mui/material/Icon';
import Box from "@mui/material/Box";
import {useSelector} from "react-redux";
import {RootState} from "@store/store.ts"; // 用于示例，您需要根据实际情况使用适当的图标

// 假设的类别数据，您需要用实际的数据替换它
const categories = [
    {name: 'Travel and Outdoor', icon: 'flight_takeoff', color: '#44755c'},
    {name: 'Social Activities', icon: 'people', color: '#702546'},
    {name: 'Hobbies and Passions', icon: 'palette', color: '#ea7558'},
    {name: 'Sports and Fitness', icon: 'fitness_center', color: '#44755c'},
    {name: 'Health and Wellbeing', icon: 'spa', color: '#388072'},
    {name: 'Technology', icon: 'computer', color: '#44755c'},
    {name: 'Art and Culture', icon: 'theater_comedy', color: '#8d5b74'},
    {name: 'Games', icon: 'videogame_asset', color: '#f37558'},
];

const ExploreCategories = () => {
    const theme = useTheme();
    const isMobile = useSelector((state: RootState) => state.common.isMobile);

    return (
        <Box sx={{my: theme.spacing(5)}}>
            <Typography variant={isMobile ? "h5" : "h4"} gutterBottom
                        sx={{fontWeight: 700}}>
                Explore top categories
            </Typography>
            <Grid container spacing={2} justifyContent="center">
                {categories.map((category) => (
                    <Grid item key={category.name} xs={6} sm={6} md={3} lg={1.5}>
                        <Box
                            sx={{
                                display: 'flex',
                                flexDirection: 'column',
                                alignItems: 'center',
                                justifyContent: 'center',
                                height: 122,
                                width: {xs: '100%', sm: '100%', md: '100'},
                                backgroundColor: '#97CAD114',
                                borderRadius: 2.5,
                            }}
                        >
                            <Icon sx={{
                                color: category.color,
                                fontSize: 'large',
                                mb: 1
                            }}>{category.icon}</Icon>
                            <Typography variant="caption"
                                        sx={{fontWeight: 700}}>{category.name}</Typography>
                        </Box>
                    </Grid>
                ))}
            </Grid>
        </Box>
    );
}

export default ExploreCategories;
