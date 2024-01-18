import {createHashRouter} from "react-router-dom";
import {AppLayout} from "@pages/AppLayout.tsx";
import MainPage from "@pages/MainPage.tsx";
import {HomePage} from "@pages/HomePage.tsx";
import {RequireAuth} from "@config/RequireAuth.tsx";
// import {Outlet} from "react-router";
export const router = createHashRouter([
    {
        path: "/",
        element: <AppLayout/>,
        children: [
            {path: "/", element: <MainPage/>},
            {element: (
                    <RequireAuth>
                    </RequireAuth>
                ),
                children: [
                    { path: "/home", element: <HomePage /> },
                ],}
        ]
    }
])