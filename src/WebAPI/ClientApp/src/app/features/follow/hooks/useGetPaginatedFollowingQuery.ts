import {useQuery} from 'react-query';
import {getPaginatedFollowing} from '@apis/Following.ts';


const useGetPaginatedFollowingQuery = (targetUserId: string, pageSize: number, page:number) => {
    // 使用 useQuery 钩子请求分页数据
    const {data, isLoading} = useQuery(
        ['getPaginatedFollowing', targetUserId, pageSize, page],
        () => getPaginatedFollowing(targetUserId, pageSize, page),
        {
            keepPreviousData: true, // 保持上一页数据直到新的数据加载
        }
    );

    return {following:data, isFollowingLoading:isLoading};
};

export default useGetPaginatedFollowingQuery;