import React from "react";
import {Controller, useFormContext} from "react-hook-form";
import {Checkbox, FormControlLabel, useTheme} from "@mui/material";
interface FormCheckboxProps {
    name: string;
    label?: string;
}

const FormCheckbox: React.FC<FormCheckboxProps> = ({ name, label = '' }) => {
    const theme = useTheme();
    const {control} = useFormContext();

    return (
        <Controller
            name={name}
            control={control}
            render={({ field: { onChange, value } }) => (
                <FormControlLabel
                    control={
                        <Checkbox
                            onChange={onChange}
                            checked={value}
                            sx={{
                                padding: theme.spacing(2),
                                color: '#00798A', // 未选中时的颜色
                                '&.Mui-checked': {
                                    color: '#00798A', // 选中时的颜色
                                },
                            }}
                        />
                    }
                    label={label}
                />
            )}
        />
    );
};
export default FormCheckbox;
