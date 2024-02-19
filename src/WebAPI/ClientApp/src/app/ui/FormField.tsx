import CustomTextField from "@ui/Custom/CustomTextField.tsx";
import CustomPasswordTextField from "@ui/Custom/CustomPasswordTextField.tsx"; // 假设您有一个这样的组件
import { Controller } from "react-hook-form";

interface FormFieldProps {
    name: string;
    control: any;
    errors: Record<string, any>;
    label: string;
    type?: string;
    required?: boolean;
}

const FormField = ({
                       name,
                       control,
                       errors,
                       label,
                       type = 'text',
                       required = false,
                   }: FormFieldProps) => {
    // 根据 type 选择要使用的组件
    const Component = type === 'password' ? CustomPasswordTextField : CustomTextField;
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
                    helperText={errors[name]?.message || ''}
                />
            )}
        />
    );
};

export default FormField;
