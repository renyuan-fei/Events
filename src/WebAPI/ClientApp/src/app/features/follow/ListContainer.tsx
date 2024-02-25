import React from "react";
import { List, useTheme} from "@mui/material";

interface ListContainerProps {
    children: React.ReactNode;
}

export const ListContainer: React.FC<ListContainerProps> = ({children}) => {
    const theme = useTheme();
    return (
        <List sx={{
            width: '100%',
            maxWidth: 460,
            bgcolor: 'background.paper',
            borderRadius: theme.shape.borderRadius,
            paddingX: theme.spacing(1),
        }}>
            {children}
        </List>
    );
};