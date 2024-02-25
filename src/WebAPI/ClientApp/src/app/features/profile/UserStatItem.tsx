import Typography from "@mui/material/Typography";
import Box from "@mui/material/Box";
import React from "react";
import {useTheme} from "@mui/material";

interface UserStatItemProps {
    label: string;
    value: number;
    onClick?: () => void;
}

export const UserStatItem: React.FC<UserStatItemProps> = ({label, value, onClick}) => {
    const theme = useTheme();

    return (
        <Box sx={{textAlign: 'center', width: '100%'}}
             onClick={onClick}>
            <Typography variant='h6'
                        component={"div"}
                        sx={{
                            fontWeight: theme.typography.fontWeightBold,
                            fontSize: 30
                            , height: 36
                        }}>{value}</Typography>
            <Typography
                sx={{
                    '&:hover': {
                        color: theme.palette.primary.main
                    },
                    cursor: 'pointer',
                }}
                variant='subtitle1'
                component={"div"}>{label}</Typography>
        </Box>
    );
};