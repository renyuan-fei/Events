import {createHashRouter} from "react-router-dom";
import {AppLayout} from "@pages/AppLayout.tsx";
import MainPage from "@pages/MainPage.tsx";

export const router = createHashRouter([
    {
        path: "/",
        element: <AppLayout/>,
        children: [
            {path: "/", element: <MainPage/>}
        ]
    }
])