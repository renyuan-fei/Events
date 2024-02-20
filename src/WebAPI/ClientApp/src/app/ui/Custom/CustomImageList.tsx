import ImageList from '@mui/material/ImageList';
import ImageListItem from '@mui/material/ImageListItem';
import Box from "@mui/material/Box";
import Typography from "@mui/material/Typography";
import {IconButton, Paper, useTheme} from "@mui/material";
import {useNavigate} from "react-router";
import Button from "@mui/material/Button";
import {Photo} from "@type/Photo.ts";
import React, {useState} from "react";
import PhotoUploadModal from "@ui/PhotoUploadModal.tsx";
import {Delete} from "@mui/icons-material";

interface CustomImageListProps {
    id: string | undefined;
    isCurrentUser: boolean
    photos: Array<Photo>;
    remainingCount: number;
}

const CustomImageList: React.FC<CustomImageListProps> = (props: CustomImageListProps) => {
    const theme = useTheme();
    const navigate = useNavigate();
    const [isUploadModalOpen, setUploadModalOpen] = useState(false);
    const {id, photos, isCurrentUser, remainingCount} = props;
    const handleSeeAllClick = () => {
        navigate(`/photos/${id}`);
    };

    const handleAddPhotosClick = () => {
        setUploadModalOpen(true);
    };

    const handleCloseModal = () => {
        setUploadModalOpen(false);
    };

    return (

        <>
            <PhotoUploadModal userId={props.id!}
                              open={isUploadModalOpen}
                              onClose={handleCloseModal}
                              uploadType={'photo'}
            />
            <Box sx={{marginTop: theme.spacing(2), marginBottom: theme.spacing(3)}}>
                <Box sx={{
                    display: 'flex',
                    justifyContent: 'space-between',
                    alignItems: 'center',
                    marginBottom: theme.spacing(1)
                }}>
                    <Typography component='div' variant='h2' sx={{
                        fontSize: '20px',
                        fontWeight: theme.typography.fontWeightBold,
                        fontFamily: '"Graphik Meetup", -apple-system, sans-serif',
                    }}>
                        Photos ({photos.length})
                    </Typography>
                    {remainingCount > 0 ? <Button
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
                        </Button> :
                        <Button
                            onClick={handleAddPhotosClick}
                            variant={"text"}
                            sx={{
                                color: '#00798A', // The color from the image
                                fontFamily: '"Graphik Meetup", -apple-system, sans-serif', // The font from the image
                                fontSize: '16px', // The font size from the image
                                textTransform: 'none', // Prevents uppercase transformation
                                textDecoration: 'none', // Removes underline
                            }}
                        >
                            + Add Photos
                        </Button>}
                </Box>

                {photos.length === 0 ?
                    (<Paper sx={{
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
                        {isCurrentUser && (
                            <Button
                                variant='outlined'
                                onClick={handleAddPhotosClick}
                                sx={{mb: 2}}
                            >
                                Add Photos
                            </Button>
                        )}
                    </Paper>)
                    : (<ImageList sx={{width: '100%', height: 'auto'}} cols={3}>
                        {photos.map((item, ) => (
                            <ImageListItem key={item.publicId}
                                           sx={{ position: 'relative', borderRadius: theme.shape.borderRadius }}>
                                <img
                                    src={`${item.url}?w=164&h=150&fit=crop&auto=format`}
                                    srcSet={`${item.url}?w=164&h=150&fit=crop&auto=format&dpr=2 2x`}
                                    alt={item.publicId}
                                    loading='lazy'
                                    style={{
                                        width: '100%',
                                        height: 'auto',
                                        borderRadius: theme.shape.borderRadius,
                                        borderWidth: 1,
                                        borderColor: theme.palette.grey[300],
                                    }}
                                />
                                {isCurrentUser && <IconButton
                                    onClick={() => {/* 添加删除图片的逻辑 */
                                    }}
                                    sx={{
                                        position: 'absolute',
                                        top: theme.spacing(1),
                                        right: theme.spacing(1),
                                        backgroundColor: 'rgba(0, 0, 0, 0.5)', // 半透明背景
                                        padding: '5px',
                                        '&:hover': {
                                            backgroundColor: 'rgba(0, 0, 0, 0.7)', // 鼠标悬停效果
                                        },
                                    }}
                                >
                                    <Delete sx={{fontSize:'1.25rem',color: 'common.white'}}/>
                                </IconButton>}
                                {/* ...其余代码 */}
                            </ImageListItem>
                        ))}

                        {/*{photos.map((item, index) => (*/}
                        {/*    <ImageListItem key={item.publicId}*/}
                        {/*                   sx={{borderRadius: theme.shape.borderRadius}}>*/}
                        {/*        <img*/}
                        {/*            src={`${item.url}?w=164&h=150&fit=crop&auto=format`}*/}
                        {/*            srcSet={`${item.url}?w=164&h=150&fit=crop&auto=format&dpr=2 2x`}*/}
                        {/*            alt={item.publicId}*/}
                        {/*            loading='lazy'*/}
                        {/*            style={{*/}
                        {/*                width: '100%',*/}
                        {/*                height: 'auto',*/}
                        {/*                borderRadius: theme.shape.borderRadius,*/}
                        {/*                borderWidth: 1,*/}
                        {/*                borderColor: theme.palette.grey[300],*/}
                        {/*            }}*/}
                        {/*        />*/}
                        {/*        {index === photos.length - 1 && remainingCount > 0 && (*/}
                        {/*            <Box sx={{*/}
                        {/*                position: 'absolute',*/}
                        {/*                top: 0,*/}
                        {/*                right: 0,*/}
                        {/*                bottom: 0,*/}
                        {/*                left: 0,*/}
                        {/*                display: 'flex',*/}
                        {/*                justifyContent: 'center',*/}
                        {/*                alignItems: 'center',*/}
                        {/*                backgroundColor: 'rgba(0, 0, 0, 0.7)',*/}
                        {/*                borderRadius: theme.shape.borderRadius,*/}
                        {/*            }}>*/}
                        {/*                <Typography component='div' variant='h6'*/}
                        {/*                            sx={{color: 'common.white'}}>*/}
                        {/*                    +{remainingCount}*/}
                        {/*                </Typography>*/}
                        {/*            </Box>*/}
                        {/*        )}*/}
                        {/*    </ImageListItem>*/}
                        {/*))}*/}
                    </ImageList>)}
            </Box>
        </>
    );
}

export default CustomImageList;