import React, {useState, useCallback} from 'react';
import {
    Box,
    Typography,
    Modal,
    IconButton,
    Alert, CardHeader // 引入Alert组件用于显示错误信息
} from '@mui/material';
import CloseIcon from '@mui/icons-material/Close';
import {useDropzone} from 'react-dropzone';
import {useAppDispatch} from "@store/store.ts";
import {setAlertInfo} from "@features/commonSlice.ts";
import LoadingComponent from "@ui/LoadingComponent.tsx";
import {LoadingButton} from "@mui/lab";
import Card from "@mui/material/Card";
import CardContent from "@mui/material/CardContent";

const style = {
    position: 'absolute' as const,
    top: '50%',
    left: '50%',
    transform: 'translate(-50%, -50%)',
    width: 400,
    bgcolor: 'background.paper',
    boxShadow: 24,
    padding: 2,
    outline: 'none',
    display: 'flex',
    flexDirection: 'column',
    alignItems: 'center',
    borderRadius: 2
};

interface UploadHook {
    isUploading: boolean;
    upload: (formData: FormData, callbacks: { onSuccess?: () => void }) => void;
}

interface PhotoUploadModalProps {
    open: boolean;
    onClose: () => void;
    uploadHook: UploadHook;
}

const PhotoUploadModal: React.FC<PhotoUploadModalProps> = ({open, onClose, uploadHook }) => {
    const dispatch = useAppDispatch();
    const { isUploading, upload } = uploadHook;
    const [file, setFile] = useState<File | null>(null);
    const [fileError, setFileError] = useState(false);
    const [preview, setPreview] = useState<string | null>(null);


    const handleClose = useCallback(() => {
        if (!fileError && !isUploading) {
            onClose();
        }
    }, [onClose, fileError, isUploading]);

    const onDrop = useCallback((acceptedFiles: File[]) => {
        if (acceptedFiles[0].size > 5242880) { // 5MB
            setFileError(true);
            setFile(null);
            dispatch(setAlertInfo({
                open: true,
                message: "File size exceeds 5MB",
                severity: "error"
            }));
        } else {
            setFileError(false);
            setFile(acceptedFiles[0]);
            setPreview(URL.createObjectURL(acceptedFiles[0]));
        }
    }, [dispatch]);

    const { getRootProps, getInputProps, isDragActive } = useDropzone({
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
                setFileError(false);
                onClose();
            }
        });
    };

    return (
        <>
            {isUploading && <LoadingComponent/>}
                <Modal open={open} onClose={handleClose} hideBackdrop={isUploading}>
                    <Card sx={style}>
                        <Box flexGrow={1}>
                            <CardHeader
                                action={
                                    <IconButton onClick={handleClose}
                                                disabled={isDragActive || isUploading}>
                                        <CloseIcon />
                                    </IconButton>
                                }
                            />
                            <CardContent>
                                {fileError && (
                                    <Alert severity='error' sx={{ width: '100%', mb: 2 }}>File size exceeds 5MB</Alert>
                                )}
                                <Box {...getRootProps()} sx={{
                                    p: 4,
                                    border: '2px dashed grey',
                                    width: '100%',
                                    textAlign: 'center'
                                }}>
                                    <input {...getInputProps()} />
                                    {preview ? (
                                        <img src={preview} style={{ width: '100%', objectFit: 'cover' }} alt="Upload preview" />
                                    ) : isDragActive ? (
                                        <Typography variant='h6'>Drop the files here ...</Typography>
                                    ) : (
                                        <Typography variant='h6'>Drag your image here or click to select an image</Typography>
                                    )}
                                </Box>
                                {file && (
                                    <Typography variant='subtitle1' sx={{ mt: 2 }}>
                                        Selected file: {file.name}
                                    </Typography>
                                )}
                                <Typography variant='caption' sx={{ mt: 2 }}>
                                    These photos must be in JPEG, GIF, or PNG format, be less than 5MB in size.
                                </Typography>
                            </CardContent>
                        </Box>
                        <Box>
                            <LoadingButton
                                variant='contained'
                                onClick={handleUpload}
                                loading={isUploading}
                                disabled={!file || isDragActive || fileError}
                            >
                                {isUploading ? 'Loading' : 'Upload Photo'}
                            </LoadingButton>
                        </Box>
                    </Card>
                </Modal>
        </>
    );
};

export default PhotoUploadModal;
