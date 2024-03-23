import {UserDetailBase} from "@type/UserDetailBase.ts";

export interface Attendee extends UserDetailBase {
    isHost: boolean;
    image: string;
}