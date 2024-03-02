import {Attendee} from "@type/Attendee.ts";
import {Category} from "@type/Category.ts";

export interface Activity {
    id: string;
    title: string;
    imageUrl: string;
    date: Date;
    description: string;
    category: Category;
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
