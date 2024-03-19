import { FollowerList } from "@features/follow/FollowerList.tsx";
import {useParams, useSearchParams} from "react-router-dom";
import {Box, Typography} from "@mui/material";

export const FollowerPage = () => {
    const { userId } = useParams<{ userId: string }>();
    const [searchParams] = useSearchParams();
    const pageSize = Number(searchParams.get("pageSize")) || 1;

    return (
        <>
            <Box sx={{ textAlign: 'center', my: 4 }}>
                <Typography variant="h4" component="h1">
                    Follower
                </Typography>
            </Box>
            <Box sx={{ display: 'flex', justifyContent: 'center' }}>
                <FollowerList userId={userId!} pageSize={pageSize} />
            </Box>
        </>
    );
};
