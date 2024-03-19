import React from "react";
import {Avatar, Box, Typography, Badge} from "@mui/material";
import { useNavigate } from "react-router";
import {Attendee} from "@type/Attendee.ts";

interface DetailAttendeeItemProps {
    attendee: Attendee;
}

const DetailAttendeeItem: React.FC<DetailAttendeeItemProps> = ({ attendee }) => {
    const navigate = useNavigate();
    const { userId, isHost, displayName, image } = attendee;

    const navigateToUserProfile = () => {
        navigate(`/user/${userId}`);
    };

    return (
        <Box sx={{
            textAlign: 'center',
            backgroundColor: 'rgb(246,247,248)',
            height: '182px',
            width: '140px',
            padding: '20px 8px',
            borderRadius: '8px',
            display: 'flex',
            flexDirection: 'column',
            justifyContent: 'center',
            alignItems: 'center',
            borderColor: 'rgb(217 217 217)',
            borderStyle: 'solid',
            borderWidth: '0.2px'
        }}>
            <Badge
                overlap="circular"
                anchorOrigin={{ vertical: 'top', horizontal: 'left' }}
                variant="dot"
                color="secondary"
                invisible={!isHost}
                sx={{
                    '& .MuiBadge-dot': {
                        width: 12,  // Increase the dot size
                        height: 12, // Increase the dot size
                        borderRadius: '50%',
                    }
                }}
            >
                <Avatar
                    alt={displayName}
                    src={image}
                    sx={{
                        width: 56,
                        height: 56,
                        marginBottom: '8px',
                        cursor: 'pointer',
                    }}
                    onClick={navigateToUserProfile}
                />
            </Badge>
            <Typography component="div" variant="caption" display="block" gutterBottom>
                {displayName}
            </Typography>
            <Typography component="div" variant="caption" display="block">
                {isHost ? 'Host' : 'Member'}
            </Typography>
        </Box>
    );
};

export default DetailAttendeeItem;
