export interface Comment {
    id: string;
    userId: string;
    displayName?: string;
    userName?: string;
    bio?: string;
    created: Date;
    body: string;
    image: string;
}
