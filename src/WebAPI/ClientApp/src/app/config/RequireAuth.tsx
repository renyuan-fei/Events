import { Navigate, Outlet, useLocation } from "react-router-dom";

import { useSelector } from 'react-redux'
import {RootState, useAppDispatch} from "@store/store.ts";
import {setLoginForm} from "@features/commonSlice.ts";

export function RequireAuth() {

    const dispatch = useAppDispatch()
    const isLogin = useSelector((state : RootState) => state.user.isLogin)
    const location = useLocation();

    if (!isLogin) {
        dispatch(setLoginForm(true))
        return <Navigate to='/' state={{from: location}} />
    }
    return <Outlet />
}