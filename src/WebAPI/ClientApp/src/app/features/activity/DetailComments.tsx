import {
    Typography,
    useTheme, Paper
} from '@mui/material';
import CommentInput from "@ui/CommentInput.tsx";
import React from "react";
import CommentList from "@features/comment/CommentList.tsx";

interface DetailCommentsProps {
    activityId: string | undefined;
}

const DetailComments: React.FC<DetailCommentsProps> = ({activityId}) => {
    // TODO quick check user profile
    // TODO delete comment (only for comment owner)

    const theme = useTheme();

    return (
        <Paper sx={{width: '100%', padding: theme.spacing(2)}}>
            <Typography component='div' variant='h2' sx={{
                fontSize: '20px',
                fontWeight: theme.typography.fontWeightBold,
                fontFamily: '"Graphik Meetup", -apple-system, sans-serif',
            }}>
                Comments
            </Typography>

            <CommentInput activityId={activityId!}/>

            <CommentList activityId={activityId!}/>

        </Paper>
    );
}

export default DetailComments;
