import { useFormContext } from 'react-hook-form';
import FormField from './FormField';
import useCityInfo from "../features/activity/hooks/useCityInfo";
import {useEffect} from "react"; // 假设你已经有一个基础的 FormField 组件

const AutoFillCityField = () => {
    const { cityName} = useCityInfo();
    const { setValue,control,formState: { errors } } = useFormContext(); // 使用 useFormContext 来获取 setValue 方法

    // 当 cityName 有值时，自动填充表单的城市字段
    useEffect(() => {
        if (cityName) {
            setValue('city', cityName); // 假设表单中城市字段的名称为 'city'
        }
    }, [cityName, setValue]);

    return (
        <FormField
            name='city'
            label='City'
            type='text'
            control={control}
            required={true}
            errors={errors}
        />
    );
};

export default AutoFillCityField;
