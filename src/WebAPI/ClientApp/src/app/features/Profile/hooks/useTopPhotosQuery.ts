import {useQuery} from "react-query";
import {TopPhotosWithRemainCount} from "@type/TopPhotosWithRemainCount.ts";
import {AxiosError} from "axios";
import {ApiResponse} from "@type/ApiResponse.ts";
import {getTopPhotos} from "@apis/Photos.ts";

const userTopPhotos = (id: string) => {
    const {data, isLoading} = useQuery<TopPhotosWithRemainCount,AxiosError<ApiResponse<any>>>(
        ["TopPhotos",id],
        () => getTopPhotos(id),
        {
            onError(error: AxiosError<ApiResponse<any>>) {
                console.log(error);
            }
        }
    )
    return {data, isPhotosLoading:isLoading};
}

export default userTopPhotos;