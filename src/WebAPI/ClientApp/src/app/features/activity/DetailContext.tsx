import Typography from "@mui/material/Typography";
import Box from "@mui/material/Box";
import {useTheme} from "@mui/material";
import React from "react";

interface DetailContextProps {
    title: string;
    description: string;
}

export const DetailContext:React.FC<DetailContextProps> = ({title,description}) => {
    const theme = useTheme();

    return (
        <Box component={"div"}>
            <Typography variant={"h2"} component={"h2"} sx={{
                fontWeight: theme.typography.fontWeightBold,
                fontSize: '20px',
                marginBottom: theme.spacing(2.5),
            }}>
                Details
            </Typography>
            <Typography variant={"h1"} component={"div"} sx={{
                fontWeight: theme.typography.fontWeightBold,
                fontSize: '16px',
                marginBottom: theme.spacing(0.5),
            }}>
                {title}
            </Typography>
            <Typography variant={"body1"} component={"strong"} sx={{}}>
                {description}
            </Typography>
        </Box>
    );
};

export default DetailContext;