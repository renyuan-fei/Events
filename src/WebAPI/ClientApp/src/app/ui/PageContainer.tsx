import React from 'react';
import Container from '@mui/material/Container';

interface ContainerProps {
    children: React.ReactNode;
}

export function PageContainer({children}: ContainerProps) {
    return (
        //maxWidth={false} sx={{ mt: 16, width: '100%', maxWidth: '1920px' }}
        <Container maxWidth={"xl"} sx={{
            mt: 20
        }}>
            {children}
        </Container>
    );
}