import { z } from 'zod';
import {Category} from "../types/Category";

const activitySchema = z.object({
    image: z.instanceof(File).optional(),
    title: z.string().min(1, "Title is required"),
    description: z.string().min(1, "Description is required"),
    category: z.nativeEnum(Category),
    date: z.date(),
    city: z.string().min(1, "City is required"),
    venue: z.string().min(1, "Venue is required"),
});

type ActivityFormValues = z.infer<typeof activitySchema>;

export default activitySchema;
export type { ActivityFormValues };
