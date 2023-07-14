import { Injectable } from '@angular/core';
import { BaseURLService } from '../base-url.service';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs-compat';
import { of, BehaviorSubject, Subject } from 'rxjs';
import { Coupon } from '../../models/coupon/coupon.model';
import { CouponsType } from '../../models/coupon/coupons-type.model';

@Injectable({
  providedIn: 'root'
})
export class ProductCouponService {

  // for update state & rowDate and change between add & edit form
  private stateSubject: BehaviorSubject<string> = new BehaviorSubject<string>('add');
  private rowDataSubject: BehaviorSubject<Coupon> = new BehaviorSubject<Coupon>(null);

  public state$: Observable<string> = this.stateSubject.asObservable();
  public rowData$: Observable<Coupon> = this.rowDataSubject.asObservable();

  updateHandleAndRowData(state: string, rowData?: any) {
    this.stateSubject.next(state);
    if(rowData != undefined) {
      this.rowDataSubject.next(rowData as Coupon); 
    }
  }

  // for changing when create, edit, delete => reload
  private couponChangeSubject = new Subject<void>();

  get couponChange$(): Observable<void> {
    return this.couponChangeSubject.asObservable();
  }

  notifyCouponChange(): void {
    this.couponChangeSubject.next();
  }

  baseUrl = "http://127.0.0.1:8000/api/";
  
  constructor(
    private baseUrlService: BaseURLService,
    private httpClient: HttpClient
  ) { 
  }

  findAll(): Observable<Coupon[]> {
    const url: string = `${this.baseUrlService.baseURL}/coupon`
    return this.httpClient.get<Coupon[]>(url)
  }

  insert(coupon: Coupon): Observable<boolean> {
    const url: string = `${this.baseUrlService.baseURL}/coupon/create`
    return this.httpClient.post<boolean>(url, coupon);
  }

  update(coupon: Coupon): Observable<boolean> {
    const url: string = `${this.baseUrlService.baseURL}/coupon/update/${coupon.couponId}`
    console.log(url);
    
    return this.httpClient.post<boolean>(url, coupon);
  }

  delete(couponId: number): Observable<boolean> {    
    const url: string = `${this.baseUrlService.baseURL}/coupon/delete/${couponId}`
    return this.httpClient.get<boolean>(url); 
  }

  findCouponTypeById(id: number): CouponsType {
    if(id == 1) {
      return { couponsTypeId: 1, nameType: 'percent' }
    } else {
      return { couponsTypeId: 2, nameType: 'fixed' }
    }
  }
  findAllCouponType(): Observable<CouponsType[]> {
    const url: string = `${this.baseUrlService.baseURL}/coupon-detail`
    return this.httpClient.get<CouponsType[]>(url)
  }

  isCouponExists(couponCode: string): Observable<boolean> {
    const url: string = `${this.baseUrlService.baseURL}/isCouponExists/${couponCode}`
    return this.httpClient.get<boolean>(url);
  }

  findIdByCode(couponCode: string): Observable<string> {
    const url: string = `${this.baseUrlService.baseURL}/findIdByCode/${couponCode}`
    return this.httpClient.get<string>(url);
  }
}
