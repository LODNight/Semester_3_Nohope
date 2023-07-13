import { Injectable } from '@angular/core';
import { Observable, of, Subject } from 'rxjs';
import { Order } from '../../models/order/order.model';
import { BaseURLService } from '../base-url.service';
import { HttpClient } from '@angular/common/http';
import { OrderStatus } from '../../models/order/order-status.model';
import { OrderStatusService } from './order-status.service';
import { PaymentMethodService } from './payment-method.service';

@Injectable({
  providedIn: 'root'
})
export class OrderService {

  private orderChangeSubject = new Subject<void>();

  get orderChange$(): Observable<void> {
    return this.orderChangeSubject.asObservable();
  }

  notifyOrderChange(): void {
    this.orderChangeSubject.next();
  }

  constructor(
    private baseUrlService: BaseURLService,
    private httpClient: HttpClient,
    private orderStatusService: OrderStatusService,
    private paymentMethodService: PaymentMethodService
  ) { }


  findById(id: number): Observable<Order> {
    const url: string = `${this.baseUrlService.baseURL}/order/${id}`
    return this.httpClient.get<Order>(url)
  }
  

  findAll(): Observable<Order[]> {
    const url: string = `${this.baseUrlService.baseURL}/order`
    return this.httpClient.get<Order[] >(url)
  }

  insert(order: Order): Observable<Order> {
    const url: string = `${this.baseUrlService.baseURL}/order/create`
    return this.httpClient.post<Order>(url, order);
  }

  updateOrderStatus(orderId: number, orderStatus: OrderStatus): Observable<boolean> {
    const url: string = `${this.baseUrlService.baseURL}/order/update/${orderId}`
    return this.httpClient.post<boolean>(url, orderStatus);
  }

  delete(orderId: number): Observable<boolean> {    
    const url: string = `${this.baseUrlService.baseURL}/order/delete/${orderId}`
    return this.httpClient.delete<boolean>(url); 
  }

  findOrderStatusById(orderId: number): Observable<OrderStatus> {
    const url: string = `${this.baseUrlService.baseURL}/order/${orderId}/order-status`
    return this.httpClient.get<OrderStatus>(url); 
  }
}
