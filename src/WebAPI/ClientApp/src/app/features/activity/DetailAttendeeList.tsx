import React from "react";
import { Box, Stack, Avatar } from "@mui/material";
import DetailAttendeeItem from "./DetailAttendeeItem";
import { Attendee } from "@type/Attendee";

interface DetailAttendeeListProps {
    attendees: Array<Attendee>;
}

const DetailAttendeeList: React.FC<DetailAttendeeListProps> = ({ attendees }) => {
    const displayedAttendees = attendees.slice(0, 5); // 获取前 5 个参与者
    const extraCount = attendees.length - displayedAttendees.length;

    return (
        <Stack direction="row" spacing={2} alignItems="center">
            {displayedAttendees.map((attendee) => (
                <DetailAttendeeItem key={attendee.userId} attendee={attendee} />
            ))}
            {extraCount > 0 && (
                <Box sx={{ textAlign: 'center' }}>
                    <Avatar>+{extraCount} more</Avatar>
                </Box>
            )}
        </Stack>
    );
};

export default DetailAttendeeList;
