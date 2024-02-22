import {Photo} from "@type/Photo.ts";

export interface TopPhotos {
    photos: Photo[];
    remainingCount: number;
}