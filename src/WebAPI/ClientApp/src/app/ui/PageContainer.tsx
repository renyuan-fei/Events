import React from 'react';
import Container from '@mui/material/Container';

interface ContainerProps {
    children: React.ReactNode;
}

export function PageContainer({children}: ContainerProps) {
    return (
        <Container maxWidth={"xl"} sx={{
            pt: '32px',
            pb: '48px',
            mt: 9.8
        }}>
            {children}
        </Container>
    );
}