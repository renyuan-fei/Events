import {createHashRouter, useLocation} from "react-router-dom";
import AppLayout from "@pages/AppLayout.tsx";
import MainPage from "@pages/MainPage.tsx";
import {RequireAuth} from "@config/RequireAuth.tsx";
import HomePage from "@pages/HomePage.tsx";
import ActivityDetailPage from "@pages/ActivityDetailPage.tsx";
import UserProfile from "@pages/userProfile.tsx";
import {FollowerPage} from "@pages/FollowerPage.tsx";
import {FollowingPage} from "@pages/FollowingPage.tsx";
import {useEffect} from "react";
import {CreateActivity} from "@pages/CreateActivity.tsx";
import {NotificationPage} from "@pages/NotificationPage.tsx";
import AttendeePage from "@pages/AttendeesPage.tsx";
import AllPhotosPage from "@pages/AllPhotosPage.tsx";


const ScrollToTop = () => {
    const location = useLocation();

    useEffect(() => {
        window.scrollTo(0, 0);
    }, [location]);

    return null;
}

export const router = createHashRouter([
    {
        path: "/",
        element: (
            <>
                <ScrollToTop/>
                <AppLayout/>
            </>),
        children: [
            {path: "/", element: <MainPage/>},
            {
                element: (
                    <RequireAuth>
                    </RequireAuth>
                ),
                children: [
                    {path: "/home", element: <HomePage/>},
                    {path: "/activity/new", element: <CreateActivity/>},
                    {path: "/activity/new/:activityId", element: <CreateActivity/>},
                    {path: "/activity/:activityId", element: <ActivityDetailPage/>},
                    {path: "/activity/:activityId/attendees", element: <AttendeePage/>},
                    {path: "/user/:userId", element: <UserProfile/>},
                    {path: "/follower/:userId", element: <FollowerPage/>},
                    {path: "/following/:userId", element: <FollowingPage/>},
                    {path: "/photos/:id", element: <AllPhotosPage/>},
                    {path: "/notification", element: <NotificationPage/>},
                ]
            },
        ]
    },
])