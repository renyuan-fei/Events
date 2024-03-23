import React from "react";
import {Paper, useTheme} from "@mui/material";
interface GalleryDisplayProps {
    children: React.ReactNode;
}
const GalleryDisplay: React.FC<GalleryDisplayProps> = ({children}) => {
    const theme = useTheme();

    return (
        <Paper sx={{
            display: 'flex',
            flexDirection: 'column',
            justifyContent: 'center',
            alignItems: 'center',
            height: 'auto',
            padding: theme.spacing(3)
        }}>
            {children}
        </Paper>
    );
};

export default GalleryDisplay;
