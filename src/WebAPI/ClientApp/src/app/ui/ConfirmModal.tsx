import React from 'react';
import {Modal, Box, Button, Typography, Paper, CircularProgress} from '@mui/material';

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

interface ConfirmModalProps {
    open: boolean;
    onClose: () => void;
    onConfirm: () => void;
    isLoading: boolean;
}

const ConfirmModal: React.FC<ConfirmModalProps> = ({
                                                       open,
                                                       onClose,
                                                       onConfirm,
                                                       isLoading
                                                   }) => {
    return (
        <>
            <Modal
            open={open}
            onClose={onClose}>

            <Paper sx={style}>
                <Typography variant='h6'>Confirm Deletion</Typography>
                <Typography variant='body1'>Are you sure you want to delete this
                    photo?</Typography>
                <Box>
                    <Button disabled={isLoading}
                            variant='outlined'
                            onClick={onClose}>Cancel</Button>
                    <Button disabled={isLoading}
                            variant='contained'
                            color='error'
                            onClick={onConfirm}>{isLoading &&
                        <CircularProgress size={16}/>}Confirm</Button>
                </Box>
            </Paper>
        </Modal>
        </>
    );
};

export default ConfirmModal;
