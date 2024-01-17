import { useMutation, UseMutationOptions } from 'react-query';
import { useAppDispatch } from "@store/store.ts";

// 定义泛型 AsyncFunction，它是一个返回 Promise 的函数
type AsyncFunction<TData, TVariables> = (variables: TVariables) => Promise<TData>;

export const useGenericMutation = <TData, TVariables>(
    asyncFunction: AsyncFunction<TData, TVariables>,
    options?: UseMutationOptions<TData, unknown, TVariables>
) => {
    // @ts-ignore
    const dispatch = useAppDispatch();

    // 定义默认的成功和失败处理逻辑
    const defaultOptions: UseMutationOptions<TData, unknown, TVariables> = {
        // @ts-ignore
        onSuccess: (data) => {

        },
        // @ts-ignore
        onError: (error: any) => {

        },
        ...options, // 合并外部传入的 options
    };

    return useMutation<TData, unknown, TVariables>(asyncFunction, defaultOptions);
};
