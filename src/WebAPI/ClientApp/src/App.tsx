import './App.css'
import {RouterProvider} from "react-router-dom";
import '@fontsource/roboto/300.css';
import '@fontsource/roboto/400.css';
import '@fontsource/roboto/500.css';
import '@fontsource/roboto/700.css';
import {router} from "@config/Router.tsx";
import {QueryClientProvider} from "react-query";
import {queryClient} from "@apis/queryClient.ts";
import {useDispatch} from "react-redux";
import {useEffect} from "react";
import {setIsMobile} from "@features/commonSlice.ts";
import {ThemeProvider} from "@mui/material/styles";
import {theme} from "@config/CustomTheme.ts";

function App() {
    const dispatch = useDispatch();

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

    return (
        <ThemeProvider theme={theme}>
            <QueryClientProvider client={queryClient}>
                <RouterProvider router={router}/>
            </QueryClientProvider>
        </ThemeProvider>

    )
}

export default App
