import React from 'react';
import Container from '@mui/material/Container';
import {useMediaQuery, useTheme} from "@mui/material";

interface ContainerProps {
    children: React.ReactNode;
}

const PageContainer = ({children}: ContainerProps) => {
    const theme = useTheme();
    const isMd = useMediaQuery(theme.breakpoints.down("lg"));

    return (
        <Container maxWidth={isMd ? "lg" : "xl"}
                   sx={{
                       pt: '32px',
                       pb: '48px',
                       mt: 9.8
                   }}>
            {children}
        </Container>
    );
}

export default PageContainer;