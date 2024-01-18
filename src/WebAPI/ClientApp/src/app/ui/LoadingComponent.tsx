import {Backdrop, CircularProgress} from "@mui/material";

export function LoadingComponent() {
    return (
        <Backdrop
            sx={{ color: '#fff', zIndex: (theme) => theme.zIndex.drawer + 1 }}
            open={true}
        >
            <CircularProgress color="inherit" size={80}  thickness={4}  />
        </Backdrop>
    );
}