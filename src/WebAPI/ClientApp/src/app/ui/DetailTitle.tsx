import Box from "@mui/material/Box";
import Typography from "@mui/material/Typography";
import {Avatar, Grid, useTheme} from "@mui/material";
import Divider from "@mui/material/Divider";

function stringToColor(string: string) {
    let hash = 0;
    let i;

    /* eslint-disable no-bitwise */
    for (i = 0; i < string.length; i += 1) {
        hash = string.charCodeAt(i) + ((hash << 5) - hash);
    }

    let color = '#';

    for (i = 0; i < 3; i += 1) {
        const value = (hash >> (i * 8)) & 0xff;
        color += `00${value.toString(16)}`.slice(-2);
    }
    /* eslint-enable no-bitwise */

    return color;
}

function stringAvatar(name: string) {
    return {
        sx: {
            bgcolor: stringToColor(name),
        },
        children: `${name.split(' ')[0][0]}${name.split(' ')[1][0]}`,
    };
}

interface DetailTitleProps {
    title: string;
    hostUser: { username: string; id: string; };
}

export function DetailTitle({title, hostUser}: DetailTitleProps) {
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