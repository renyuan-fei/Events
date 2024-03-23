import {useTheme} from "@mui/material";
import CardContent from "@mui/material/CardContent";
import React from "react";

interface UserCardContextProps {
    children: React.ReactNode;
}
const UserCardContext:React.FC<UserCardContextProps> = ({children}) => {
    const theme = useTheme()
    return (
        <CardContent sx={{
            width: 336,
            marginTop: theme.spacing(2),
        }}>
            {children}
        </CardContent>
    );
};

export default UserCardContext;