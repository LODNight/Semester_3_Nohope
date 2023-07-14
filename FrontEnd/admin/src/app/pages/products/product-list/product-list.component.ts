import { takeUntil } from 'rxjs/operators';
import { Component, OnInit, AfterViewInit, ViewChild, ElementRef } from '@angular/core';
import { LocalDataSource } from 'ng2-smart-table';
import { Router } from '@angular/router';
import { CustomProductActionComponent } from './custom/custom-product-action.component';
import { CustomProductFilterActionsComponent } from './custom/custom-product-filter-actions.component';
import { ProductService } from '../../../@core/services/product/product.service';
import { CategoryService } from '../../../@core/services/product/category.service';
import { NbToastrService } from '@nebular/theme';
import { Product } from '../../../@core/models/product/product.model';
import { forkJoin, Subject } from 'rxjs';
import { UtilsService } from '../../../@core/services/utils.service';
import { Category } from '../../../@core/models/product/category';
import { CustomProductImageComponent } from './custom/custom-category-image.component';
import { Manufacturer } from '../../../@core/models/product/manufacturer';
import { ManufacturerService } from '../../../@core/services/product/manufacturer.service';

@Component({
  selector: 'ngx-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.scss']
})
export class ProductListComponent implements OnInit, AfterViewInit {
  private unsubscribe = new Subject<void>();
  numberOfItem: number = localStorage.getItem('itemPerPage') != null ? +localStorage.getItem('itemPerPage') : 10; // default
  source: LocalDataSource = new LocalDataSource();
  // Setting for List layout
  categories: Category[];
  manufacturers: Manufacturer[]

  settings = {
    actions: {
      position: 'right',
      edit: false,
      delete: false,
      add: false,
      columnTitle: ''
    },
    columns: {},
    pager: {
      display: true,
      perPage: this.numberOfItem
    },
  };


  constructor(
    private productService: ProductService,
    private router: Router,
    private categoryService: CategoryService,
    private manufacturerService: ManufacturerService,
    private toastService: NbToastrService,
    private utilsService: UtilsService
  ) {

  }

  ngOnInit(): void {
    const categoryObservable = this.categoryService.findAll();
    const manufacturerObservable = this.manufacturerService.findAll();
    
    // Combine the observables using forkJoin
    forkJoin([categoryObservable, manufacturerObservable]).subscribe(
      ([categoryData, mftData]) => {
        this.categories = categoryData;
        this.manufacturers = mftData
        // after run all of them, then load settings
        this.settings = {
          actions: {
            position: 'right',
            edit: false,
            delete: false,
            add: false,
            columnTitle: ''
          },
          columns: {
            productId: {
              title: 'ID',
              type: 'number',
              width: '3%'
            },
            image: {
              title: 'Image',
              type: 'custom',
              sort: false,
              filter: false,
              renderComponent: CustomProductImageComponent
            },
            productName: {
              title: 'Name',
              type: 'string',
            },
            category: {
              title: 'Category',
              type: 'string',
              filter: {
                type: 'list',
                config: {
                  selectText: 'Category...',
                  list: this.categories.map(cate => {
                    return { value: cate.categoryName, title: cate.categoryName }
                  }),
                },
              },
            },
            manufacturer: {
              title: 'Manufacturer',
              type: 'string',
              filter: {
                type: 'list',
                config: {
                  selectText: 'Manufacturer...',
                  list: this.manufacturers.map(mft => {
                    return { value: mft.mftName, title: mft.mftName }
                  }),
                },
              },
            },
            quantitySold: {
              title: 'Sold',
              type: 'number',
              width: '5%'
            },
            totalLikes: {
              title: 'Likes',
              type: 'number',
              width: '5%'
            },
            rating: {
              title: 'Rating',
              type: 'number',
              width: '3%'
            },
            actions: {
              title: 'Actions',
              type: 'custom',
              sort: false,
              filter: {
                type: 'custom',
                component: CustomProductFilterActionsComponent,
              },
              renderComponent: CustomProductActionComponent
            }
          },
          pager: {
            display: true,
            perPage: this.numberOfItem
          },
        };
      },
      (error: any) => {
        console.error("Error:", error);
      }
    );

    this.productService.productChange$
      .pipe(takeUntil(this.unsubscribe))
      .subscribe(() => {
        this.loadProducts();
      });
    this.loadProducts();
  }

  loadProducts() {
    this.productService.findAll().subscribe(
      data => {
        const mappedProducts: any[] = (data as Product[]).map(pro => {
          return {
            productId: pro.productId,
            productName: pro.productName,
            isHide: pro.hide,
            category: pro.category.categoryName,
            manufacturer: pro.manufacturer.mftName,
            image: (!pro.images) ? this.utilsService.getImageFromBase64(pro.images[0].imageUrl) : 'assets/images/default-product.png',
            quantitySold: pro?.quantitySold,
            totalLikes: pro?.totalLikes,
            rating: pro?.rating
          }
        })
        this.source.load(mappedProducts)
      })
  }

  ngAfterViewInit() {
    const pager = document.querySelector('ng2-smart-table-pager');
    pager.classList.add('d-block')
  }

  changeCursor(): void {
    const element = document.getElementById('product-table'); // Replace 'myElement' with the ID of your element
    if (element) {
      element.style.cursor = 'pointer';
    }
  }

  numberOfItemsChange() {
    localStorage.setItem('itemPerPage', this.numberOfItem.toString())
    this.source.setPaging(1, this.numberOfItem)
  }
}
