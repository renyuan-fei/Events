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
import {useMediaQuery, useTheme} from "@mui/material";
import useGetCurrentUserQuery from "@features/user/hooks/useGetCurrentUserQuery.ts";
import LoadingComponent from "@ui/LoadingComponent.tsx";


function App() {
    const theme = useTheme();
    const matchesXS = useMediaQuery(theme.breakpoints.down('xs'));
    const matchesSM = useMediaQuery(theme.breakpoints.between('sm', 'md'));
    const matchesMD = useMediaQuery(theme.breakpoints.between('md', 'lg'));
    const matchesLG = useMediaQuery(theme.breakpoints.between('lg', 'xl'));
    const matchesXL = useMediaQuery(theme.breakpoints.up('xl'));

    useEffect(() => {
        if (matchesXS) {
            console.log('Current breakpoint: xs');
        }
        if (matchesSM) {
            console.log('Current breakpoint: sm');
        }
        if (matchesMD) {
            console.log('Current breakpoint: md');
        }
        if (matchesLG) {
            console.log('Current breakpoint: lg');
        }
        if (matchesXL) {
            console.log('Current breakpoint: xl');
        }
    }, [matchesXS, matchesSM, matchesMD, matchesLG, matchesXL]);

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
