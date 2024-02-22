import Box from "@mui/material/Box";
import {Paper, useTheme} from "@mui/material";
import Typography from "@mui/material/Typography";

interface DetailBodyProps {
    imageUrl: string;
    title: string;
    description: string;
}

const DetailBody = (props: DetailBodyProps) => {
    const theme = useTheme();
    const { imageUrl, title, description } = props;

    return (
        <Paper sx={{ marginTop: theme.spacing(2), marginBottom:theme.spacing(5), padding: theme.spacing(2.8)}}>
                <Box
                    component="img"
                    src={imageUrl}
                    sx={{
                        width: '100%',
                        height: 'auto',
                        objectFit: 'cover', // 图片将覆盖整个容器区域
                        objectPosition: 'center', // 图片从中心位置展示
                        marginBottom: theme.spacing(2),
                    }}
                    onLoad={(e) => {
                        const target = e.target as HTMLImageElement; // Cast to the correct type
                        const height = target.height;
                        target.style.height = `${height * 0.8}px`; // Set the height to 80% of the loaded image height
                    }}
                />


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
                    <Typography variant={"body1"} component={"strong"} sx={{

                    }}>
                        {description}
                    </Typography>
                </Box>
        </Paper>
    );
}

export default DetailBody;