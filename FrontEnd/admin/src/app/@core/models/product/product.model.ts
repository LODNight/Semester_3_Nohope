import { Category } from "./category";
import { Manufacturer } from "./manufacturer";
import { ProductImages } from "./product-images.model";

export class Product {
    productId: number;
    productName: string;
    price: number;
    category: Category;
    description: string;
    quantity: number;
    detail: string;
    expireDate: Date
    manufacturer?: Manufacturer
    hide: boolean;
    createdAt: Date;
    updatedAt: Date;
    
    // optionals
    images?: ProductImages[]
    totalQuantity?: number
    quantitySold?: number
    totalLikes?: number
    rating?: number
    totalRating?: number
}