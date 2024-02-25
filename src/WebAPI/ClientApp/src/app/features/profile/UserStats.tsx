import Divider from "@mui/material/Divider";
import {Stack} from "@mui/material";
import React from "react";

interface UserStatsProps {
    children: React.ReactNode;
}
const UserStats:React.FC<UserStatsProps> = ({children}) => {
    return (
        <Stack
            direction='row'
            divider={<Divider orientation='vertical' flexItem/>}
            spacing={2}
        >
            {children}
        </Stack>
    );
};

export default UserStats;