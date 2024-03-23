import { useEffect } from "react";
import { Navigate, Outlet, useLocation } from "react-router-dom";
import { useSelector, useDispatch } from 'react-redux';
import { RootState } from "@store/store";
import { setLoginForm } from "@features/commonSlice";

export function RequireAuth() {
    const dispatch = useDispatch();
    const isLogin = useSelector((state: RootState) => state.user.isLogin);
    const location = useLocation();

    const jwt = localStorage.getItem("jwt");

    useEffect(() => {
        if (!isLogin && !jwt) {
            dispatch(setLoginForm(true));
            // 如果你需要显示提示信息，确保你的提示信息处理逻辑能够在组件渲染后执行
            // dispatch(setAlertInfo({
            //     open: true,
            //     message: 'Please login first',
            //     severity: 'warning',
            // }));
        }
    }, [dispatch, isLogin, jwt]);

    if (!isLogin && !jwt) {
        // 注意：由于状态更新是异步的，这里可能仍然需要处理重定向逻辑
        return <Navigate to="/" state={{ from: location }} />;
    }

    return <Outlet />;
}
