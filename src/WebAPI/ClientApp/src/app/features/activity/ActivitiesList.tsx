import React from "react";
import Box from "@mui/material/Box";
import Divider from "@mui/material/Divider";
import {ActivityFilter} from "@features/activity/ActivityFilter.tsx";

export function ActivitiesList({children} : { children: React.ReactNode; }) {
    return (
        <Box sx={{
            display: 'flex',
            flexDirection: 'column',
            alignItems: 'center', // Centers children horizontally
            justifyContent: 'center', // Centers children vertically (if they have a definite height)
            width: '100%', // Ensures the container takes full width
        }}>
            <ActivityFilter/>
            {children}
        </Box>
    );
}