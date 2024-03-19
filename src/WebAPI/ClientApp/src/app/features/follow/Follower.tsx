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
        <ListItem alignItems='flex-start'>
            <ListItemAvatar
                onClick={handleNavigateToUserDetail}
                sx={{
                    cursor: 'pointer',
                }}>
                <Avatar alt='Remy Sharp' src={image} sx={{
                    width: 56, // 增大 Avatar 大小
                    height: 56,
                    marginRight: 3, // 为了对齐文本，可能需要调整间距
                }}
                />
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