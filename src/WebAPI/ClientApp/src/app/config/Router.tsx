import {createBrowserRouter} from "react-router-dom";
import {AppLayout} from "@pages/AppLayout.tsx";

export const router = createBrowserRouter([
    {
        path: "/",
        element: <AppLayout/>,
    }
])