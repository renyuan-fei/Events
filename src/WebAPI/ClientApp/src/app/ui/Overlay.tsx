import { Box, Typography } from '@mui/material';
import React from 'react';

interface OverlayProps {
    count: number;
}

const Overlay: React.FC<OverlayProps> = ({ count }) => {
    return (
        <Box
            sx={{
                position: 'absolute',
                top: 0,
                left: 0,
                width: '100%',
                height: '100%',
                bgcolor: 'rgba(0, 0, 0, 0.7)',
                display: 'flex',
                justifyContent: 'center',
                alignItems: 'center',
                zIndex: 2, // Make sure it's above other content
            }}
        >
            <Typography variant='h6' sx={{ color: 'white' }}>
                +{count} more
            </Typography>
        </Box>
    );
};

export default Overlay;
