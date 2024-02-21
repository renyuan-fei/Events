import React from 'react';
import { Button } from '@mui/material';

interface GalleryButtonProps {
    text: string;
    onClick: () => void;
    isAddButton?: boolean;
}

const GalleryButton: React.FC<GalleryButtonProps> = ({ text, onClick, isAddButton = false }) => (
    <Button
        variant="text"
        onClick={onClick}
        sx={{
            color: '#00798A',
            fontFamily: '"Graphik Meetup", -apple-system, sans-serif',
            fontSize: '16px',
            textTransform: 'none',
            textDecoration: 'none',
        }}
    >
        {isAddButton ? `+ ${text}` : text}
    </Button>
);

export default GalleryButton;