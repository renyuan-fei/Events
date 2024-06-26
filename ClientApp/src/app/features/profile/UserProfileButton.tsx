import React, {useState} from 'react';
import Button from '@mui/material/Button';
import {Box, useTheme} from "@mui/material";
import useFollowMutation from "@features/follow/hooks/useFollowMutation.ts";
import useUnfollowMutation from "@features/follow/hooks/useUnfollowMutation.ts";
import UserUpdateModal from "@features/profile/UserUpdateModal.tsx";
import useIsFollowingQuery from "@features/profile/hooks/useIsFollowingQuery.ts";
import {queryClient} from "@apis/queryClient.ts";
import {userInfo} from "@type/UserInfo.ts";
import {Circle} from "@mui/icons-material";

interface UserProfileButtonProps {
    isCurrentUser: boolean;
    id: string;
}

const UserProfileButton: React.FC<UserProfileButtonProps> = ({
                                                                 isCurrentUser,
                                                                 id
                                                             }) => {
    const currentUserId = queryClient.getQueryData<userInfo>("userInfo")?.id;

    const {
        isLoading: isFollowingLoading,
        isFollowing: isFollowed
    } = useIsFollowingQuery(id!, currentUserId!);

    const {isFollowing, follow} = useFollowMutation(id);
    const {isUnfollowing, unfollow} = useUnfollowMutation(id);
    const [isOpen, setIsOpen] = useState<boolean>(false);

    const handleFollowClick = async () => {
        await follow();
    }

    const handleUnfollowClick = async () => {
        await unfollow();
    }

    const handleOpen = () => {
        setIsOpen(true);
    }

    const handleClose = () => {
        setIsOpen(false);
    }

    const theme = useTheme();
    const buttonSx = {
        width: 280,
        height: 44,
        fontSize: 20
    };

    return (
        <>
            <UserUpdateModal open={isOpen}
                             handleClose={handleClose}
            />
            <Box sx={{
                display: 'flex',
                justifyContent: 'center',
                alignItems: 'center',
                marginTop: theme.spacing(1.5)
            }}>
                {isFollowingLoading? <Circle/> : isCurrentUser ? (
                    <Button
                        variant='outlined'
                        color='primary'
                        sx={buttonSx}
                        onClick={handleOpen}
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
        </>
    );
};

export default UserProfileButton;
