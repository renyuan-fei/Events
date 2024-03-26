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
    setIsMobile, setLoginForm
} from "@features/commonSlice.ts";
import useGetCurrentUserQuery from "@features/user/hooks/useGetCurrentUserQuery.ts";
import LoadingComponent from "@ui/LoadingComponent.tsx";


function App() {

    const dispatch = useDispatch();

    const {refetch,isRefetching} = useGetCurrentUserQuery();

    useEffect(() => {
        const checkMobile = () => dispatch(setIsMobile(window.innerWidth < 600));

        window.addEventListener('resize', checkMobile);

        checkMobile();

        return () => {
            window.removeEventListener('resize', checkMobile);
        };
    }, [dispatch]);


    useEffect(() => {
        if (localStorage.getItem('jwt' )) {
            dispatch(setLoginForm(false))
            refetch();
        }
    }, [dispatch,refetch]);

    if (isRefetching) {
        return <LoadingComponent />;
    }

    return (
        <RouterProvider router={router}/>
    );
}

export default App
