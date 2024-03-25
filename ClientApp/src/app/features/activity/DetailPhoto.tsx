import {useMediaQuery, useTheme} from "@mui/material";
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

const DetailPhoto: React.FC<DetailHeaderProps> = ({src, isCurrentUser, uploadHook}) => {
    const theme = useTheme();
    const isMd = useMediaQuery(theme.breakpoints.down("lg"));
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
                        width: '100%',
                        height: 400,
                        objectFit: "cover",
                        borderRadius: theme.shape.borderRadius,
                    }}
                />
                {isCurrentUser &&
                    <Button
                        variant='contained'
                        startIcon={<PhotoCamera/>}
                        onClick={handleAddPhotoClick}
                        sx={{
                            position: "absolute",
                            bottom: theme.spacing(2),
                            right: theme.spacing(2),
                            minWidth: 0,
                            padding: "6px 16px",
                            fontSize: "0.875rem",
                            backgroundColor: "rgba(0,0,0,0.5)",
                            color: "#fff",
                            "&:hover": {
                                backgroundColor: "rgba(0,0,0,0.55)",
                            },
                            textTransform: "none",
                            borderRadius: theme.shape.borderRadius,
                        }}
                    >
                        {isMd ? 'Change' : 'Change profile photo'}
                    </Button>}
            </Box>
        </>);
}

export default DetailPhoto;