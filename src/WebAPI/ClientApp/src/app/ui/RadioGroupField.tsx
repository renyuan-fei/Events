import React from 'react';
import { Controller, useFormContext } from 'react-hook-form';
import { FormControl, FormControlLabel, FormLabel, Radio, RadioGroup } from '@mui/material';

interface Option {
    value: string;
    label: string;
}

interface RadioGroupFieldProps {
    name: string;
    label: string;
    options: Option[];
    defaultValue?: string;
}

const RadioGroupField: React.FC<RadioGroupFieldProps> = ({ name, label, options, defaultValue }) => {
    const { control } = useFormContext(); // 使用 useFormContext 来访问 react-hook-form 的控制器

    return (
        <FormControl component="fieldset">
            <FormLabel component="legend">{label}</FormLabel>
            <Controller
                name={name}
                control={control}
                defaultValue={defaultValue}
                render={({ field }) => (
                    <RadioGroup
                        {...field}
                        defaultValue={defaultValue}
                    >
                        {options.map((option) => (
                            <FormControlLabel
                                key={option.value}
                                value={option.value}
                                control={<Radio />}
                                label={option.label}
                            />
                        ))}
                    </RadioGroup>
                )}
            />
        </FormControl>
    );
};

export default RadioGroupField;
