import {Paper, useTheme} from "@mui/material";
import React from "react";

interface DetailHeaderProps {
    children: React.ReactNode;
}
export const DetailContainer:React.FC<DetailHeaderProps> = ({children}) => {
    const theme = useTheme();

    return (
        <Paper sx={{
            marginTop: theme.spacing(2),
            marginBottom: theme.spacing(5),
            padding: theme.spacing(2.8)
        }}>
            {children}
        </Paper>
    );
};

export default DetailContainer;