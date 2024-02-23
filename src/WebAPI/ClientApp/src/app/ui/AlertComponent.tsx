import {Alert, Snackbar} from "@mui/material";
import {useDispatch, useSelector} from "react-redux";
import {setAlertInfo} from "@features/commonSlice.ts";
import {RootState} from "@store/store.ts";

interface SnackbarProps {
    vertical?: 'top' | 'bottom';
    horizontal?: 'center' | 'left' | 'right';
    severity: 'error' | 'success' | 'warning' | 'info';
    message: string;
}

const AlertComponent = ({
                                   vertical = 'top',
                                   horizontal = 'center',
                                   severity = 'info',
                                   message,
                               }: SnackbarProps) => {

    const dispatch = useDispatch();
    const open = useSelector((state: RootState) => state.common.alertInfo.open);

    const handleClose = () => {
        dispatch(setAlertInfo({open: false}));
    };

    return (
        <Snackbar anchorOrigin={{vertical: vertical, horizontal: horizontal}}
                  open={open}
                  autoHideDuration={3000}
                  onClose={handleClose}>
            <Alert onClose={handleClose} severity={severity} sx={{width: '100%'}}>
                {message}
            </Alert>
        </Snackbar>
    );
}

export default AlertComponent