import React from 'react';
import { Controller, useFormContext } from 'react-hook-form';
import { AdapterDateFns } from '@mui/x-date-pickers/AdapterDateFns';
import { LocalizationProvider } from '@mui/x-date-pickers/LocalizationProvider';
import {DateTimePicker} from "@mui/x-date-pickers";

interface DatePickerFieldProps {
    name: string;
    label: string;
}

const DatePickerField: React.FC<DatePickerFieldProps> = ({ name, label }) => {
    const { control } = useFormContext();

    return (
        <LocalizationProvider dateAdapter={AdapterDateFns}>
            <Controller
                name={name}
                control={control}
                defaultValue={new Date()} // 设置默认值为当前时间
                render={({ field: { ref, ...restField } }) => (
                    <DateTimePicker
                        label={label}
                        {...restField}
                        onChange={(newValue) => {
                            restField.onChange(newValue);
                        }}
                    />
                )}
            />
        </LocalizationProvider>
    );
};

export default DatePickerField;
