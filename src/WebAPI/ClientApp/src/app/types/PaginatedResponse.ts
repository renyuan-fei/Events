type Item = {
    id: string;
    title: string;
    imageUrl: string;
    date: string;
    category: string;
    city: string;
    venue: string;
    goingCount: number;
    hostUser: {
        username: string;
        id: string;
    };
};

type PaginatedResponse<T> = {
    items: T[];
    pageNumber: number;
    totalPages: number;
    totalCount: number;
    hasPreviousPage: boolean;
    hasNextPage: boolean;
};
