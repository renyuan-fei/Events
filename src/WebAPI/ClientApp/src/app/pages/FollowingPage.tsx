import {useParams} from "react-router";
import {FollowingList} from "@features/follow/FollowingList.tsx";
import {useSearchParams} from "react-router-dom";
import {Box, Typography} from "@mui/material";

export const FollowingPage = () => {
    const [searchParams] = useSearchParams();
    const {userId} = useParams<{ userId: string }>();
    const page = Number(searchParams.get("page")) || 1;
    const pageSize = Number(searchParams.get("pageSize")) || 10;

    return (
        <>
            <Box sx={{ textAlign: 'center', my: 4 }}>
                <Typography variant="h4" component="h1">
                    Following
                </Typography>
            </Box>
            <Box sx={{ display: 'flex', justifyContent: 'center' }}>
                <FollowingList userId={userId!} page={page} pageSize={pageSize} />
            </Box>
        </>
    );
};