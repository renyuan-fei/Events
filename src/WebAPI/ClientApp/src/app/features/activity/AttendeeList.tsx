import {ListContainer} from "@features/follow/ListContainer.tsx";
import {CircularProgress, Divider} from "@mui/material";
import AttendeeItem from "@features/activity/AttendeeItem.tsx";
import useGetPaginatedAttendeeQuery
    from "@features/activity/hooks/useGetPaginatedAttendeeQuery.ts";
import useInfiniteScroll from "@hooks/useInfiniteScroll.ts";
import React from "react";
import LoadingComponent from "@ui/LoadingComponent.tsx";

interface AttendeeListProps {
    activityId: string
}

const AttendeeList: React.FC<AttendeeListProps> = ({activityId}) => {
    const {
        attendees,
        fetchNextPage,
        hasNextPage,
        isFetchingNextPage
    } = useGetPaginatedAttendeeQuery(activityId, 15);

    const loadMoreRef = useInfiniteScroll(fetchNextPage, hasNextPage!, isFetchingNextPage);

    if (isFetchingNextPage) {
        return <LoadingComponent/>
    }

    return (
        <ListContainer>
            {attendees?.map((attendee, index) => (
                <React.Fragment key={attendee.userId}>
                    <AttendeeItem attendee={attendee}/>
                    {index < attendees.length - 1 &&
                        <Divider variant='inset' component='li'/>}
                </React.Fragment>
            ))}
            <div ref={loadMoreRef} style={{width: '100%', height: '1px'}}>
                {isFetchingNextPage && <CircularProgress/>}
            </div>
        </ListContainer>
    );
};

export default AttendeeList;