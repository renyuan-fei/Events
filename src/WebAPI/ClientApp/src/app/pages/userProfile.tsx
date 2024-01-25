import { useParams } from "react-router";
import Box from "@mui/material/Box";
import { Grid, Stack, useTheme } from "@mui/material";
import { UserCard } from "@features/user/UserCard.tsx";
import CustomImageList from "@ui/Custom/CustomImageList.tsx";
import { UserInformation } from "@features/user/UserInformation.tsx";
import { useGetUserProfileQuery } from "@apis/Account.ts";
import { useEffect } from "react";
import { useIsFollowingUser } from "@apis/Following.ts";

export function UserProfile() {
    const theme = useTheme();
    const { userId } = useParams<{ userId: string }>();

    const getUserProfileQuery = useGetUserProfileQuery(userId);
    const isFollowedQuery = useIsFollowingUser(userId as string);

    useEffect(() => {
        getUserProfileQuery.refetch();
        if (userId) {
            isFollowedQuery.refetch();
        }
    }, []);

    const { isLoading, data } = getUserProfileQuery;
    const isUser = userId === undefined;
    const isFollowed = isFollowedQuery.data?.isFollowing ?? false;

    if (isLoading) {
        return <div>Loading...</div>;
    }

    if (!data) {
        return <div>Error</div>;
    }

    return (
        <Box>
            <Grid container spacing={theme.spacing(8)}>
                <Grid item lg={4}>
                    <UserCard imageUrl={data.image} isUser={isUser} isFollowed={isFollowed}/>
                </Grid>
                <Grid item lg={8}>
                    <Stack spacing={2}>
                        <UserInformation name={data.userName} email={data.email} bio={data.bio} phoneNumber={data.phoneNumber}/>
                        <CustomImageList Id={data.id} isUser={isUser} images={data.photos}/>
                    </Stack>
                </Grid>
            </Grid>
        </Box>
    );
}
