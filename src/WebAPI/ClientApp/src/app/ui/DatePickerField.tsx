import React from 'react';
import {Controller, useFormContext} from 'react-hook-form';
import {AdapterDateFns} from '@mui/x-date-pickers/AdapterDateFns';
import {LocalizationProvider} from '@mui/x-date-pickers/LocalizationProvider';
import {DateTimePicker} from "@mui/x-date-pickers";

interface DatePickerFieldProps {
    name: string;
    label: string;
}

const DatePickerField: React.FC<DatePickerFieldProps> = ({name, label}) => {
    const {control, formState: {errors}} = useFormContext();

    return (
        <Controller
            name={name}
            control={control}
            defaultValue={new Date()} // 设置默认值为当前时间
            render={({field: {ref, ...restField}}) => (
                <LocalizationProvider dateAdapter={AdapterDateFns}>
                    <DateTimePicker
                        label={label}
                        {...restField}
                        onChange={(newValue) => {
                            restField.onChange(newValue);
                        }}
                        slotProps={{
                            textField: {
                                error: !!errors[name], // 如果存在错误，则设置error属性为true
                                helperText: (errors[name]?.message || '') as string || '', // 仅当存在错误消息时显示，否则为null
                            }
                        }}
                    />
                </LocalizationProvider>
            )}
        />
    );
};

export default DatePickerField;
