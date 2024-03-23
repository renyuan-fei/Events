import { useFormContext } from 'react-hook-form';
import FormField from './FormField';
import useCityInfo from "../features/activity/hooks/useCityInfo";
import {useEffect} from "react"; // 假设你已经有一个基础的 FormField 组件

const AutoFillCityField = () => {
    const { cityName} = useCityInfo();
    const { setValue} = useFormContext();

    useEffect(() => {
        if (cityName) {
            setValue('city', cityName);
        }
    }, [cityName, setValue]);

    return (
        <FormField
            name='city'
            label='City'
            type='text'
        />
    );
};

export default AutoFillCityField;
