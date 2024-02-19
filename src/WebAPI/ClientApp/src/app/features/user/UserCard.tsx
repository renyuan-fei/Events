import CardMedia from "@mui/material/CardMedia";
import CardContent from "@mui/material/CardContent";
import {Stack, useTheme} from "@mui/material";
import Divider from "@mui/material/Divider";
import Box from "@mui/material/Box";
import Typography from "@mui/material/Typography";
import Button from "@mui/material/Button";
import Card from "@mui/material/Card";

interface UserCardProps {
    imageUrl: string;
    isUser: boolean;
    isFollowed: boolean;
}

const UserCard = (props: UserCardProps) => {
    const theme = useTheme();
    const {imageUrl, isUser, isFollowed} = props;

    return (
        <Card sx={{
            width: 400,
            height: 632,
            padding: theme.spacing(4),
            borderRadius: theme.shape.borderRadius,
        }}>
            <CardMedia
                image={imageUrl}
                title={"Profile Picture"}
                sx={{
                    borderRadius: theme.shape.borderRadius,
                    objectFit: 'cover',
                    width: 336,
                    height: 403,
                }}/>
            <CardContent sx={{
                width: 336,
                marginTop: theme.spacing(2),
            }}>
                <Stack
                    direction="row"
                    divider={<Divider orientation="vertical" flexItem/>}
                    spacing={2}
                >
                    <Box sx={{textAlign: 'center', width: '33%'}}>
                        <Typography variant="h6"
                                    component={"div"}
                                    sx={{
                                        fontWeight: theme.typography.fontWeightBold,
                                        fontSize: 30
                                        , height: 36
                                    }}>43</Typography>
                        <Typography variant="subtitle1"
                                    component={"div"}>Follower</Typography>
                    </Box>
                    <Box sx={{textAlign: 'center', width: '33%'}}>
                        <Typography variant="h6"
                                    component={"div"}
                                    sx={{
                                        fontWeight: theme.typography.fontWeightBold,
                                        fontSize: 30
                                        , height: 36
                                    }}>0</Typography>
                        <Typography variant="subtitle1"
                                    component={"div"}>Following</Typography>
                    </Box>
                    <Box sx={{textAlign: 'center', width: '33%'}}>
                        <Typography variant="h6"
                                    component={"div"}
                                    sx={{
                                        fontWeight: theme.typography.fontWeightBold,
                                        fontSize: 30
                                        , height: 36
                                    }}>11</Typography>
                        <Typography variant="subtitle1"
                                    component={"div"}>Events</Typography>
                    </Box>
                </Stack>
                <Box sx={{
                    display: 'flex',
                    justifyContent: 'center',
                    alignItems: 'center',
                    marginTop: theme.spacing(1.5)
                }}>
                    {isUser ? (
                        <Button
                            variant="outlined"
                            color="primary"
                            sx={{width: 280, height: 44, fontSize: 20}}
                        >
                            Edit Profile
                        </Button>
                    ) : isFollowed ? (
                        <Button
                            variant="outlined"
                            color="primary"
                            sx={{width: 280, height: 44, fontSize: 20}}
                        >
                            Unfollow
                        </Button>
                    ) : (
                        <Button
                            variant="contained"
                            color="secondary"
                            sx={{width: 280, height: 44, fontSize: 20}}
                        >
                            Follow
                        </Button>
                    )}

                </Box>
            </CardContent>
        </Card>
    );
}

export default UserCard;