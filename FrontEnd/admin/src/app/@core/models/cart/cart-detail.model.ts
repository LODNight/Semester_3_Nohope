import { Product } from "../product/product.model";
import { Cart } from "./cart.model";

export class CartDetail {
    cartDetailId: number;
    cartId: number;
    product?: Product;
    cart?: Cart;
    quantity: number;
    price: number;
    createdAt: Date;
    updatedAt: Date;
}