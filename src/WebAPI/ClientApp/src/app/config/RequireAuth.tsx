// import { Navigate, Outlet, useLocation } from "react-router-dom";
//
// import { useSelector } from 'react-redux'
// import {RootState} from "@store/store.ts";
//
// function RequireAuth() {
//
//     const isLogin = useSelector((state : RootState) => state.user.isLogin)
//     const location = useLocation();
//
//     if (!isLogin) {
//         return <Navigate to='/' state={{from: location}} />
//     }
//     return <Outlet />
// }