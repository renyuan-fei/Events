export interface ChatComments {
    id: string;
    userId: string;
    displayName?: string; // '?' 表示该属性是可选的
    userName?: string;
    bio?: string;
    created: Date;
    body: string;
    image: string;
}