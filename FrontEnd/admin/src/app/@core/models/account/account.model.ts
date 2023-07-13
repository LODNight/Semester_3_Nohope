import { Cart } from "../cart/cart.model";
import { Roles } from "./roles.model"
import { Wishlist } from "./wishlist.model";
import { Address } from "../address/address.model";
import { Order } from "../order/order.model";

export class Account {
    accountId: number
    email: string;
    password?: string;
    firstName: string;
    lastName: string;
    phoneNumber: string;
    gender: string;
    avatar: string;
    status: boolean; 
    securityCode: string;
    role?: Roles;
    createdAt: Date
    updatedAt: Date
    cart?: Cart;
    address?: Address[]
    wishlist?: Wishlist[];
    orders?: Order[]
}