import React from "react";
import Box from "@mui/material/Box";
import ActivityFilter from "@features/activity/ActivityFilter.tsx";

const ActivitiesList = ({children} : { children: React.ReactNode; }) => {
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

export default ActivitiesList;