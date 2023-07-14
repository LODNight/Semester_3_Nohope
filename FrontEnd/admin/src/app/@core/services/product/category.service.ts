import { Injectable } from '@angular/core';
import { BaseURLService } from '../base-url.service';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs-compat';
import { of, BehaviorSubject, Subject } from 'rxjs';
import { Category } from '../../models/product/category';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {
// change between add & edit form
  private stateSubject: BehaviorSubject<string> = new BehaviorSubject<string>('add');
  private rowDataSubject: BehaviorSubject<Category> = new BehaviorSubject<Category>(null);

  public state$: Observable<string> = this.stateSubject.asObservable();
  public rowData$: Observable<any> = this.rowDataSubject.asObservable();

  updateHandleAndRowData(state: string, rowData?: any) {
    this.stateSubject.next(state);
    if(rowData != undefined) {
      this.rowDataSubject.next(rowData as Category); 
    }
  }

  // for changing when create, edit, delete => reload
  private categoryChangeSubject = new Subject<void>();

  // Getter for the subject as an observable
  get categoryChange$(): Observable<void> {
    return this.categoryChangeSubject.asObservable();
  }

  // Call this method whenever a change occurs in the product list
  notifyCategoryChange(): void {
    this.categoryChangeSubject.next();
  }
  
  constructor(
    private baseUrlService: BaseURLService,
    private httpClient: HttpClient
  ) { 
  }

  findAll(): Observable<Category[]> {
    const url: string = `${this.baseUrlService.baseURL}/category/findAll`
    return this.httpClient.get<Category[]>(url)
  }

  insert(category: Category): Observable<Category> {
    const url: string = `${this.baseUrlService.baseURL}/category/create`
    return this.httpClient.post<Category>(url, category);
  }

  update(category: Category): Observable<boolean> {
    const url: string = `${this.baseUrlService.baseURL}/category/edit`
    return this.httpClient.put<boolean>(url, category);
  }

  delete(categoryId: number): Observable<boolean> {    
    const url: string = `${this.baseUrlService.baseURL}/category/delete/${categoryId}`
    return this.httpClient.delete<boolean>(url); 
  }
}
