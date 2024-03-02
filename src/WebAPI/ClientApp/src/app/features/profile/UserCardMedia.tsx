import CardMedia from "@mui/material/CardMedia";
import Button from "@mui/material/Button";
import {PhotoCamera} from "@mui/icons-material";
import {useTheme} from "@mui/material";
import {useState} from "react";
import useUploadUserAvatarMutation from "@hooks/useUploadUserAvatarMutation.ts";
import PhotoUploadModal from "@ui/PhotoUploadModal.tsx";
import {userInfo} from "@type/UserInfo.ts";
import {queryClient} from "@apis/queryClient.ts";

interface UserCardMediaProps {
    isCurrentUser: boolean;
    image: string;
}

const UserCardMedia: React.FC<UserCardMediaProps> = ({isCurrentUser, image}) => {
    const theme = useTheme();
    const [isUploadModalOpen, setUploadModalOpen] = useState<boolean>(false);
    const id = queryClient.getQueryData<userInfo>('userInfo')?.id!;

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
        </>
    );
};

export default UserCardMedia;