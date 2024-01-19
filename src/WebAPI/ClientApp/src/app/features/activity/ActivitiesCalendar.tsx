import {DateCalendar} from "@mui/x-date-pickers";
import { LocalizationProvider } from "@mui/x-date-pickers/LocalizationProvider";
import { AdapterDateFns } from "@mui/x-date-pickers/AdapterDateFns";
import {Paper, useTheme} from "@mui/material";

export function ActivitiesCalendar() {
    const theme = useTheme();

    return (
        <Paper sx={{ padding: theme.spacing(2) , margin: theme.spacing(2)}}>
            <LocalizationProvider dateAdapter={AdapterDateFns}>
                <DateCalendar
                    displayWeekNumber
                    sx={{
                        width: '100%',
                        maxWidth: '360px'
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
