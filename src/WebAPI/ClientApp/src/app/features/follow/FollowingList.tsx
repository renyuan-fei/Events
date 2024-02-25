import React from "react";
import useGetPaginatedFollowingQuery
    from "@features/follow/hooks/useGetPaginatedFollowingQuery.ts";
import LoadingComponent from "@ui/LoadingComponent.tsx";
import {Follower} from "@features/follow/Follower.tsx";
import {ListContainer} from "@features/follow/ListContainer.tsx";
import {queryClient} from "@apis/queryClient.ts";
import {userInfo} from "@type/UserInfo.ts";
import {Divider, Typography} from "@mui/material";

interface FollowingListProps {
    userId: string;
    page: number;
    pageSize: number;
}

export const FollowingList: React.FC<FollowingListProps> = ({userId, page, pageSize}) => {
    const {
        following,
        isFollowingLoading
    } = useGetPaginatedFollowingQuery(userId, pageSize, page);

    if (isFollowingLoading) {
        return <LoadingComponent/>
    }

    const currentUserId = queryClient.getQueryData<userInfo>('userInfo')?.id;

    const isCurrentUser = currentUserId === userId;

    if (following?.items.length === 0) {
        return (
            <Typography sx={{margin: 2}} variant='subtitle1'>
                {isCurrentUser ? 'No following yet, Try following someone!' : 'No following yet'}
            </Typography>
        );
    }

    return (
        <>
            <ListContainer>
                {following?.items.map((follower,index,array) => (
                    <React.Fragment key={follower.userId}>
                        <Follower
                            isCurrentUser={isCurrentUser}
                            isFollowing={true}
                            userId={follower.userId}
                            userName={follower.userName}
                            displayName={follower.displayName}
                            image={follower.image}
                            bio={follower.bio}
                        />
                        {index < array.length - 1 && <Divider variant="inset" component="li" />}
                    </React.Fragment>
                ))}
            </ListContainer>
        </>
    );
};