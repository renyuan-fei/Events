import {DateCalendar} from "@mui/x-date-pickers";
import { LocalizationProvider } from "@mui/x-date-pickers/LocalizationProvider";
import { AdapterDateFns } from "@mui/x-date-pickers/AdapterDateFns";
import {Paper, useTheme} from "@mui/material";
import {useNavigate} from "react-router";
import { format } from 'date-fns';

const ActivitiesCalendar = () => {
    const theme = useTheme();
    const navigate = useNavigate();
    const searchParams = new URLSearchParams(location.search);

    const handleDateChange = (date: Date | null) => {
        // Format the date to YYYY-MM-DD which is a common format for URL parameters
        const formattedDate = format(date!, 'yyyy-MM-dd');

        searchParams.set('startdate', formattedDate);

        if (searchParams.has('page')) {
            searchParams.set('page', '1');
        }
        if (searchParams.has('pageSize')) {
            searchParams.set('pageSize', '8');
        }

        navigate({ search: searchParams.toString() });

    };

    return (
        <Paper sx={{ padding: theme.spacing(2) , margin: theme.spacing(2) , height: '362px', width: '368px'}}>
            <LocalizationProvider dateAdapter={AdapterDateFns}>
                <DateCalendar
                    onChange={handleDateChange}
                    displayWeekNumber
                    sx={{
                        width: '100%',
                        height: '362',
                    }}
                    slots={{
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
