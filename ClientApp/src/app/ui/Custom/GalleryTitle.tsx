import React from 'react';
import { Typography } from '@mui/material';

interface GalleryTitleProps {
    title: string;
    Count: number;
}

const GalleryTitle: React.FC<GalleryTitleProps> = ({ title, Count }) => (
    <Typography component="div" variant="h2" sx={{
        fontSize: '20px',
        fontWeight: 'bold',
        fontFamily: '"Graphik Meetup", -apple-system, sans-serif',
    }}>
        {title} ({Count})
    </Typography>
);

export default GalleryTitle;
