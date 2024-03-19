import {useQuery} from "react-query";
import {TopPhotos} from "@type/TopPhotos.ts";
import {AxiosError} from "axios";
import {ApiResponse} from "@type/ApiResponse.ts";
import {getTopPhotos} from "@apis/Photos.ts";

const useTopPhotosQuery = (id: string) => {
    const {data, isLoading} = useQuery<TopPhotos,AxiosError<ApiResponse<any>>>(
        ["topPhotos",id],
        () => getTopPhotos(id),
        {
            onError(error: AxiosError<ApiResponse<any>>) {
                console.log(error);
            }
        }
    )
    return {data, isPhotosLoading:isLoading};
}

export default useTopPhotosQuery;