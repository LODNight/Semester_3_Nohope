import { Observable, of, Subject } from 'rxjs';
import { Injectable } from '@angular/core';
import { BaseURLService } from '../base-url.service';
import { HttpClient } from '@angular/common/http';
import { CouponsType } from '../../models/coupon/coupons-type.model';

@Injectable({
  providedIn: 'root'
})
export class ProductStyleService {

  // for changing when create, edit, delete => reload
  private styleChangeSubject = new Subject<void>();

  get styleChange$(): Observable<void> {
    return this.styleChangeSubject.asObservable();
  }

  notifyStyleChange(): void {
    this.styleChangeSubject.next();
  }

  constructor(
    private baseUrlService: BaseURLService,
    private httpClient: HttpClient
  ) {
  }

  findAll(): Observable<CouponsType[]> {
    const url: string = `${this.baseUrlService.baseURL}/style`
    return this.httpClient.get<CouponsType[]>(url)
  }
}
