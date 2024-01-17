import {PageContainer} from "@ui/PageContainer.tsx";
import Footer from "@ui/Footer.tsx";
import {NavBar} from "@ui/NavBar.tsx";
import LoginModal from "@features/user/LoginForm.tsx";
import {Outlet} from "react-router";
import SignUpForm from "@features/user/SignUpForm.tsx";
import {useSelector} from "react-redux";
import {LoadingComponent} from "@ui/LoadingComponent.tsx";
import {AlterComponent} from "@ui/AlterComponent.tsx";
import {RootState} from "@store/store.ts";

export function AppLayout() {
    const { isLoading, alertInfo } = useSelector((state : RootState) => state.common);


    return (
        <>
            {isLoading && <LoadingComponent />}
            {alertInfo.open && (
                <AlterComponent severity={alertInfo.severity} message={alertInfo.message} />
            )}
            <NavBar/>
            <PageContainer>
                <SignUpForm/>
                <LoginModal/>
                <Outlet />
            </PageContainer>
            <Footer/>
        </>
    );
}