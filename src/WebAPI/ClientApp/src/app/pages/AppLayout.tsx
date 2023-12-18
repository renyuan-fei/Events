import {PageContainer} from "@ui/PageContainer.tsx";
import MainPage from "@pages/MainPage.tsx";
import Footer from "@ui/Footer.tsx";
import {NavBar} from "@ui/NavBar.tsx";

export function AppLayout() {
    return (
        <>
            <NavBar/>
            <PageContainer>
                <MainPage/>
            </PageContainer>
            <Footer/>
        </>
    );
}