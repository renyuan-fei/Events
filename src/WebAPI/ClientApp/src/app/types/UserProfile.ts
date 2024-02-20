export interface UserProfile {
    id: string; // Assuming Guid is represented as a string in TypeScript
    displayName: string;
    bio: string;
    userName: string;
    email: string;
    phoneNumber: string;
    image: string;
    followers: number;
    following: number;
}
