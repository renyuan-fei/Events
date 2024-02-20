import {Photo} from "@type/Photo.ts";

export interface TopPhotosWithRemainCount {
    photos: Photo[];
    remainingCount: number;
}