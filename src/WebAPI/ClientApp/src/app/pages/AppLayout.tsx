import {PageContainer} from "@ui/PageContainer.tsx";
import Footer from "@ui/Footer.tsx";
import {NavBar} from "@ui/NavBar.tsx";
import LoginModal from "@features/user/LoginForm.tsx";
import {Outlet} from "react-router";
import SignUpForm from "@features/user/SignUpForm.tsx";

export function AppLayout() {


    return (
        <>
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