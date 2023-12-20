import {createHashRouter} from "react-router-dom";
import {AppLayout} from "@pages/AppLayout.tsx";

export const router = createHashRouter([
    {
        path: "/",
        element: <AppLayout/>,
    }
])