import {useNavigate} from "react-router";
import UserCardContainer from "@features/profile/UserCardContainer.tsx";
import UserCardMedia from "@features/profile/UserCardMedia.tsx";
import UserStats from "@features/profile/UserStats.tsx";
import {UserStatItem} from "@features/profile/UserStatItem.tsx";
import UserProfileButton from "@features/profile/UserProfileButton.tsx";
import UserCardContext from "@features/profile/UserCardContext.tsx";
import React from "react";

interface UserCardProps {
    id: string;
    image: string;
    isCurrentUser: boolean;
    isFollowed?: boolean;
    followers: number;
    following: number;
}

const UserCard: React.FC<UserCardProps> = ({
                                               id,
                                               image,
                                               isCurrentUser,
                                               isFollowed,
                                               following,
                                               followers
                                           }) => {
    const navigate = useNavigate();

    const handleNavigateToFollower = () => {
        navigate(`/follower/${id}`);
    };

    const handleNavigateToFollowing = () => {
        navigate(`/following/${id}`);
    };

    return (
        <>
            <UserCardContainer>
                <UserCardMedia image={image} isCurrentUser={isCurrentUser}/>

                <UserCardContext>
                    <UserStats>
                        <UserStatItem onClick={handleNavigateToFollower}
                                      label={'Follower'}
                                      value={followers}/>
                        <UserStatItem onClick={handleNavigateToFollowing}
                                      label={'Following'}
                                      value={following}/>
                        <UserStatItem label={'Events'} value={0}/>
                    </UserStats>

                    <UserProfileButton
                        isCurrentUser={isCurrentUser}
                        isFollowed={isFollowed!}
                        id={id}
                    />

                </UserCardContext>
            </UserCardContainer>
        </>
    );
}

export default UserCard;