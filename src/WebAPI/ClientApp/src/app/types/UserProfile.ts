import {Photo} from "../types/Photo";
import {Activity} from "../types/Activity";
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
    photos: Photo[]; // Define PhotoDTO interface based on its structure
    activities: Activity[]; // Define ActivityWithHostUserDTO interface based on its structure
}
