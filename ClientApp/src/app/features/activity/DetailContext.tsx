import Typography from "@mui/material/Typography";
import Box from "@mui/material/Box";
import {useTheme} from "@mui/material";
import React from "react";
import parse from "react-html-parser"

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
            <Typography variant={"body1"} component={"div"} sx={{
                whiteSpace: 'normal', // 允许换行
                wordBreak: 'break-word', // 在必要时断词
                marginBottom: theme.spacing(0.5), // 根据需要添加适当的底部外边距
            }}>
                {parse(description)}
            </Typography>
        </Box>
    );
};

export default DetailContext;