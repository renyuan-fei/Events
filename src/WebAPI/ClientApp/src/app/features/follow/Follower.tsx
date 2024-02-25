import {
    Avatar,
    ListItem, ListItemAvatar, ListItemText,
    Typography
} from "@mui/material";
import React from "react";
import {UserDetailBase} from "@type/UserDetailBase.ts";
import {useNavigate} from "react-router";
import LoadingComponent from "@ui/LoadingComponent.tsx";
import useUnfollowMutation from "@features/follow/hooks/useUnfollowMutation.ts";
import Button from "@mui/material/Button";
import {ListItemSecondaryAction} from "@mui/material";

interface FollowerProps extends UserDetailBase {
    isFollowing?: boolean;
    isCurrentUser?: boolean;
}

export const Follower: React.FC<FollowerProps> = ({
                                                      isCurrentUser = false,
                                                      isFollowing = false,
                                                      userId,
                                                      image,
                                                      displayName,
                                                      bio
                                                  }) => {
    const navigate = useNavigate();
    const {isUnfollowing, unfollow} = useUnfollowMutation(userId);
    const handleNavigateToUserDetail = () => {
        navigate(`/user/${userId}`);
    }

    const handleUnfollowClick = async () => {
        await unfollow();
    }

    if (isUnfollowing) {
        return <LoadingComponent/>
    }

    return (
        <ListItem alignItems='flex-start' sx={{
            cursor: 'pointer',
            '&:hover': {
                backgroundColor: 'rgba(0, 0, 0, 0.04)'
            }
        }}>
            <ListItemAvatar
                onClick={handleNavigateToUserDetail}
                sx={{
                    cursor: 'pointer',
                }}>
                <Avatar alt='Remy Sharp' src={image} sx={{
                    width: 56, // 增大 Avatar 大小
                    height: 56,
                    marginRight: 2, // 为了对齐文本，可能需要调整间距
                }}
                />
            </ListItemAvatar>
            <ListItemText
                primary={<Typography variant="h6" component="span" sx={{ fontWeight: 'bold' }}>{displayName}</Typography>}
                secondary={
                    <>
                        <Typography
                            sx={{display: 'inline'}}
                            component='span'
                            variant='body2'
                            color='text.primary'
                        >
                            {bio}
                        </Typography>
                    </>
                }
            />
            {isCurrentUser && isFollowing && <ListItemSecondaryAction>
              <Button
                  variant='outlined'
                  size='small'
                  disabled={isUnfollowing}
                  onClick={handleUnfollowClick}
              >
                unFollow
              </Button>
            </ListItemSecondaryAction>}

        </ListItem>
    );
};