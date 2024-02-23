import React from "react";
import {List} from "@mui/material";

interface ListContainerProps {
    children: React.ReactNode;
}

export const ListContainer:React.FC<ListContainerProps> = ({children}) => {
    return (
        <List sx={{ width: '100%', maxWidth: 360, bgcolor: 'background.paper' }}>
            {children}
        </List>
    );
};