import {Attendee} from "@type/Attendee.ts";
import React from "react";
import {
    Avatar,
    Badge,
    ListItem,
    ListItemAvatar,
    ListItemText,
    Typography
} from "@mui/material";
import {useNavigate} from "react-router";

interface AttendeeItemProps {
    attendee: Attendee
}

const AttendeeItem: React.FC<AttendeeItemProps> = ({attendee}) => {
    const navigate = useNavigate();

    const {
        bio,
        image,
        userId,
        displayName,
        isHost
    } = attendee;

    const navigateToUserProfile = () => {
        navigate(`/user/${userId}`);
    };

    return (
        <ListItem alignItems={'flex-start'}>
            <ListItemAvatar
                onClick={navigateToUserProfile}
                sx={{
                    cursor: 'pointer',
                }}>
                <Badge
                    overlap="circular"
                    anchorOrigin={{ vertical: 'top', horizontal: 'left' }}
                    variant="dot"
                    color="secondary"
                    invisible={!isHost}
                    sx={{
                        '& .MuiBadge-dot': {
                            width: 15,  // Increase the dot size
                            height: 15, // Increase the dot size
                            borderRadius: '50%',
                        }
                    }}
                >
                    <Avatar alt='Remy Sharp' src={image} sx={{
                        width: 56,
                        height: 56,
                        marginRight: 3,
                    }}/>
                </Badge>
            </ListItemAvatar>
            <ListItemText
                primary={<Typography variant="h6" component="span" sx={{
                    fontWeight: 'bold',
                    display: 'block', // 或者 'inline-block' 都可以，取决于布局需求
                    whiteSpace: 'nowrap',
                    overflow: 'hidden',
                    textOverflow: 'ellipsis',
                    width: '100%', // 可以调整为实际需要的宽度
                    marginTop:0.5
                }}>{displayName}</Typography>}
                secondary={
                    <>
                        <Typography
                            component='span'
                            variant='body1'
                            color='text.primary'
                            sx={{
                                display: 'inline',
                                overflow: 'hidden',
                                textOverflow: 'ellipsis',
                                wordWrap: 'break-word', // Ensures that long words will break and wrap onto the next line
                            }}
                        >
                            {bio}
                        </Typography>
                    </>
                }
            />
        </ListItem>
    );
};

export default AttendeeItem;