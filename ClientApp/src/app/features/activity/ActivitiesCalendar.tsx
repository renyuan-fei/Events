import React, {useEffect} from "react";
import {DateCalendar} from "@mui/x-date-pickers";
import {LocalizationProvider} from "@mui/x-date-pickers/LocalizationProvider";
import {AdapterDateFns} from "@mui/x-date-pickers/AdapterDateFns";
import {Paper, useTheme} from "@mui/material";
import {useNavigate} from "react-router";
import {format, parseISO} from 'date-fns';
import {useDispatch, useSelector} from "react-redux";
import {RootState} from "@store/store.ts";
import {setStartDate} from "@features/commonSlice.ts";

const ActivitiesCalendar: React.FC = () => {
    const theme = useTheme();
    const navigate = useNavigate();
    const dispatch = useDispatch();

    const reduxDate = useSelector((state: RootState) => state.common.startDate);
    const selectedDate = reduxDate ? parseISO(reduxDate) : new Date();

    const hash = window.location.hash;
    const queryString = hash.substring(hash.indexOf('?'));
    const searchParams = new URLSearchParams(queryString);

    const handleDateChange = (date: Date | null) => {
        if (date) {
            const formattedDate = format(date, 'yyyy-MM-dd');
            dispatch(setStartDate(formattedDate)); // Update Redux state
            searchParams.set('startDate', formattedDate);
            updateUrlParams(formattedDate);
        }
    };

    const updateUrlParams = (date: string) => {
        if (searchParams.has('page')) {
            searchParams.set('page', '1');
        }
        if (searchParams.has('pageSize')) {
            searchParams.set('pageSize', '8');
        }
        searchParams.set('startDate', date);
        navigate({search: searchParams.toString()});
    };

    useEffect(() => {
        const urlDate = searchParams.get('startDate') || format(new Date(), 'yyyy-MM-dd');
        if (reduxDate !== urlDate) {
            dispatch(setStartDate(urlDate)); // Sync with URL or current date
            updateUrlParams(urlDate);
        }
    }, [navigate, reduxDate, searchParams, dispatch]);

    return (
        <Paper sx={{
            padding: theme.spacing(2),
            margin: theme.spacing(2),
            height: {md:350,lg:362},
            width: {md:320,lg:368}
        }}>
            <LocalizationProvider dateAdapter={AdapterDateFns}>
                <DateCalendar
                    value={selectedDate}
                    onChange={handleDateChange}
                    displayWeekNumber
                    sx={{
                        width: '100%',
                        height: 362,
                    }}
                    slotProps={{
                        leftArrowIcon: {
                            sx: {
                                color: theme.palette.primary.main,
                            },
                        },
                        rightArrowIcon: {
                            sx: {
                                color: theme.palette.primary.main,
                            },
                        },
                        calendarHeader: {
                            sx: {
                                color: theme.palette.primary.main,
                            },
                        },
                    }}
                />
            </LocalizationProvider>
        </Paper>
    );
}

export default ActivitiesCalendar;
