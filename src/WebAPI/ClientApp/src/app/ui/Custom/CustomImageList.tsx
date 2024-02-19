import ImageList from '@mui/material/ImageList';
import ImageListItem from '@mui/material/ImageListItem';
import Box from "@mui/material/Box";
import Typography from "@mui/material/Typography";
import {Paper, useTheme} from "@mui/material";
import {useNavigate} from "react-router";
import Button from "@mui/material/Button";
import {Photo} from "@type/Photo.ts";

interface CustomImageListProps {
    Id: string | undefined;
    isUser: boolean
    images: Array<Photo>;
}

const CustomImageList = (props: CustomImageListProps) => {
    const theme = useTheme();
    const navigate = useNavigate();
    const {Id, images, isUser} = props;

    const handleSeeAllClick = () => {
        navigate(`/photos/${Id}`);
    };

    return (
        <Box sx={{marginTop: theme.spacing(2), marginBottom: theme.spacing(3)}}>
            <Box sx={{
                display: 'flex',
                justifyContent: 'space-between',
                alignItems: 'center',
                marginBottom: theme.spacing(1)
            }}>
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

            {images.length === 0 && <Paper sx={{
                display: 'flex',
                flexDirection: 'column',
                justifyContent: 'center', // 使内容在Paper中垂直居中
                alignItems: 'center',
                height: 150,
                padding: theme.spacing(4)
            }}>
                <Typography sx={{mb: 2}}
                            variant={"h6"}
                            component={"div"}>
                    No photos available
                </Typography>
                {isUser && (
                    <Button
                        variant="outlined"
                        onClick={() => navigate(`/photos/upload`)} // 替换为上传照片的实际路由
                        sx={{mb: 2}}
                    >
                        Add Photos
                    </Button>
                )}
            </Paper>}

            <ImageList sx={{width: '100%', height: 'auto'}} cols={3}>
                {images.slice(0, 6).map((item, index) => (
                    <ImageListItem key={item.publicId}
                                   sx={{borderRadius: theme.shape.borderRadius}}>
                        <img
                            src={`${item.url}?w=164&h=150&fit=crop&auto=format`}
                            srcSet={`${item.url}?w=164&h=150&fit=crop&auto=format&dpr=2 2x`}
                            alt={item.publicId}
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
                                <Typography component="div" variant="h6"
                                            sx={{color: 'common.white'}}>
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

export default CustomImageList;