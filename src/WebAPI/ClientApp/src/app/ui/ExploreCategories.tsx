import {Grid, Typography, useTheme} from '@mui/material';
import Icon from '@mui/material/Icon';
import Box from "@mui/material/Box"; // 用于示例，您需要根据实际情况使用适当的图标

// 假设的类别数据，您需要用实际的数据替换它
const categories = [
    { name: 'Travel and Outdoor', icon: 'flight_takeoff', color: '#44755c' },
    { name: 'Social Activities', icon: 'people' , color: '#702546'},
    { name: 'Hobbies and Passions', icon: 'palette' ,color: '#ea7558'},
    { name: 'Sports and Fitness', icon: 'fitness_center' , color: '#44755c' },
    { name: 'Health and Wellbeing', icon: 'spa' ,color: '#388072'},
    { name: 'Technology', icon: 'computer' ,color: '#44755c'},
    { name: 'Art and Culture', icon: 'theater_comedy' ,color: '#8d5b74'},
    { name: 'Games', icon: 'videogame_asset' ,color: '#f37558'},
];

export default function ExploreCategories() {
    const theme = useTheme();

    return (
        <Box sx={{
            my: theme.spacing(5),
        }}>
            <Typography variant="h4" gutterBottom sx={{fontWeight: 700 }}>
                Explore top categories
            </Typography>
            <Grid container spacing={2} justifyContent="center" sx={{
                mt: theme.spacing(2),
            }}>
                {categories.map((category) => (
                    <Grid item key={category.name} xs={1.5}>
                        <Box
                            sx={{
                                display: 'flex',
                                flexDirection: 'column',
                                alignItems: 'center',
                                justifyContent: 'center',
                                height: 122,
                                width: 155,
                                backgroundColor: '#97CAD114',
                                borderRadius: 2.5,
                            }}
                        >
                            <Icon sx={{color: category.color, paddingBottom:5}}>{category.icon}</Icon>
                            <Typography variant="caption" sx={{fontWeight:700}}>{category.name}</Typography>
                        </Box>
                    </Grid>
                ))}
            </Grid>
        </Box>
    );
}
