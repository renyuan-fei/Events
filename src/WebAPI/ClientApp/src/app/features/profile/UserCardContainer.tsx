import {useTheme} from "@mui/material";
import Card from "@mui/material/Card";
import React from "react";

interface UserCardContainerProps {
    children: React.ReactNode;
}
const UserCardContainer:React.FC<UserCardContainerProps> = ({children}) => {
    const theme = useTheme();

    return (
        <Card sx={{
            width: 400,
            height: 'auto',
            padding: theme.spacing(4),
            borderRadius: theme.shape.borderRadius,
            position: 'relative'
        }}>
            {children}
        </Card>
    );
};

export default UserCardContainer;