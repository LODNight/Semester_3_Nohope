import { Account } from "../account/account.model";

export class Blogs {
    blogId: number;
    blogName: string;
    blogImage: string;
    shortDescription: string
    longDescription: string
    hide: boolean;
    createdAt: Date
    updatedAt: Date
}