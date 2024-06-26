import {Grid, useMediaQuery, useTheme} from "@mui/material";
import Box from "@mui/material/Box";
import Button from "@mui/material/Button";
import Divider from "@mui/material/Divider";
import {useNavigate} from "react-router";
import React from "react";
import {useLocation} from "react-router-dom";
import {format} from "date-fns";
import {setCategory, setIsGoing, setIsHost, setStartDate} from "@features/commonSlice.ts";
import {useDispatch} from "react-redux";
import CategoriesSelect from "@features/activity/CategoriesSelect.tsx";

const ActivityFilter = () => {
    const dispatch = useDispatch();
    const location = useLocation();
    const navigate = useNavigate();
    const searchParams = new URLSearchParams(location.search);
    const theme = useTheme();
    const isMd = useMediaQuery(theme.breakpoints.down("lg"));
    const handleHost = (e : React.MouseEvent<HTMLButtonElement, MouseEvent>) => {
        e.preventDefault();

        dispatch(setIsHost(false));
        dispatch(setIsGoing(true));

        if (searchParams.has('page')) {
            searchParams.set('page', '1');
        }
        if (searchParams.has('pageSize')) {
            searchParams.set('pageSize', '8');
        }

        searchParams.delete('category');
        searchParams.delete('startdate');

        searchParams.delete('isGoing');
        searchParams.set('isHost', 'true');
        searchParams.set('isCancelled', 'true');

        navigate({ search: searchParams.toString() });
    }

    const handleGoing = (e : React.MouseEvent<HTMLButtonElement, MouseEvent>) => {
        e.preventDefault();

        dispatch(setIsHost(false));
        dispatch(setIsGoing(true));

        if (searchParams.has('page')) {
            searchParams.set('page', '1');
        }
        if (searchParams.has('pageSize')) {
            searchParams.set('pageSize', '8');
        }
        searchParams.delete('isHost');
        searchParams.set('isGoing', 'true');
        searchParams.set('isCancelled', 'true');

        navigate({ search: searchParams.toString() });
    }

    const handleReset = (e : React.MouseEvent<HTMLButtonElement, MouseEvent>) => {
        e.preventDefault();

        const today = format(new Date(), 'yyyy-MM-dd');
        dispatch(setStartDate(today)); // 使用默认日期，如今天
        dispatch(setIsHost(false));
        dispatch(setIsGoing(false));
        dispatch(setCategory('')); // 假设''是category的默认值


        if (searchParams.has('page')) {
            searchParams.set('page', '1');
        }
        if (searchParams.has('pageSize')) {
            searchParams.set('pageSize', '8');
        }

        searchParams.set('startDate', today);
        searchParams.delete('isHost');
        searchParams.delete('isGoing');
        searchParams.delete('isCancelled')
        searchParams.delete('category'); // 或设置为默认category值

        navigate({ search: searchParams.toString() });
    };

    return (
        <Box sx={{
            width: '100%', // Ensures the container takes full width
        }}>
            <Grid container>
                <Grid item md={6} lg={5}>
                    <CategoriesSelect/>
                </Grid>
                <Grid item md={2} lg={2}>
                    <Button onClick={handleHost} variant={"text"} color={"primary"}>
                        Host
                    </Button>
                </Grid>
                <Grid item md={2} lg={2}>
                    <Button onClick={handleGoing} variant={"text"} color={"primary"}>
                        Going
                    </Button>
                </Grid>
                <Grid item md={2} lg={3}>
                    <Button onClick={handleReset} variant={"text"} color={"primary"}>
                        {isMd ? 'Reset' :'Reset filters'}
                    </Button>
                </Grid>
            </Grid>
            <Divider sx={{ borderWidth: 1.2, borderColor: 'rgb(162,162,162)' ,marginY:{
                md:theme.spacing(0.5),
                lg:theme.spacing(1.5),
                }}} />
        </Box>
    );
}

export default ActivityFilter;