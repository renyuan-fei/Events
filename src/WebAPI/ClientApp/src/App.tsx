import './App.css'
import {RouterProvider} from "react-router-dom";
import '@fontsource/roboto/300.css';
import '@fontsource/roboto/400.css';
import '@fontsource/roboto/500.css';
import '@fontsource/roboto/700.css';
import {router} from "@config/Router.tsx";
import {useDispatch} from "react-redux";
import {useEffect} from "react";
import {
    setIsMobile
} from "@features/commonSlice.ts";
import {useGetCurrentUserQuery} from "@apis/Account.ts";
import {LoadingComponent} from "@ui/LoadingComponent.tsx";

function App() {
    const dispatch = useDispatch();
    const currentUserQuery = useGetCurrentUserQuery();

    useEffect(() => {
        const checkMobile = () => dispatch(setIsMobile(window.innerWidth < 768));

        // 监听窗口尺寸变化
        window.addEventListener('resize', checkMobile);

        // 初次检查
        checkMobile();

        return () => {
            // 清理监听器
            window.removeEventListener('resize', checkMobile);
        };
    }, [dispatch]);


    useEffect(() => {
        // 检查本地存储中是否有JWT令牌
        if (localStorage.getItem('jwt')) {
            currentUserQuery.refetch(); // 手动触发用户数据获取请求
        }
    }, []);

    // 使用 currentUserQuery 的状态来控制加载组件的显示
    if (currentUserQuery.isLoading) {
        return <LoadingComponent />;
    }

    return (
        <RouterProvider router={router}/>
    );
}

export default App
