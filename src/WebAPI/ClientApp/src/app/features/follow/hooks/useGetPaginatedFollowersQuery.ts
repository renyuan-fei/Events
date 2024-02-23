import {useQuery} from 'react-query';
import {getPaginatedFollowers} from '@apis/Following.ts';

const useGetPaginatedFollowersQuery = (userId: string, pageSize: number, page: number) => {
    const {
        isLoading,
        data
    } = useQuery(
        ['followers', userId, pageSize, page],
        () => getPaginatedFollowers(userId, pageSize, page),
        {
            keepPreviousData: true, // keep privious page data,until new data is fetched
        }
    );

    return {followers:data,isFollowersLoading:isLoading};
};

export default useGetPaginatedFollowersQuery;
