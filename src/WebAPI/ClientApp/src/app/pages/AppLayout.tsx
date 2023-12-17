import {NavBar} from "@ui/NavBar.tsx";
import {PageContainer} from "@ui/PageContainer.tsx";
import MainPage from "@pages/MainPage.tsx";

export function AppLayout() {
    return (
        <>
            <NavBar/>
            <PageContainer>
                <MainPage/>
            </PageContainer>
        </>
    );
}