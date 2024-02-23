import React from "react";
import useGetPaginatedFollowingQuery
    from "@features/follow/hooks/useGetPaginatedFollowingQuery.ts";
import LoadingComponent from "@ui/LoadingComponent.tsx";

interface FollowingListProps {
    userId: string;
    page: number;
    pageSize: number;
}

export const FollowingList:React.FC<FollowingListProps> = ({userId,page,pageSize}) => {
    const {following,isFollowingLoading} = useGetPaginatedFollowingQuery(userId,pageSize,page);

    if (isFollowingLoading) {
        return <LoadingComponent/>
    }

    return (
        <></>
    );
};