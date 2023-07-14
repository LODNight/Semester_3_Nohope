import { ProductColor } from "../product/product-color.model";
import { Product } from "../product/product.model";
import { Order } from "./order.model";

export class OrderDetail {
    orderDetailId: number;
    order?: Order
    productId?: number;
    quantity: number;
    price: number;

    createdAt: Date
    updatedAt: Date
}