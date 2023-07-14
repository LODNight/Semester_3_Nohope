import { Observable, of, Subject } from 'rxjs';
import { Injectable } from '@angular/core';
import { BaseURLService } from '../base-url.service';
import { HttpClient } from '@angular/common/http';
import { Coupon } from '../../models/coupon/coupon.model';

@Injectable({
  providedIn: 'root'
})
export class CouponService {

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

  findAll(): Observable<Coupon[]> {
    const url: string = `${this.baseUrlService.baseURL}/style`
    return this.httpClient.get<Coupon[]>(url)
  }

  insert(coupon: Coupon): Observable<boolean> {
    const url: string = `${this.baseUrlService.baseURL}/style/create`
    return this.httpClient.post<boolean>(url, coupon);
  }

  update(coupon: Coupon): Observable<boolean> {
    const url: string = `${this.baseUrlService.baseURL}/style/update/${coupon.couponId}`
    return this.httpClient.post<boolean>(url, coupon);
  }

  delete(couponId: number): Observable<boolean> {
    const url: string = `${this.baseUrlService.baseURL}/style/delete/${couponId}`
    return this.httpClient.get<boolean>(url);
  }
}
