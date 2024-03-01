import {useEffect, useState} from "react";
import {useQuery} from "react-query";
import getCityName from "../../../apis/Geocoding";
import { useDispatch } from "react-redux";
import {setAlertInfo} from "@features/commonSlice.ts";

const useCityInfo = () => {
    const [coords, setCoords] = useState<{ latitude: number; longitude: number } | null>(null);
    const dispatch = useDispatch();

    // 获取用户的当前位置
    useEffect(() => {
        navigator.geolocation.getCurrentPosition(
            (position) => {
                setCoords({
                    latitude: position.coords.latitude,
                    longitude: position.coords.longitude,
                });
            },
            (error) => {
                dispatch(setAlertInfo({
                    open: true,
                    severity: 'error',
                    message: 'You must allow location access to autofill the city field',
                }))
                console.error('Error getting location', error);
            }
        );
    }, []);

    // 使用react-query来管理API请求状态
    const { data: cityName, isLoading, error } = useQuery(
        ['fetchCityName', coords?.latitude, coords?.longitude],
        () => getCityName(coords!.latitude, coords!.longitude),
        {
            enabled: !!coords, // 只有当coords不为null时才执行查询
        }
    );

    return { cityName, isLoading, error };
};

export default useCityInfo;