import React from "react";
import {Box, useTheme} from "@mui/material";

interface GalleryContainerProps{
    children: React.ReactNode;
}
const GalleryContainer:React.FC<GalleryContainerProps> = ({children}) => {
    const theme = useTheme();
    return (
        <Box sx={{marginTop: theme.spacing(2), marginBottom: theme.spacing(3)}}>
            {children}
        </Box>
    );
};

export default GalleryContainer;