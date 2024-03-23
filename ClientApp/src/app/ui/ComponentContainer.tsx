import React from 'react';
import Container from '@mui/material/Container';

interface ContainerProps {
    children: React.ReactNode;
}

const ComponentContainer = ({children}: ContainerProps) => {
    return (
        <Container maxWidth={"xl"}>
            {children}
        </Container>
    );
}

export default ComponentContainer;