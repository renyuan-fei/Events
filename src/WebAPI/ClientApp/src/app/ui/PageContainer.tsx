import React from 'react';
import Container from '@mui/material/Container';

interface ContainerProps {
    children: React.ReactNode;
}

export function PageContainer({children}: ContainerProps) {
    return (
        <Container maxWidth={"lg"} sx={{
            mt: 20
        }}>
            {children}
        </Container>
    );
}