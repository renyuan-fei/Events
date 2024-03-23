import Footer from "@ui/Footer.tsx";
import {NavBar} from "@ui/NavBar.tsx";
import LoginModal from "@features/user/LoginForm.tsx";
import {Outlet} from "react-router";
import SignUpForm from "@features/user/SignUpForm.tsx";
import {useSelector} from "react-redux";
import {RootState} from "@store/store.ts";
import Box from "@mui/material/Box";
import LoadingComponent from "@ui/LoadingComponent.tsx";
import AlertComponent from "@ui/AlertComponent.tsx";
import PageContainer from "@ui/PageContainer.tsx";

const AppLayout = () => {
    const {isLoading, alertInfo} = useSelector((state: RootState) => state.common);

    // const location = useLocation();
    // const hideFooterRoutes = ['/notification', '/photos/:Id', '/following/:userId', '/follower/:userId','/activity/:activityId/attendees'];
    // const showFooter = !hideFooterRoutes.some(path => new RegExp(`^${path.replace(/:[^\s/]+/g, '([\\w-]+)')}$`).test(location.pathname));

    if (isLoading) {
        return <LoadingComponent/>;
    }

    return (
        <Box sx={{
            display: 'flex',
            flexDirection: 'column', // 将主轴设置为垂直方向
            minHeight: '100vh', // 最小高度设置为视口高度
        }}>
            <NavBar />
            {alertInfo.open && (
                <AlertComponent severity={alertInfo.severity} message={alertInfo.message} />
            )}

            <Box sx={{
                flexGrow: 1, // 允许内容区域伸展以占用所有可用空间
                backgroundColor: 'rgb(246,247,248)',
            }}>
                <PageContainer>
                    <LoginModal />
                    <SignUpForm />
                    <Outlet />
                </PageContainer>
            </Box>
            <Footer />
            {/*{showFooter && <Footer />}*/}
        </Box>
    );
}

export default AppLayout;