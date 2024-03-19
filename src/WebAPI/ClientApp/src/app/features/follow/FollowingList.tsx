import React from "react";
import useGetPaginatedFollowingQuery
    from "@features/follow/hooks/useGetPaginatedFollowingQuery.ts";
import LoadingComponent from "@ui/LoadingComponent.tsx";
import {Follower} from "@features/follow/Follower.tsx";
import {ListContainer} from "@features/follow/ListContainer.tsx";
import {queryClient} from "@apis/queryClient.ts";
import {userInfo} from "@type/UserInfo.ts";
import {CircularProgress, Divider, Typography} from "@mui/material";
import useInfiniteScroll from "@hooks/useInfiniteScroll.ts";

interface FollowingListProps {
    userId: string;
    pageSize: number;
}

export const FollowingList: React.FC<FollowingListProps> = ({userId, pageSize}) => {
    // TODO user can follow user when view other user's Following list
    const {
        following,
        isFetchingNextPage,
        fetchNextPage,
        hasNextPage
    } = useGetPaginatedFollowingQuery(userId, pageSize);

    const loadMoreRef = useInfiniteScroll(
        fetchNextPage,
        hasNextPage!,
        isFetchingNextPage,
    );

    const currentUserId = queryClient.getQueryData<userInfo>('userInfo')?.id;
    const isCurrentUser = currentUserId === userId;

    if (isFetchingNextPage) {
        return <LoadingComponent/>
    }

    if (following?.length === 0) {
        return (
            <Typography sx={{margin: 2}} variant='subtitle1'>
                {isCurrentUser ? 'No following yet, Try following someone!' : 'No following yet'}
            </Typography>
        );
    }


    return (
        <ListContainer>
            {following?.map((follower, index, array) => (
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
                    {index < array.length - 1 &&
                        <Divider variant='inset' component='li'/>}
                </React.Fragment>
            ))}
            <div ref={loadMoreRef}>
                {isFetchingNextPage && <CircularProgress/>}
            </div>
        </ListContainer>
    );
};