import {PageContainer} from "@ui/PageContainer.tsx";
import MainPage from "@pages/MainPage.tsx";
import Footer from "@ui/Footer.tsx";
import {NavBar} from "@ui/NavBar.tsx";
import SignUpModal from "@features/user/SignUpModal.tsx";
import LoginModal from "@features/user/LoginModal.tsx";

export function AppLayout() {


    return (
        <>
            <NavBar/>
            <PageContainer>
                <SignUpModal/>
                <LoginModal/>
                <MainPage/>
            </PageContainer>
            <Footer/>
        </>
    );
}