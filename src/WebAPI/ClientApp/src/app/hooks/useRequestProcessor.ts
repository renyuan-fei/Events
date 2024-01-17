import { useQuery, useMutation, useQueryClient } from 'react-query';
import {ApiClient} from "@apis/BaseApi.ts";


export function useRequestProcessor() {
    const queryClient = useQueryClient();

    function query(key : string, queryFunction : ApiClient, options = {}) {
        return useQuery({
            queryKey: key,
            queryFn: queryFunction,
            ...options,
        });
    }

    function mutate(key : string, mutationFunction : ApiClient, options = {}) {
        return useMutation({
            mutationKey: key,
            mutationFn: mutationFunction,
            onSettled: () => queryClient.invalidateQueries(key),
            ...options,
        });
    }

    return { query, mutate };
}