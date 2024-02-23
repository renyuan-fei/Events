import {FollowerList} from "@features/follow/FollowList.tsx";
import {useParams} from "react-router";
import {useSearchParams} from "react-router-dom";
import {Box, Typography} from "@mui/material";

export const FollowerPage = () => {
    const [searchParams] = useSearchParams();
    const {userId} = useParams<{ userId: string }>();
    const page = Number(searchParams.get("page")) || 1;
    const pageSize = Number(searchParams.get("pageSize")) || 10;

    return (
        <>
            <Box sx={{ textAlign: 'center', my: 4 }}>
                <Typography variant="h4" component="h1">
                    Followers
                </Typography>
            </Box>
            <Box sx={{ display: 'flex', justifyContent: 'center' }}>
                <FollowerList userId={userId!} page={page} pageSize={pageSize}/>
            </Box>
        </>

    );
};