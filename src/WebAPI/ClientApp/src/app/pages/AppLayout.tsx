import Footer from "@ui/Footer.tsx";
import {NavBar} from "@ui/NavBar.tsx";
import LoginModal from "@features/user/LoginForm.tsx";
import {Outlet} from "react-router";
import SignUpForm from "@features/user/SignUpForm.tsx";
import {useSelector} from "react-redux";
import {RootState} from "@store/store.ts";
import Box from "@mui/material/Box";
import LoadingComponent from "@ui/LoadingComponent.tsx";
import AlterComponent from "@ui/AlterComponent.tsx";
import PageContainer from "@ui/PageContainer.tsx";

const AppLayout = () => {
    const {isLoading, alertInfo} = useSelector((state: RootState) => state.common);

    if (isLoading) {
        return <LoadingComponent/>;
    }

    return (
        <Box sx={{
            backgroundColor: 'rgb(246,247,248)'
        }}>
            {alertInfo.open && (
                <AlterComponent severity={alertInfo.severity}
                                message={alertInfo.message}/>
            )}
            <NavBar/>
            <PageContainer>
                <SignUpForm/>
                <LoginModal/>
                <Outlet/>
            </PageContainer>
            <Footer/>
        </Box>
    );
}

export default AppLayout;