import React from "react";
import useGetPaginatedFollowersQuery
    from "@features/follow/hooks/useGetPaginatedFollowersQuery.ts";
import LoadingComponent from "@ui/LoadingComponent.tsx";
import {ListContainer} from "@features/follow/ListContainer.tsx";
import {Follower} from "@features/follow/Follower.tsx";
import {CircularProgress, Divider, Typography} from "@mui/material";
import useInfiniteScroll from "@hooks/useInfiniteScroll.ts";

interface FollowerListProps {
    userId: string;
    pageSize: number;
}

export const FollowerList: React.FC<FollowerListProps> = ({userId, pageSize}) => {

    //TODO user can follow user when view other user's Following list
    const {
        followers,
        isFetchingNextPage,
        fetchNextPage,
        hasNextPage
    } = useGetPaginatedFollowersQuery(userId, pageSize);

    const loadMoreRef = useInfiniteScroll(
        fetchNextPage,
        hasNextPage!,
        isFetchingNextPage
    );


    if (isFetchingNextPage) {
        return <LoadingComponent/>
    }

    if (followers?.length === 0) {
        return (
            <Typography sx={{margin: 2}} variant='subtitle1'>
                No followers yet
            </Typography>
        );
    }


    return (
        <ListContainer>
            {followers?.map((follower, index) => (
                <React.Fragment key={follower.userId}>
                    <Follower
                        userId={follower.userId}
                        userName={follower.userName}
                        displayName={follower.displayName}
                        image={follower.image}
                        bio={follower.bio}
                    />
                    {index < followers.length - 1 &&
                        <Divider variant='inset' component='li'/>}
                </React.Fragment>
            ))}
            <div ref={loadMoreRef}>
                {isFetchingNextPage && <CircularProgress />}
            </div>
        </ListContainer>
    );
};