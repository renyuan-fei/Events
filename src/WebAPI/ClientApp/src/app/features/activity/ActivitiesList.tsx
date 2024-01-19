import React from "react";
import Box from "@mui/material/Box";
import Divider from "@mui/material/Divider";
import {ActivityFilter} from "@features/activity/ActivityFilter.tsx";

export function ActivitiesList({children} : { children: React.ReactNode; }) {
    return (
        <Box >
            <ActivityFilter/>
            <Divider sx={{ borderWidth: 1.2, borderColor: 'rgb(162,162,162)' }} />
            {children}
        </Box>
    );
}