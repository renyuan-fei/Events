import {forwardRef} from 'react';
import { TextField, TextFieldProps, useTheme } from '@mui/material';

const CustomTextField = forwardRef<HTMLDivElement, TextFieldProps>((props: TextFieldProps, ref) => {
    const theme = useTheme();

    // 使用主题值自定义样式
    const customStyles = {
        '& label.Mui-focused': {
            color: theme.palette.primary.main,
        },
        '& .MuiOutlinedInput-root': {
            '&.Mui-focused fieldset': {
                borderColor: theme.palette.primary.main,
            },
            borderRadius: 0.5 * theme.shape.borderRadius,
        },
    };

    return (
        <TextField
            {...props}
            variant="outlined"
            fullWidth
            sx={{ ...customStyles, ...props.sx }}
            inputRef={ref} // 使用 inputRef 而不是 ref
        />
    );
});

export default CustomTextField;
