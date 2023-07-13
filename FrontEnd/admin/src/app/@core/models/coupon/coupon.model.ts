import { CouponsType } from "./coupons-type.model";

export class Coupon {
    couponId: number;
    couponType?: CouponsType
    couponName: string;
    discount: number;
    description: string;
    createdAt: Date;
    expiredAt: Date;
}