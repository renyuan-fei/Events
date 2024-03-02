import CustomTextField from "@ui/Custom/CustomTextField.tsx";
import CustomPasswordTextField from "@ui/Custom/CustomPasswordTextField.tsx"; // 假设您有一个这样的组件
import { Controller, useFormContext } from "react-hook-form";

interface FormFieldProps {
    name: string;
    label: string;
    type?: 'text' | 'password' | 'email' | 'number';
    required?: boolean;
}

const FormField = ({
                       name,
                       label,
                       type = 'text',
                       required = true,
                   }: FormFieldProps) => {
    const Component = type === 'password' ? CustomPasswordTextField : CustomTextField;
    const { control, formState: { errors } } = useFormContext();

    return (
        <Controller
            name={name}
            control={control}
            render={({ field }) => (
                <Component
                    {...field}
                    label={label}
                    required={required}
                    error={!!errors[name]}
                    helperText={(errors[name]?.message || '') as string}
                />
            )}
        />
    );
};

export default FormField;
