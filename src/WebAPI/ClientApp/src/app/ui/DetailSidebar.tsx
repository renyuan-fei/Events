import {Paper, useTheme} from "@mui/material";

export function DetailSidebar() {
    const theme = useTheme();
    return (
        <Paper sx={{
            padding: theme.spacing(2),
            marginTop: theme.spacing(2),
        }}>

        </Paper>
    );
}