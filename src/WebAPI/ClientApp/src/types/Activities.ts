declare namespace Activities {
    type Activity = {
        Id : string,
        Title ?: string,
        Date ?: Date
        Category ?: string,
        Description ?: string
        City ?: string,
        Venue ?: string,
        Attendees ?: Array<string>,
    }

    type ActivityAttendee = {
        Id : string,
        IsHost : boolean,
        AppUser ?: []
    }
}
