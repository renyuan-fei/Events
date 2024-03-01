import { Category } from "./Category";

export interface NewActivity {
    title: string;
    city: string;
    venue: string;
    description: string;
    date: Date,
    category: Category;
}