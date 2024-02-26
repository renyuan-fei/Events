import CardContent from "@mui/material/CardContent";
import Typography from "@mui/material/Typography";
import {List, ListItem, ListItemText, useTheme} from "@mui/material";
import Card from "@mui/material/Card";

interface UserInformation{
    name: string;
    email: string;
    phoneNumber: string;
    bio: string;
}
const UserInformation = (props: UserInformation) => {
    const theme = useTheme();
    const {name, email, phoneNumber, bio} = props;

    return (
        <Card sx={{ padding: theme.spacing(3), margin: theme.spacing(2), boxShadow: 3 }}>
            <CardContent>
                <Typography variant="h5" gutterBottom component="div" sx={{ fontWeight: 'bold', color: theme.palette.primary.main }}>
                    About me
                </Typography>
                <List>
                    <ListItem divider sx={{ padding: theme.spacing(1, 0) }}>
                        <ListItemText primary="Name" secondary={name} primaryTypographyProps={{ fontWeight: 'bold' }} />
                    </ListItem>
                    <ListItem divider sx={{ padding: theme.spacing(1, 0) }}>
                        <ListItemText primary="Email" secondary={email} primaryTypographyProps={{ fontWeight: 'bold' }} />
                    </ListItem>
                    <ListItem divider sx={{ padding: theme.spacing(1, 0) }}>
                        <ListItemText primary="phoneNumber" secondary={phoneNumber} primaryTypographyProps={{ fontWeight: 'bold' }} />
                    </ListItem>
                    <ListItem sx={{ padding: theme.spacing(1, 0) }}>
                        <ListItemText sx={{
                            display: 'inline',
                            overflow: 'hidden',
                            textOverflow: 'ellipsis',
                            wordWrap: 'break-word', // Ensures that long words will break and wrap onto the next line
                        }} primary="What's Up" secondary={bio} primaryTypographyProps={{ fontWeight: 'bold' }} />
                    </ListItem>
                </List>
            </CardContent>
        </Card>
    );
}

export default UserInformation;