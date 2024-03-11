import {NotificationType} from "@type/NotificationType.ts";

export interface NotificationMessage {
    id: string;
    content: string;
    relatedId: string;
    type: NotificationType;
    status: boolean;
}