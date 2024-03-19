import {useParams} from "react-router";
import {FollowingList} from "@features/follow/FollowingList.tsx";
import {useSearchParams} from "react-router-dom";
import {Box, Typography} from "@mui/material";

export const FollowingPage = () => {
    const {userId} = useParams<{ userId: string }>();
    const [searchParams] = useSearchParams();
    const pageSize = Number(searchParams.get("pageSize")) || 15;

    return (
        <>
            <Box sx={{ textAlign: 'center', my: 4 }}>
                <Typography variant="h4" component="h1">
                    Following
                </Typography>
            </Box>
            <Box sx={{ display: 'flex', justifyContent: 'center' }}>
                <FollowingList userId={userId!} pageSize={pageSize} />
            </Box>
        </>
    );
};