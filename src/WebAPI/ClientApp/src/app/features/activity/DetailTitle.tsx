import Box from "@mui/material/Box";
import Typography from "@mui/material/Typography";
import {Avatar, Grid, useTheme} from "@mui/material";
import Divider from "@mui/material/Divider";

const stringToColor = (string: string) => {
    let hash = 0;
    let i;

    for (i = 0; i < string.length; i += 1) {
        hash = string.charCodeAt(i) + ((hash << 5) - hash);
    }

    let color = '#';

    for (i = 0; i < 3; i += 1) {
        const value = (hash >> (i * 8)) & 0xff;
        color += `00${value.toString(16)}`.slice(-2);
    }

    return color;
}

const stringAvatar = (name: string) => {
    let initials = '';
    const nameParts = name.split(' ');

    // 获取第一个单词的首字母
    if (nameParts[0]) {
        initials += nameParts[0][0];
    }

    // 如果存在第二个单词，获取其首字母
    if (nameParts.length > 1 && nameParts[1]) {
        initials += nameParts[1][0];
    }

    return {
        sx: {
            bgcolor: stringToColor(name),
        },
        children: initials,
    };
}


interface DetailTitleProps {
    title: string;
    hostUser: { username: string; id: string; };
}

const DetailTitle = ({title, hostUser}: DetailTitleProps) => {
    const theme = useTheme();
    return (
        <Box>
            <Divider/>
            <Box component="div" sx={{
                padding: theme.spacing(2),
            }}>
                <Grid container spacing={2} alignItems="center">
                    <Grid item xs={12}>
                        <Typography variant="h1" component="h1" sx={{
                            fontWeight: 'bold',
                            fontSize: '30px',
                            lineHeight: '1.1',
                            mb: 2,
                            wordWrap: 'break-word'
                        }}>
                            {title}
                        </Typography>
                    </Grid>
                    <Grid item>
                        <Avatar {...stringAvatar(hostUser.username)} />
                    </Grid>
                    <Grid item>
                        <Typography component="div" sx={{}}>
                            Hosted By
                        </Typography>
                        <Typography component="div" sx={{fontWeight: 'bold'}}>
                            {hostUser.username}
                        </Typography>
                    </Grid>
                </Grid>
            </Box>
            <Divider/>
        </Box>
    );
}

export default DetailTitle;