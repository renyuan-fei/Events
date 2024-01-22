import {Attendee} from "./Attendee.ts";

export interface Activity {
    id: string;
    title: string;
    imageUrl: string;
    date: string; // or Date if you plan to convert the string to a Date object
    description: string;
    category: string;
    city: string;
    venue: string;
    hostUser: {
        username: string;
        id: string;
        imageUrl: string;
    };
    isCancelled: boolean;
    attendees: Attendee[];
}
