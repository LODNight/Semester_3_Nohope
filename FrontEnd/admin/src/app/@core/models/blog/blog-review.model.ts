import { Account } from "../account/account.model";
import { Blogs } from "./blogs.model";

export class BlogReview {
    blogReviewId: number;
    blog?: Blogs;
    account: Account;
    content: string;
    createdAt: Date
    updatedAt: Date
}