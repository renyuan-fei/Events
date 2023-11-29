import {createBrowserRouter} from "react-router-dom";
import {AppLayout} from "../layout/AppLayout.tsx";

export const router = createBrowserRouter([
    {
        path: "/",
        element: <AppLayout/>,
    }
])