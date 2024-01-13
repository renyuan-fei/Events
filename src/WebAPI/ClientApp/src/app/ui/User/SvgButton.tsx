import IconButton from "@mui/material/IconButton";
import {Grid, SvgIcon} from "@mui/material";
import Typography from "@mui/material/Typography";
import {theme} from "@config/CustomTheme.ts";
// import {useNavigate} from "react-router";

interface SvgButtonProps {
    svg: string;
    title: string;
    path?: string;
}

function SvgButton({svg, title, path}: SvgButtonProps) {

    // const navigate = useNavigate(); // Hook to get navigate function

    function handleOnClick() {
        if (path) {
            // navigate(path);
        }
    }

    return (
        <IconButton aria-label="fingerprint" color="default" onClick={handleOnClick} sx={{
            "&:hover": {
                // Remove the default hover style from IconButton
                backgroundColor: 'transparent',
            },
            "&:hover .icon, &:hover .text": {
                // Apply primary color to SvgIcon and Typography on hover
                color: theme.palette.primary.light,
                transition: 'color 0.3s ease',
                cursor: 'pointer',
            },
            "& .icon": {
                height: 24,
                width: 24,
            },
            "& .text": {
                fontWeight: 600,
            }
        }}>
            <Grid container alignItems="center" justifyContent="center">
                <Grid item xs={12}>
                    <SvgIcon className="icon">
                        <path d={svg}></path>
                    </SvgIcon>
                </Grid>
                <Grid item xs={12}>
                    <Typography variant="body2" className="text">
                        {title}
                    </Typography>
                </Grid>
            </Grid>
        </IconButton>
    );
}

export default SvgButton;
