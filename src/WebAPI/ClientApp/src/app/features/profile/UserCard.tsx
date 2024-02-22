import CardMedia from "@mui/material/CardMedia";
import CardContent from "@mui/material/CardContent";
import {Stack, useTheme} from "@mui/material";
import Divider from "@mui/material/Divider";
import Box from "@mui/material/Box";
import Typography from "@mui/material/Typography";
import Button from "@mui/material/Button";
import Card from "@mui/material/Card";
import {PhotoCamera} from "@mui/icons-material";
import PhotoUploadModal from "@ui/PhotoUploadModal.tsx";
import {useState} from "react";
import useUploadUserAvatarMutation from "@hooks/useUploadUserAvatarMutation.ts";
import useFollowMutation from "@features/follow/hooks/useFollowMutation.ts";
import useUnfollowMutation from "@features/follow/hooks/useUnfollowMutation.ts";

interface UserCardProps {
    id: string;
    image: string;
    isCurrentUser: boolean;
    isFollowed?: boolean;
    followers: number;
    following: number;
}

const UserCard = (props: UserCardProps) => {
    const theme = useTheme();
    const [isUploadModalOpen, setUploadModalOpen] = useState<boolean>(false);
    const {id, image, isCurrentUser, isFollowed, following, followers} = props;
    const {isFollowing, follow} = useFollowMutation(id);
    const {isUnfollowing, unfollow} = useUnfollowMutation(id);

    const handleFollowClick = async () => {
        await follow();
    }

    const handleUnfollowClick = async () => {
        await unfollow();
    }

    const handleAddPhotoClick = () => {
        setUploadModalOpen(true);
    };

    const handleCloseModal = () => {
        setUploadModalOpen(false);
    };

    return (
        <>
            <PhotoUploadModal
                open={isUploadModalOpen}
                onClose={handleCloseModal}
                uploadHook={useUploadUserAvatarMutation(id)}
            />
            <Card sx={{
                width: 400,
                height: 632,
                padding: theme.spacing(4),
                borderRadius: theme.shape.borderRadius,
                position: 'relative'
            }}>
                <CardMedia
                    image={image}
                    title={"Profile Picture"}
                    sx={{
                        borderRadius: theme.shape.borderRadius,
                        objectFit: 'cover',
                        width: 336,
                        height: 403,
                        position: 'relative', // 确保Media组件相对定位
                    }}/>
                {isCurrentUser && ( // 仅当isCurrentUser为true时显示按钮
                    <Button
                        variant='contained'
                        startIcon={<PhotoCamera/>}
                        sx={{
                            position: 'absolute',
                            top: theme.spacing(4), // 顶部间距
                            right: theme.spacing(4.5), // 右侧间距
                            minWidth: 0, // 最小宽度
                            padding: '6px 16px', // 内边距
                            fontSize: '0.875rem', // 字体大小
                            backgroundColor: 'rgba(0,0,0,0.5)', // 背景颜色
                            color: '#fff', // 文本颜色
                            '&:hover': {
                                backgroundColor: 'rgba(0,0,0,0.55)', // 鼠标悬停效果
                            },
                            textTransform: 'none', // 防止字母大写
                        }}
                        onClick={handleAddPhotoClick}
                    >
                        Change profile photo
                    </Button>
                )}
                <CardContent sx={{
                    width: 336,
                    marginTop: theme.spacing(2),
                }}>
                    <Stack
                        direction='row'
                        divider={<Divider orientation='vertical' flexItem/>}
                        spacing={2}
                    >

                        <Box sx={{textAlign: 'center', width: '33%'}}>
                            <Typography variant='h6'
                                        component={"div"}
                                        sx={{
                                            fontWeight: theme.typography.fontWeightBold,
                                            fontSize: 30
                                            , height: 36
                                        }}>{followers}</Typography>
                            <Typography variant='subtitle1'
                                        component={"div"}>Follower</Typography>
                        </Box>

                        <Box sx={{textAlign: 'center', width: '33%'}}>
                            <Typography variant='h6'
                                        component={"div"}
                                        sx={{
                                            fontWeight: theme.typography.fontWeightBold,
                                            fontSize: 30
                                            , height: 36
                                        }}>{following}</Typography>
                            <Typography variant='subtitle1'
                                        component={"div"}>Following</Typography>
                        </Box>

                        <Box sx={{textAlign: 'center', width: '33%'}}>
                            <Typography variant='h6'
                                        component={"div"}
                                        sx={{
                                            fontWeight: theme.typography.fontWeightBold,
                                            fontSize: 30
                                            , height: 36
                                        }}>11</Typography>
                            <Typography variant='subtitle1'
                                        component={"div"}>Events</Typography>
                        </Box>

                    </Stack>
                    <Box sx={{
                        display: 'flex',
                        justifyContent: 'center',
                        alignItems: 'center',
                        marginTop: theme.spacing(1.5)
                    }}>
                        {isCurrentUser ? (
                            <Button
                                variant='outlined'
                                color='primary'
                                sx={{width: 280, height: 44, fontSize: 20}}
                            >
                                Edit Profile
                            </Button>
                        ) : isFollowed ? (
                            <Button
                                onClick={handleUnfollowClick}
                                disabled={isUnfollowing}
                                variant='outlined'
                                color='primary'
                                sx={{width: 280, height: 44, fontSize: 20}}
                            >
                                Unfollow
                            </Button>
                        ) : (
                            <Button
                                onClick={handleFollowClick}
                                disabled={isFollowing}
                                variant='contained'
                                color='secondary'
                                sx={{width: 280, height: 44, fontSize: 20}}
                            >
                                Follow
                            </Button>
                        )}

                    </Box>
                </CardContent>
            </Card></>
    );
}

export default UserCard;