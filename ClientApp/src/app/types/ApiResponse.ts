export interface ApiResponse<T> {
    statusCode: number;
    message: string;
    data: T ;
    timestamp: Date;
    metadata?: any;
}
