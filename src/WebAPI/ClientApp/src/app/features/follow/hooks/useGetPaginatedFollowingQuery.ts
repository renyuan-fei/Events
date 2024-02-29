import {useQuery} from 'react-query';
import {getPaginatedFollowing} from '@apis/Following.ts';


const useGetPaginatedFollowingQuery = (Id: string, pageSize: number, page:number) => {
    // 使用 useQuery 钩子请求分页数据
    const {data, isLoading} = useQuery(
        ['following', Id, pageSize, page],
        () => getPaginatedFollowing(Id, pageSize, page),
        {
            keepPreviousData: true,
        }
    );

    return {following:data, isFollowingLoading:isLoading};
};

export default useGetPaginatedFollowingQuery;