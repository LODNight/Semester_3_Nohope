import { Component, OnInit, AfterViewInit, ViewChild, ElementRef } from '@angular/core';
import { LocalDataSource } from 'ng2-smart-table';
import { Router } from '@angular/router';
import { CustomProductActionComponent } from './custom/custom-product-action.component';
import { CustomProductFilterActionsComponent } from './custom/custom-product-filter-actions.component';
import { ProductService } from '../../../@core/services/product/product.service';
import { ProductCategoryService } from '../../../@core/services/product/product-category.service';
import { ProductShapeService } from '../../../@core/services/product/product-shape.service';
import { ProductStyleService } from '../../../@core/services/product/product-style.service';
import { ProductColorService } from '../../../@core/services/product/product-color.service';
import { ProductShape } from '../../../@core/models/product/product-shape.model';
import { ProductColor } from '../../../@core/models/product/product-color.model';
import { ProductStyle } from '../../../@core/models/product/product-style.model';
import { ProductCategory } from '../../../@core/models/product/product-category.model';
import { CustomCategoryImageComponent } from '../product-category/custom/custom-category-image.component';
import { NbGlobalLogicalPosition, NbGlobalPhysicalPosition, NbGlobalPosition, NbToastrService } from '@nebular/theme';

@Component({
  selector: 'ngx-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.scss']
})
export class ProductListComponent implements OnInit, AfterViewInit {
  numberOfItem: number = localStorage.getItem('itemPerPage') != null ? +localStorage.getItem('itemPerPage') : 10; // default
  source: LocalDataSource = new LocalDataSource();
  // Setting for List layout
  shapes: ProductShape[];
  colors: ProductColor[];
  styles: ProductStyle[];
  categories: ProductCategory[];

  settings = {};


  constructor(
    private productService: ProductService,
    private router: Router,
    private categoryService: ProductCategoryService,
    private shapeService: ProductShapeService,
    private styleService: ProductStyleService,
    private colorService: ProductColorService,
    private toastService: NbToastrService
  ) {
    this.categoryService.findAll().subscribe(data => this.categories = data)
    this.shapeService.findAll().subscribe(data => this.shapes = data)
    this.colorService.findAll().subscribe(data => this.colors = data)
    this.styleService.findAll().subscribe(data => this.styles = data)
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
          renderComponent: CustomCategoryImageComponent
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
              list: [
                { value: '1', title: 'Category 1' },
                { value: '2', title: 'Category 2' },
                
              ],
            },
          },
        },
        quantity: {
          title: 'Quantity',
          type: 'int',
          sort: false,
        },
        manufacturerId: {
          title: 'Manufacturies',
          type: 'int',
          sort: false,
        },
        totalRating: {
          title: 'Total Rating',
          type: 'int',
          sort: false,
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
    }
    this.productService.findAll().subscribe(
      data => {
        const mappedProducts: any[] = data.map(pro => {
          return {
            productId: pro.productId,
            productName: pro.productName,
            isHide: pro.isHide,
            category: pro.categoryName,
            quantity: pro.quantity,
            image: pro.imageUrls[0],
            quantitySold: pro.quantitySold,
          }
        })
        this.source.load(mappedProducts)
      }

    )
  }

  ngOnInit(): void {
    let x;
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
