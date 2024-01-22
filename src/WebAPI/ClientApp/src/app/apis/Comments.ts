export interface Comment {
    id: string;
    userId: string;
    displayName?: string;
    userName?: string;
    bio?: string;
    createdAt: Date;
    body: string;
    image: string;
}
