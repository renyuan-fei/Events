import { z } from 'zod';
import { Category } from "../types/Category";

const activitySchema = z.object({
    title: z.string().min(1, "Title is required"),
    description: z.string().min(1, "Description is required"),
    category: z.nativeEnum(Category),
    date: z.date(),
    city: z.string().min(1, "City is required"),
    venue: z.string().min(1, "Venue is required"),
}).refine((data) => {
    const now = new Date();
    const tomorrow = new Date(now);
    tomorrow.setDate(tomorrow.getDate() + 1);
    tomorrow.setHours(0, 0, 0, 0); // 将时间设置为午夜，确保整个明天都是有效的

    const inputDate = new Date(data.date);

    return inputDate >= tomorrow;
}, {
    message: "Date must be at least one day after the current date and time",
    path: ["date"],
});

export default activitySchema;
