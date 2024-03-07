import React, { useEffect, useState } from "react";
import {DateCalendar} from "@mui/x-date-pickers";
import { LocalizationProvider } from "@mui/x-date-pickers/LocalizationProvider";
import { AdapterDateFns } from "@mui/x-date-pickers/AdapterDateFns";
import {Paper, useTheme} from "@mui/material";
import {useNavigate} from "react-router";
import {format, parseISO} from 'date-fns';

const ActivitiesCalendar:React.FC = () => {
    const theme = useTheme();
    const navigate = useNavigate();

    const hash = window.location.hash;
    const queryString = hash.substring(hash.indexOf('?'));
    const searchParams = new URLSearchParams(queryString);

    const initialDate = searchParams.has('startDate') ? parseISO(searchParams.get('startDate')!) : new Date();
    const [selectedDate, setSelectedDate] = useState<Date>(initialDate);

    const handleDateChange = (date: Date | null) => {
        setSelectedDate(date!);
        const formattedDate = format(date!, 'yyyy-MM-dd');
        searchParams.set('startDate', formattedDate);
        if (searchParams.has('page')) {
            searchParams.set('page', '1');
        }
        if (searchParams.has('pageSize')) {
            searchParams.set('pageSize', '8');
        }
        navigate({ search: searchParams.toString() });
    };

    useEffect(() => {
        if (!searchParams.has('startDate')) {
            const today = format(new Date(), 'yyyy-MM-dd');
            searchParams.set('startDate', today);
            navigate({ search: searchParams.toString() });
        }
    }, []);

    return (
        <Paper sx={{ padding: theme.spacing(2), margin: theme.spacing(2), height: '362px', width: '368px'}}>
            <LocalizationProvider dateAdapter={AdapterDateFns}>
                <DateCalendar
                    value={selectedDate} // 设置日历组件的当前选定值
                    onChange={handleDateChange}
                    displayWeekNumber
                    sx={{
                        width: '100%',
                        height: '362',
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
