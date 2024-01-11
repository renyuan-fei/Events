import React from 'react';
import TextField from '@mui/material/TextField';

interface CustomTextFieldProps {
    label: string;
    type: string;
    autoComplete?: string;
    fullWidth?: boolean;
    margin?: 'none' | 'dense' | 'normal';
    variant?: 'filled' | 'outlined' | 'standard';
}

const CustomTextField: React.FC<CustomTextFieldProps> = ({
                                                             label,
                                                             type,
                                                             autoComplete = 'off',
                                                             fullWidth = true,
                                                             margin = 'dense',
                                                             variant = 'outlined',
                                                         }) => {
    return (
        <TextField
            label={label}
            type={type}
            fullWidth={fullWidth}
            variant={variant}
            margin={margin}
            autoComplete={autoComplete}
            sx={{
                '& label.Mui-focused': {
                    color: '#00798A',
                },
                '& .MuiOutlinedInput-root': {
                    '&.Mui-focused fieldset': {
                        borderColor: '#00798A',
                    },
                },
            }}
        />
    );
};

export default CustomTextField;
