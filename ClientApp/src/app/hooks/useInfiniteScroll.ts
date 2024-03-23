import { useRef, useEffect } from 'react';

const useInfiniteScroll = (
    callback: (() => void) | null,
    hasNextPage: boolean,
    isLoading: boolean,
    timeout: number = 500
) => {
    const loadMoreRef = useRef(null);
    const loadingTimeout = useRef<NodeJS.Timeout | null>(null);

    useEffect(() => {
        const observer = new IntersectionObserver(
            (entries) => {
                if (entries[0].isIntersecting && hasNextPage && !isLoading && callback) {
                    if (loadingTimeout.current) {
                        clearTimeout(loadingTimeout.current);
                    }
                    loadingTimeout.current = setTimeout(callback, timeout); // 延迟触发回调
                } else if (loadingTimeout.current) {
                    clearTimeout(loadingTimeout.current);
                }
            },
            {
                rootMargin: '100px',
            }
        );

        if (loadMoreRef.current) {
            observer.observe(loadMoreRef.current);
        }

        return () => {
            observer.disconnect();
            if (loadingTimeout.current) {
                clearTimeout(loadingTimeout.current);
            }
        };
    }, [callback, hasNextPage, isLoading]);

    return loadMoreRef;
};

export default useInfiniteScroll;
