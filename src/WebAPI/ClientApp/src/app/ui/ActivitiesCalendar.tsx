import { DateCalendar } from "@mui/x-date-pickers";
import { LocalizationProvider } from "@mui/x-date-pickers/LocalizationProvider";
import { AdapterDateFns } from "@mui/x-date-pickers/AdapterDateFns";
import Box from "@mui/material/Box";

export function ActivitiesCalendar() {
    return (
        <Box style={{ position: 'sticky', top: '160px' }}>
            <LocalizationProvider dateAdapter={AdapterDateFns}>
                <DateCalendar sx={{
                    width: '100%', maxWidth: '360px'
                }}/>
            </LocalizationProvider>
        </Box>
    );
}

