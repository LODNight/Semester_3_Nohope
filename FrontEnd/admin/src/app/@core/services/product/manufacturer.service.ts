import { Injectable } from '@angular/core';
import { BaseURLService } from '../base-url.service';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs-compat';
import { of, BehaviorSubject, Subject } from 'rxjs';
import { Manufacturer } from '../../models/product/manufacturer';

@Injectable({
  providedIn: 'root'
})
export class ManufacturerService {
// change between add & edit form
  private stateSubject: BehaviorSubject<string> = new BehaviorSubject<string>('add');
  private rowDataSubject: BehaviorSubject<Manufacturer> = new BehaviorSubject<Manufacturer>(null);

  public state$: Observable<string> = this.stateSubject.asObservable();
  public rowData$: Observable<any> = this.rowDataSubject.asObservable();

  updateHandleAndRowData(state: string, rowData?: any) {
    this.stateSubject.next(state);
    if(rowData != undefined) {
      this.rowDataSubject.next(rowData as Manufacturer); 
    }
  }

  private manufacturerChangeSubject = new Subject<void>();

  get manufacturerChange$(): Observable<void> {
    return this.manufacturerChangeSubject.asObservable();
  }

  notifyManufacturerChange(): void {
    this.manufacturerChangeSubject.next();
  }
  
  constructor(
    private baseUrlService: BaseURLService,
    private httpClient: HttpClient
  ) { 
  }

  findAll(): Observable<Manufacturer[]> {
    const url: string = `${this.baseUrlService.baseURL}/manufacturer/findAll`
    return this.httpClient.get<Manufacturer[]>(url)
  }

  insert(manufacturer: Manufacturer): Observable<Manufacturer> {
    const url: string = `${this.baseUrlService.baseURL}/manufacturer/create`
    return this.httpClient.post<Manufacturer>(url, manufacturer);
  }

  update(manufacturer: Manufacturer): Observable<boolean> {
    const url: string = `${this.baseUrlService.baseURL}/manufacturer/update/${manufacturer.mftId}`
    return this.httpClient.post<boolean>(url, manufacturer);
  }

  delete(manufacturerId: number): Observable<boolean> {    
    const url: string = `${this.baseUrlService.baseURL}/manufacturer/delete/${manufacturerId}`
    return this.httpClient.get<boolean>(url); 
  }
}
