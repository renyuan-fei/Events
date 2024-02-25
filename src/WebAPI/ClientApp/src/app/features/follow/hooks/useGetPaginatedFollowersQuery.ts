import {useQuery} from 'react-query';
import {getPaginatedFollowers} from '@apis/Following.ts';
import {queryClient} from "@apis/queryClient.ts";

const useGetPaginatedFollowersQuery = (Id: string, pageSize: number, page: number) => {
    const {
        isLoading,
        data
    } = useQuery(
        ['followers', Id, page, pageSize],
        () => getPaginatedFollowers(Id, pageSize, page),
        {
            keepPreviousData: true,
            onSuccess: () => {
                queryClient.invalidateQueries(['followers', Id, pageSize, page]);
            }
        }
    );

    return {followers:data,isFollowersLoading:isLoading};
};

export default useGetPaginatedFollowersQuery;
