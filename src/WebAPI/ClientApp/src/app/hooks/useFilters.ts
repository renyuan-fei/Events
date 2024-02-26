import { useLocation } from 'react-router-dom';

export const useFilters = () => {
    const location = useLocation();
    const searchParams = new URLSearchParams(location.search);

    const filters: Record<string, string> = {};

    searchParams.forEach((value, key) => {
        if (key !== 'page' && key !== 'pageSize') {
            filters[key] = value;
        }
    });

    return filters;
};
