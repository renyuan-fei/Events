import {useDispatch, useSelector} from "react-redux";
import {RootState} from "@store/store.ts";
import {getPaginatedNotifications} from "@apis/Notifications.ts";
import {useQuery} from "react-query";
import {
    loadPaginatedNotifications,
    setInitialTimeStamp, setPageNumber
} from "@features/notification/NotificationSlice.ts";
import {useEffect} from "react";

const useGetPaginatedNotifications = () => {
    const dispatch = useDispatch();
    const {pageNumber,pageSize,initialTimeStamp} = useSelector((state: RootState) => state.notification);

    useEffect(() => {
        // 仅在组件挂载时执行，设置初始时间戳
        if (pageNumber === 1) {
            const timeStamp = new Date().toISOString();
            dispatch(setInitialTimeStamp(timeStamp));
        }
    }, [dispatch]);


    const {
        refetch,
        isLoading
    } = useQuery(
        ['notifications', pageNumber, initialTimeStamp],
        () => getPaginatedNotifications(pageNumber, pageSize, initialTimeStamp),
        {
            enabled: pageNumber === 1 && initialTimeStamp!== new Date('9999-12-31T23:59:59Z').toISOString(),
            onSuccess: (data) => {
                console.log(data);
                if (data.hasNextPage){
                    console.log("set page");
                    dispatch(setPageNumber(pageNumber + 1));
                }
                dispatch(loadPaginatedNotifications(data));
            },
        },
    );

    return {
        refetch,
        isLoading
    };
}

export default useGetPaginatedNotifications;