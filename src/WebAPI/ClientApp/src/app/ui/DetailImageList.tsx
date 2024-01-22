import ImageList from '@mui/material/ImageList';
import ImageListItem from '@mui/material/ImageListItem';
import Box from "@mui/material/Box";
import Typography from "@mui/material/Typography";
import {useTheme} from "@mui/material";
import {useNavigate} from "react-router";
import Button from "@mui/material/Button";

// interface DetailImageListProps {
//     images: Array<string>;
// }

export default function DetailImageList() {
    const theme = useTheme();
    const navigate = useNavigate();

    // Function to handle the "See all" button click
    const handleSeeAllClick = () => {
        navigate('/all-photos'); // Navigate to the route where all photos are displayed
    };

    return (
        <Box sx={{ marginTop: theme.spacing(2), marginBottom: theme.spacing(3) }}>
            <Box sx={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', marginBottom: theme.spacing(1) }}>
                <Typography component="div" variant="h2" sx={{
                    fontSize: '20px',
                    fontWeight: theme.typography.fontWeightBold,
                    fontFamily: '"Graphik Meetup", -apple-system, sans-serif',
                }}>
                    Photos ({images.length})
                </Typography>
                <Button
                    variant={"text"}
                    onClick={handleSeeAllClick}
                    sx={{
                        color: '#00798A', // The color from the image
                        fontFamily: '"Graphik Meetup", -apple-system, sans-serif', // The font from the image
                        fontSize: '16px', // The font size from the image
                        textTransform: 'none', // Prevents uppercase transformation
                        textDecoration: 'none', // Removes underline
                    }}
                >
                    See all
                </Button>

            </Box>

            <ImageList sx={{ width: '100%', height: 'auto' }} cols={3}>
                {images.slice(0, 6).map((item, index) => (
                    <ImageListItem key={item.img} sx={{ borderRadius: theme.shape.borderRadius }}>
                        <img
                            src={`${item.img}?w=164&h=150&fit=crop&auto=format`}
                            srcSet={`${item.img}?w=164&h=150&fit=crop&auto=format&dpr=2 2x`}
                            alt={item.title}
                            loading="lazy"
                            style={{
                                width: '100%',
                                height: 'auto',
                                borderRadius: theme.shape.borderRadius,
                            }}
                        />
                        {index === 5 && images.length > 6 && (
                            <Box sx={{
                                position: 'absolute',
                                top: 0,
                                right: 0,
                                bottom: 0,
                                left: 0,
                                display: 'flex',
                                justifyContent: 'center',
                                alignItems: 'center',
                                backgroundColor: 'rgba(0, 0, 0, 0.7)',
                                borderRadius: theme.shape.borderRadius,
                            }}>
                                <Typography component="div" variant="h6" sx={{ color: 'common.white' }}>
                                    +{images.length - 6}
                                </Typography>
                            </Box>
                        )}
                    </ImageListItem>
                ))}
            </ImageList>
        </Box>
    );
}


const images = [
    {
        img: 'https://images.unsplash.com/photo-1551963831-b3b1ca40c98e',
        title: 'Breakfast',
    },
    {
        img: 'https://images.unsplash.com/photo-1551782450-a2132b4ba21d',
        title: 'Burger',
    },
    {
        img: 'https://images.unsplash.com/photo-1522770179533-24471fcdba45',
        title: 'Camera',
    },
    {
        img: 'https://images.unsplash.com/photo-1444418776041-9c7e33cc5a9c',
        title: 'Coffee',
    },
    {
        img: 'https://images.unsplash.com/photo-1533827432537-70133748f5c8',
        title: 'Hats',
    },
    {
        img: 'https://images.unsplash.com/photo-1558642452-9d2a7deb7f62',
        title: 'Honey',
    },
    {
        img: 'https://images.unsplash.com/photo-1516802273409-68526ee1bdd6',
        title: 'Basketball',
    },
    {
        img: 'https://images.unsplash.com/photo-1518756131217-31eb79b20e8f',
        title: 'Fern',
    },
    {
        img: 'https://images.unsplash.com/photo-1597645587822-e99fa5d45d25',
        title: 'Mushrooms',
    },
    {
        img: 'https://images.unsplash.com/photo-1567306301408-9b74779a11af',
        title: 'Tomato basil',
    },
    {
        img: 'https://images.unsplash.com/photo-1471357674240-e1a485acb3e1',
        title: 'Sea star',
    },
    {
        img: 'https://images.unsplash.com/photo-1589118949245-7d38baf380d6',
        title: 'Bike',
    },
];