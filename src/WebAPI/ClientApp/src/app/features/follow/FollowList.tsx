import React from "react";
import useGetPaginatedFollowersQuery
    from "@features/follow/hooks/useGetPaginatedFollowersQuery.ts";
import LoadingComponent from "@ui/LoadingComponent.tsx";
import {ListContainer} from "@features/follow/ListContainer.tsx";
import {Follower} from "@features/follow/Follower.tsx";

interface FollowerListProps {
    userId: string;
    page: number;
    pageSize: number;
}

export const FollowerList: React.FC<FollowerListProps> = ({userId, page, pageSize}) => {
    const {
        followers,
        isFollowersLoading
    } = useGetPaginatedFollowersQuery(userId, pageSize, page);

    if (isFollowersLoading) {
        return <LoadingComponent/>
    }

    return (
        <ListContainer>
            {followers?.items.map((follower) => (
                <Follower key={follower.userId}
                          userId={follower.userId}
                          userName={follower.userName}
                          displayName={follower.displayName}
                          image={follower.image}
                          bio={follower.bio}
                />
            ))}
        </ListContainer>
    );
};