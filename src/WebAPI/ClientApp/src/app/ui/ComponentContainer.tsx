import React from 'react';
import Container from '@mui/material/Container';

interface ContainerProps {
    children: React.ReactNode;
}

export function ComponentContainer({children}: ContainerProps) {
    return (
        <Container maxWidth={"xl"}>
            {children}
        </Container>
    );
}