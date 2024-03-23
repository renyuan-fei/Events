import React from 'react';
import {Typography, Button, Box} from '@mui/material';

interface EmptyStateProps {
    isCurrentUser: boolean;
    onAdd: () => void;
    resourceName: string;
}

const EmptyState: React.FC<EmptyStateProps> = ({isCurrentUser, onAdd, resourceName}) => {
    return (
        <Box
            sx={{
                display: 'flex',
                flexDirection: 'column',
                justifyContent: 'center',
                alignItems: 'center',
                height: '100%',
                textAlign: 'center',
            }}
        >
            <Typography sx={{mb: 2}} variant='h6' component='div'>
                No {resourceName} available
            </Typography>
            {isCurrentUser && (
                <Button variant='outlined' onClick={onAdd} sx={{mb: 2}}>
                    Add {resourceName}
                </Button>
            )}
        </Box>
    );
};

export default EmptyState;