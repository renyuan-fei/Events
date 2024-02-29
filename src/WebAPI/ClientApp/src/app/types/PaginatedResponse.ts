export interface Item {
    id: string;
    title: string;
    imageUrl: string;
    date: string;
    category: string;
    city: string;
    venue: string;
    goingCount: number;
    isCancelled: boolean;
    hostUser: {
        username: string;
        id: string;
    };
};

export interface PaginatedResponse<T> {
    items: T[];
    pageNumber: number;
    totalPages: number;
    totalCount: number;
    hasPreviousPage: boolean;
    hasNextPage: boolean;
};
