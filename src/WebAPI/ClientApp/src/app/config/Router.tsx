import {createHashRouter} from "react-router-dom";
import AppLayout from "@pages/AppLayout.tsx";
import MainPage from "@pages/MainPage.tsx";
import {RequireAuth} from "@config/RequireAuth.tsx";
import HomePage from "@pages/HomePage.tsx";
import ActivityDetailPage from "@pages/ActivityDetailPage.tsx";
import UserProfile from "@pages/userProfile.tsx";

export const router = createHashRouter([
    {
        path: "/",
        element: <AppLayout/>,
        children: [
            {path: "/", element: <MainPage/>},
            {
                element: (
                    <RequireAuth>
                    </RequireAuth>
                ),
                children: [
                    {path: "/home", element: <HomePage/>},
                    {path: "/activity/:activityId", element: <ActivityDetailPage/>},
                    {path: "/user/:userId?", element: <UserProfile/> }
                ]
            },
        ]
    },
])