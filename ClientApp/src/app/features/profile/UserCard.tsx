import {useNavigate} from "react-router";
import UserCardContainer from "@features/profile/UserCardContainer.tsx";
import UserCardMedia from "@features/profile/UserCardMedia.tsx";
import UserStats from "@features/profile/UserStats.tsx";
import {UserStatItem} from "@features/profile/UserStatItem.tsx";
import UserProfileButton from "@features/profile/UserProfileButton.tsx";
import UserCardContext from "@features/profile/UserCardContext.tsx";
import React from "react";
import {useSelector} from "react-redux";
import {RootState} from "@store/store.ts";

interface UserCardProps {
    id: string;
    image: string;
    isCurrentUser: boolean;
    followers: number;
    following: number;
}

const UserCard: React.FC<UserCardProps> = ({
                                               id,
                                               image,
                                               isCurrentUser,
                                               following,
                                               followers
                                           }) => {
    const navigate = useNavigate();
    const isLogin = useSelector((state:RootState) => state.user.isLogin);

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
                    </UserStats>

                    {isLogin && <UserProfileButton
                        isCurrentUser={isCurrentUser}
                        id={id}
                    />}

                </UserCardContext>
            </UserCardContainer>
        </>
    );
}

export default UserCard;