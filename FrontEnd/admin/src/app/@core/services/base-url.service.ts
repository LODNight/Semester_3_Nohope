import { Injectable } from "@angular/core";

@Injectable({
    providedIn: 'root'
})
export class BaseURLService {
    // Tín
    private _baseURL: string = "http://localhost:5271/api"

    //Chau
    // private _baseURL: string = "http://localhost:5208/api"
    get baseURL(): string{
        return this._baseURL ;
    }
}