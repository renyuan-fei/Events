import {useTheme} from "@mui/material";
import Box from "@mui/material/Box";
import Button from "@mui/material/Button";
import {PhotoCamera} from "@mui/icons-material";
import React, {useState} from "react";
import PhotoUploadModal from "@ui/PhotoUploadModal.tsx";

interface UploadHook {
    isUploading: boolean;
    upload: (formData: FormData, callbacks: { onSuccess?: () => void }) => void;
}

interface DetailHeaderProps {
    src: string;
    isCurrentUser?: boolean;
    uploadHook: UploadHook;
}

const DetailPhoto: React.FC<DetailHeaderProps> = ({src, isCurrentUser,uploadHook}) => {
    const theme = useTheme();
    const [isUploadModalOpen, setUploadModalOpen] = useState<boolean>(false);

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
                uploadHook={uploadHook}
            />
            <Box sx={{
                position: "relative",
                width: "100%",
                marginBottom: theme.spacing(2)
            }}>
                <Box
                    component='img'
                    src={src}
                    sx={{
                        width: '100%', // 可以指定固定宽度
                        height: 400, // 可以指定固定高度
                        // width: "100%",
                        // height: "auto",
                        objectFit: "cover",
                        borderRadius: theme.shape.borderRadius, // 圆角
                    }}
                />
                {isCurrentUser && <Button
                    variant='contained'
                    startIcon={<PhotoCamera/>}
                    onClick={handleAddPhotoClick}
                    sx={{
                        position: "absolute",
                        bottom: theme.spacing(2),
                        left: "71%",
                        minWidth: 0,
                        padding: "6px 16px",
                        fontSize: "0.875rem",
                        backgroundColor: "rgba(0,0,0,0.5)",
                        color: "#fff",
                        "&:hover": {
                            backgroundColor: "rgba(0,0,0,0.55)",
                        },
                        textTransform: "none",
                        borderRadius: theme.shape.borderRadius, // 圆角
                    }}
                >
                  Change profile photo
                </Button>}
            </Box>
        </>);
}

export default DetailPhoto;