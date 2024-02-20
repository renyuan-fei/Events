import React, {useState, useCallback} from 'react';
import {
    Box,
    Button,
    Typography,
    Modal,
    Paper,
    IconButton,
    CircularProgress,
    Alert // 引入Alert组件用于显示错误信息
} from '@mui/material';
import CloseIcon from '@mui/icons-material/Close';
import {useDropzone} from 'react-dropzone';
import useUploadUserPhotoMutation from "@hooks/useUploadUserPhotoMutation.ts";
import {useAppDispatch} from "@store/store.ts";
import {setAlertInfo} from "@features/commonSlice.ts";
import useUploadUserAvatarMutation from "@hooks/useUploadUserAvatarMutation.ts";
import LoadingComponent from "@ui/LoadingComponent.tsx";

const style = {
    position: 'absolute' as const,
    top: '50%',
    left: '50%',
    transform: 'translate(-50%, -50%)',
    width: 400,
    bgcolor: 'background.paper',
    boxShadow: 24,
    p: 4,
    outline: 'none',
    display: 'flex',
    flexDirection: 'column',
    alignItems: 'center',
};

interface PhotoUploadModalProps {
    userId: string;
    open: boolean;
    onClose: () => void;
    uploadType: 'avatar' | 'photo';
}

const PhotoUploadModal: React.FC<PhotoUploadModalProps> = ({userId, open, onClose,uploadType}) => {
    const dispatch = useAppDispatch();
    const { isUploading, upload } = uploadType === 'avatar' ?
        useUploadUserAvatarMutation(userId) :
        useUploadUserPhotoMutation(userId);
    const [file, setFile] = useState<File | null>(null);
    const [fileError, setFileError] = useState(false); // 新增的文件错误状态

    const handleClose = useCallback(() => {
        if (!fileError && !isUploading) {
            onClose();
        }
    }, [onClose, fileError]);

    const onDrop = useCallback((acceptedFiles: File[]) => {
        if (acceptedFiles[0].size > 5242880) { // 5MB
            setFileError(true);
            setFile(null);
            dispatch(setAlertInfo({
                open: true,
                message: "File size exceeds 5MB",
                variant: "error"
            }));
        } else {
            setFileError(false);
            setFile(acceptedFiles[0]);
        }
    }, [dispatch]);

    const {getRootProps, getInputProps, isDragActive} = useDropzone({
        onDrop,
        multiple: false
    });


    const handleUpload = () => {
        if (!file || fileError) {
            return;
        }

        const formData = new FormData();
        formData.append('File', file);

        upload(formData, {
            onSuccess() {
                setFile(null);
                setFileError(false); // 成功后清除错误状态
                onClose();
            }
        });
    };

    return (
        <>
            {isUploading && <LoadingComponent/>}
            <Modal open={open} onClose={handleClose} hideBackdrop={isUploading}>
            <Paper sx={style}>
                <IconButton onClick={handleClose}
                            disabled={isDragActive || isUploading}>
                    <CloseIcon/>
                </IconButton>
                {fileError && (
                    <Alert severity='error' sx={{width: '100%', mb: 2}}>File size exceeds
                        5MB</Alert>
                )}
                <Box {...getRootProps()} sx={{
                    p: 4,
                    border: '2px dashed grey',
                    width: '100%',
                    textAlign: 'center'
                }}>
                    <input {...getInputProps()} />
                    {isDragActive ? (
                        <Typography variant='h6'>Drop the files here ...</Typography>
                    ) : (
                        <Typography variant='h6'>Drag 'n' drop a file here, or click to
                            select a file</Typography>
                    )}
                </Box>
                <Button
                    variant='contained'
                    onClick={handleUpload}
                    disabled={isUploading || !file || isDragActive || fileError}
                    sx={{mt: 2}}
                >
                    {isUploading ? <CircularProgress size={24}/> : 'Upload Photo'}
                </Button>
                {file && (
                    <Typography variant='subtitle1' sx={{mt: 2}}>
                        Selected file: {file.name}
                    </Typography>
                )}
                <Typography variant='caption' sx={{mt: 2}}>
                    These photos must be in JPEG, GIF, or PNG format, be less than 15MB in
                    size, and adhere to the Meetup's Terms of Service.
                </Typography>
            </Paper>
        </Modal></>
    );
};

export default PhotoUploadModal;
