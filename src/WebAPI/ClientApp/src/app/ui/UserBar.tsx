import Box from "@mui/material/Box";
import {Avatar, Grid, Menu, MenuItem, Tooltip} from "@mui/material";
import IconButton from "@mui/material/IconButton";
import Typography from "@mui/material/Typography";
import React from "react";
import {User} from "@type/Account.ts";
import {useLogoutMutation} from "@apis/Account.ts";
import SvgButton from "@ui/SvgButton.tsx";
import {setAlertInfo, setLoginForm} from "@features/commonSlice.ts";
import {useAppDispatch} from "@store/store.ts";
import {useNavigate} from "react-router";

const svg = [
    "M5 7.125C5 5.39911 6.39911 4 8.125 4C9.85089 4 11.25 5.39911 11.25 7.125C11.258.85089 9.85089 10.25 8.125 10.25C6.39911 10.25 5 8.85089 5 7.125ZM8.125 2.5C5.57068 2.5 3.5 4.57068 3.5 7.125C3.5 9.67932 5.57068 11.75 8.125 11.75C10.6793 11.75 12.75 9.67932 12.75 7.125C12.75 4.57068 10.6793 2.5 8.125 2.5ZM3.75736 16.0074C4.88258 14.8821 6.4087 14.25 8 14.25C9.5913 14.25 11.1174 14.8821 12.2426 16.0074C13.3679 17.1326 14 18.6587 14 20.25C14 20.6642 14.3358 21 14.75 21C15.1642 21 15.5 20.6642 15.5 20.25C15.5 18.2609 14.7098 16.3532 13.3033 14.9467C11.8968 13.5402 9.98912 12.75 8 12.75C6.01088 12.75 4.10322 13.5402 2.6967 14.9467C1.29018 16.3532 0.5 18.2609 0.5 20.25C0.5 20.6642 0.835786 21 1.25 21C1.66421 21 2 20.6642 2 20.25C2 18.6587 2.63214 17.1326 3.75736 16.0074ZM18.375 7C17.0633 7 16 8.06332 16 9.375C16 10.6867 17.0633 11.75 18.375 11.75C19.6867 11.75 20.75 10.6867 20.75 9.375C20.75 8.06332 19.6867 7 18.375 7ZM14.5 9.375C14.5 7.2349 16.2349 5.5 18.375 5.5C20.5151 5.5 22.25 7.2349 22.25 9.375C22.25 11.5151 20.5151 13.25 18.375 13.25C16.2349 13.25 14.5 11.5151 14.5 9.375ZM16.5724 15.7717C17.2941 15.5057 18.0694 15.418 18.8324 15.516C19.5953 15.6139 20.3233 15.8947 20.9544 16.3345C21.5855 16.7742 22.1011 17.3599 22.4572 18.0417C22.8134 18.7235 22.9996 19.4812 23 20.2504C23.0002 20.6646 23.3362 21.0002 23.7504 21C24.1646 20.9998 24.5002 20.6638 24.5 20.2496C24.4994 19.2388 24.2547 18.2431 23.7867 17.3472C23.3187 16.4513 22.6412 15.6817 21.8119 15.1038C20.9826 14.5259 20.026 14.1569 19.0234 14.0282C18.0209 13.8994 17.002 14.0147 16.0536 14.3643C15.665 14.5075 15.466 14.9387 15.6093 15.3274C15.7525 15.716 16.1837 15.915 16.5724 15.7717Z"
    , "M5.5 16.5L5.5 19.8791L9.72383 16.5H20C20.2761 16.5 20.5 16.2761 20.5 16V6C20.5 5.72386 20.2761 5.5 20 5.5H4C3.72386 5.5 3.5 5.72386 3.5 6V16C3.5 16.2761 3.72386 16.5 4 16.5H5.5ZM4.68765 20.5289C4.68744 20.5291 4.68786 20.5288 4.68765 20.5289V20.5289ZM4 18C2.89543 18 2 17.1046 2 16V6C2 4.89543 2.89543 4 4 4H20C21.1046 4 22 4.89543 22 6V16C22 17.1046 21.1046 18 20 18H10.25L5.6247 21.7002C4.96993 22.2241 4 21.7579 4 20.9194L4 18Z"
    , "M12 3.5C11.7239 3.5 11.5 3.72386 11.5 4C11.5 4.72952 11.0442 5.50811 10.1914 5.75452C7.48024 6.53792 5.5 9.03965 5.5 12V14C5.5 14.7325 5.18017 15.3107 4.84079 15.7064C4.50796 16.0945 4.10557 16.3718 3.7613 16.5602C3.60235 16.6472 3.5 16.8127 3.5 17C3.5 17.2761 3.72386 17.5 4 17.5H20C20.2761 17.5 20.5 17.2761 20.5 17C20.5 16.8127 20.3976 16.6472 20.2387 16.5602C19.8944 16.3718 19.492 16.0945 19.1592 15.7064C18.8198 15.3107 18.5 14.7325 18.5 14V12C18.5 9.03965 16.5198 6.53792 13.8086 5.75452C12.9558 5.50811 12.5 4.72953 12.5 4C12.5 3.72386 12.2761 3.5 12 3.5ZM10 4C10 2.89543 10.8954 2 12 2C13.1046 2 14 2.89543 14 4C14 4.1422 14.0883 4.274 14.225 4.31347C17.5607 5.27734 20 8.3538 20 12V14C20 14.5523 20.4745 14.9793 20.959 15.2445C21.5793 15.5841 22 16.2429 22 17C22 18.1046 21.1046 19 20 19H4C2.89543 19 2 18.1046 2 17C2 16.2429 2.42067 15.5841 3.04103 15.2445C3.52548 14.9793 4 14.5523 4 14V12C4 8.3538 6.43931 5.27734 9.77504 4.31347C9.91165 4.274 10 4.1422 10 4Z"
]

// const settings = ['Profile', 'Account', 'Dashboard', 'Logout'];

export function UserBar({displayName, image}: User) {
    const navigate = useNavigate();
    const dispatch = useAppDispatch()
    const {mutate: logoutMutation} = useLogoutMutation(() => {
        dispatch(setAlertInfo({
            open: true,
            message: 'You have been logged out',
            severity: 'success',
        }));

    }, (error: any) => {
        dispatch(setAlertInfo({
            open: true,
            message: error.message,
            severity: 'error',
        }));
        navigate('/');
        dispatch(setLoginForm(false))
    });
    const [anchorElUser, setAnchorElUser] = React.useState<null | HTMLElement>(null);

    function handleOpenUserMenu(event: React.MouseEvent<HTMLElement>) {
        setAnchorElUser(event.currentTarget);
    }

    function handleJump(path: string) {
        navigate(path);
    }

    function handleCloseUserMenu() {
        setAnchorElUser(null);
    }

    function handleLogout() {
        logoutMutation()
    }

    return (
        <Box component={"div"} sx={{
            width: 390,
        }}>
            <Grid container spacing={2}>
                <Grid item xs={3}>
                    <SvgButton svg={svg[0]} title={"Connection"}></SvgButton>
                </Grid>
                <Grid item xs={3}>
                    <SvgButton svg={svg[1]} title={"Messages"}></SvgButton>
                </Grid>
                <Grid item xs={3}>
                    <SvgButton svg={svg[2]} title={"Notifications"}></SvgButton>
                </Grid>
                <Grid item xs={3}>

                    <Box sx={{
                        display: 'flex',
                        alignItems: 'center', // Center vertically
                        justifyContent: 'center', // Center horizontally
                    }}>

                        <Tooltip title="Open settings">
                            <IconButton onClick={handleOpenUserMenu}>
                                <Avatar alt={displayName} src={image}/>
                            </IconButton>
                        </Tooltip>

                        <Menu
                            sx={{mt: '70px'}}
                            id="menu-appbar"
                            anchorEl={anchorElUser}
                            anchorOrigin={{
                                vertical: 'top',
                                horizontal: 'right',
                            }}
                            keepMounted
                            transformOrigin={{
                                vertical: 'top',
                                horizontal: 'right',
                            }}
                            open={Boolean(anchorElUser)}
                            onClose={handleCloseUserMenu}
                        >

                            <MenuItem key={"Profile"} onClick=
                                {
                                    () => handleJump(`/user/`)
                                }>
                                <Typography textAlign="center">Profile</Typography>
                            </MenuItem>
                            <MenuItem key={"setting"} onClick={handleCloseUserMenu}>
                                <Typography textAlign="center">setting</Typography>
                            </MenuItem>
                            <MenuItem key={"Logout"} onClick={handleLogout}>
                                <Typography textAlign="center">Logout</Typography>
                            </MenuItem>
                        </Menu>
                    </Box>

                </Grid>
            </Grid>
        </Box>
    );
}