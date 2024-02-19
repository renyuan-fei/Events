import { useParams } from "react-router";
import Box from "@mui/material/Box";
import { Grid, Stack, useTheme } from "@mui/material";
// import { UserCard } from "@features/user/UserCard.tsx";
import useGetUserProfileQuery from "@features/user/hooks/useGetUserProfileQuery.ts";
import LoadingComponent from "@ui/LoadingComponent.tsx";

const UserProfile = () => {
    const theme = useTheme();
    const { userId } = useParams<{ userId: string }>();

    console.log(userId);

    const {data, isLoading} = useGetUserProfileQuery(userId!);

    if (isLoading) {
        return <LoadingComponent/>;
    }

    if (!data) {
        return <div>Error</div>;
    }

    console.log(data);

    return (
        <Box>
            <Grid container spacing={theme.spacing(8)}>
                <Grid item lg={4}>
                    {/*<UserCard imageUrl={data.image} isUser={isUser} isFollowed={isFollowed}/>*/}
                </Grid>
                <Grid item lg={8}>
                    <Stack spacing={2}>
                        {/*<UserInformation name={data.userName} email={data.email} bio={data.bio} phoneNumber={data.phoneNumber}/>*/}
                        {/*<CustomImageList Id={data.id} isUser={isUser} images={data.photos}/>*/}
                    </Stack>
                </Grid>
            </Grid>
        </Box>
    );
}

export default UserProfile;
