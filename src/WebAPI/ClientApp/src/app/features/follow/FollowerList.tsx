import React from "react";
import useGetPaginatedFollowersQuery
    from "@features/follow/hooks/useGetPaginatedFollowersQuery.ts";
import LoadingComponent from "@ui/LoadingComponent.tsx";
import {ListContainer} from "@features/follow/ListContainer.tsx";
import {Follower} from "@features/follow/Follower.tsx";
import {Divider, Typography } from "@mui/material";

interface FollowerListProps {
    userId: string;
    page: number;
    pageSize: number;
}

export const FollowerList: React.FC<FollowerListProps> = ({userId, page, pageSize}) => {

    //TODO user can follow user when view other user's Following list
    const {
        followers,
        isFollowersLoading
    } = useGetPaginatedFollowersQuery(userId, pageSize, page);

    if (isFollowersLoading) {
        return <LoadingComponent/>
    }

    if (followers?.items.length === 0) {
        return (
            <Typography sx={{ margin: 2 }} variant="subtitle1">
                No followers yet
            </Typography>
        );
    }


    return (
        <ListContainer>
            {followers?.items.map((follower,index) => (
                <React.Fragment key={follower.userId}>
                    <Follower
                             userId={follower.userId}
                             userName={follower.userName}
                             displayName={follower.displayName}
                             image={follower.image}
                             bio={follower.bio}
                />
                    {index < followers.items.length - 1 && <Divider variant="inset" component="li" />}
                </React.Fragment>
            ))}
        </ListContainer>
    );
};