import LogoImg from "@assets/logo.png";
import Typography from "@mui/material/Typography";
import { Box, Grid, useMediaQuery } from "@mui/material";
import { useNavigate } from "react-router";
import React, { useCallback } from "react";
import {useSelector} from "react-redux";
import {RootState} from "@store/store.ts";

const Logo = () => {
    const navigate = useNavigate();
    const isLogin = useSelector((state: RootState) => state.user.isLogin);
    const isMobile = useMediaQuery('(max-width:600px)');

    const onBoxClick = useCallback((event: React.MouseEvent<HTMLDivElement>) => {
        event.preventDefault();
        if (isLogin) {
            navigate('/home');
        } else {
            navigate('/');
        }

    }, [navigate, isLogin]);

    const mobileStyle = {
        display: 'flex',
        alignItems: 'center',
        width: 80,
        cursor: 'pointer',
        '& img': {
            maxWidth: 50,
            maxHeight: 50,
        },
        '& .MuiTypography-root': { // 使用Mui的类选择器
            fontFamily: '"Marker Felt", sans-serif',
            fontSize: '1.25rem', // 相当于25px
        },
    };

    const desktopStyle = {
        display: 'flex',
        alignItems: 'center',
        width: 150,
        cursor: 'pointer',
        '& img': {
            maxWidth: 70,
            maxHeight: 70,
        },
        '& .MuiTypography-root': {
            fontFamily: '"Marker Felt", sans-serif',
            fontSize: '2.25rem',
        },
    };

    return (
        <Box sx={isMobile ? mobileStyle : desktopStyle} onClick={onBoxClick}>
            <Grid container spacing={6} alignItems='center'>
                <Grid item xs={6}>
                    <img src={LogoImg} alt="Logo"/>
                </Grid>
                <Grid item xs={6}>
                    <Typography variant='h6' component='div'>
                        Events
                    </Typography>
                </Grid>
            </Grid>
        </Box>
    );
}

export default Logo;
