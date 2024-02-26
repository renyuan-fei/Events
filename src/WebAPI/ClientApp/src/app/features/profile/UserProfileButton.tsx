import React from 'react';
import Button from '@mui/material/Button';
import {Box, useTheme} from "@mui/material";
import useFollowMutation from "@features/follow/hooks/useFollowMutation.ts";
import useUnfollowMutation from "@features/follow/hooks/useUnfollowMutation.ts";

interface UserProfileButtonProps {
    isCurrentUser: boolean;
    isFollowed: boolean;
    id: string;
}

const UserProfileButton: React.FC<UserProfileButtonProps> = ({
                                                                 isCurrentUser,
                                                                 isFollowed,
                                                                 id
                                                             }) => {
    // TODO implement edit profile

    const {isFollowing, follow} = useFollowMutation(id);
    const {isUnfollowing, unfollow} = useUnfollowMutation(id);
    const handleFollowClick = async () => {
        await follow();
    }

    const handleUnfollowClick = async () => {
        await unfollow();
    }

    const theme = useTheme();
    const buttonSx = {
        width: 280,
        height: 44,
        fontSize: 20
    };

    return (
        <Box sx={{
            display: 'flex',
            justifyContent: 'center',
            alignItems: 'center',
            marginTop: theme.spacing(1.5)
        }}>
            {isCurrentUser ? (
                <Button
                    variant='outlined'
                    color='primary'
                    sx={buttonSx}
                >
                    Edit Profile
                </Button>
            ) : isFollowed ? (
                <Button
                    onClick={handleUnfollowClick}
                    disabled={isUnfollowing}
                    variant='outlined'
                    color='primary'
                    sx={buttonSx}
                >
                    Unfollow
                </Button>
            ) : (
                <Button
                    onClick={handleFollowClick}
                    disabled={isFollowing}
                    variant='contained'
                    color='secondary'
                    sx={buttonSx}
                >
                    Follow
                </Button>
            )}
        </Box>
    );
};

export default UserProfileButton;
