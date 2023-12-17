import {ActivityAttendee} from "./ActivityAttendee.ts";

export interface Activity {
    Id: string,
    Title: string,
    Date: Date
    Category: string,
    Description: string
    City: string,
    Venue: string,
    HostName: string,
    IsCancelled: boolean,
    Attendees: Array<ActivityAttendee>,
}