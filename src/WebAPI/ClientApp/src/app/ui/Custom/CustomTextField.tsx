import { TextField, TextFieldProps, useTheme } from '@mui/material';

function CustomTextField(props: TextFieldProps) {
    const theme = useTheme();

    // Use theme values for custom styles
    const customStyles = {
        '& label.Mui-focused': {
            color: theme.palette.primary.main, // Use primary color from theme
        },
        '& .MuiOutlinedInput-root': {
            '&.Mui-focused fieldset': {
                borderColor: theme.palette.primary.main, // Use primary color from theme
            },
            borderRadius: 0.5 * theme.shape.borderRadius, // Use border radius from theme
        },
    };

    return (
        <TextField
            {...props}
            variant="outlined"
            fullWidth
            sx={{ ...customStyles, ...props.sx }}
        />
    );
}

export default CustomTextField;
