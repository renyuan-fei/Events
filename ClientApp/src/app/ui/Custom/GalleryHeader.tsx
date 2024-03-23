import React from 'react';
import {Box} from '@mui/material';

interface PhotoGalleryHeaderProps {
    children: React.ReactNode;
}

const GalleryHeader: React.FC<PhotoGalleryHeaderProps> = ({children}) => (
    <Box sx={{
        display: 'flex',
        justifyContent: 'space-between',
        alignItems: 'center',
        marginBottom: 1,
    }}>
        {children}
    </Box>
);

export default GalleryHeader;

