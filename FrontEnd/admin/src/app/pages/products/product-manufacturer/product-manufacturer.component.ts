import { takeUntil } from 'rxjs/operators';
import { Subject} from 'rxjs'
import { Component, ViewChild, OnInit } from "@angular/core";
import { LocalDataSource } from "ng2-smart-table";
import { Router } from "@angular/router";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { CustomValidator } from "../../../@core/validators/custom-validator";
import { ToastState, UtilsService } from "../../../@core/services/utils.service";
import { CategoryService } from '../../../@core/services/product/category.service';
import { CustomManufacturerFilterActionsComponent } from './custom/custom-manufacturer-filter-actions.component';
import { CustomManufacturerActionComponent } from './custom/custom-manufacturer-action.component';
import { ManufacturerService } from '../../../@core/services/product/manufacturer.service';

@Component({
  selector: "ngx-product-manufacturer",
  templateUrl: "./product-manufacturer.component.html",
  styleUrls: ["./product-manufacturer.component.scss"],
})
export class ProductManufacturerComponent implements OnInit {
  state: string = "add"; // default
  private unsubscribe = new Subject<void>();
  
  editCategoryFormGroup: FormGroup;
  // Setting for List layout
  numberOfItem: number = localStorage.getItem('itemPerPage') != null ? +localStorage.getItem('itemPerPage') : 10; // default
  source: LocalDataSource = new LocalDataSource();
  settings = {
    actions: {
      edit: false,
      delete: false,
      add: false,
      columnTitle: ''
    },
    mode: "external", // when add/edit -> navigate to another url
    columns: {
      mftId: {
        title: "ID",
        type: "number",
      },
      mftName: {
        title: "Name",
        type: "string",
      },
      mftAddress: {
        title: "Address",
        type: "string",
      },
      actions: {
        title: 'Actions',
        type: 'custom',
        sort: false,
        filter: {
          type: 'custom',
          component: CustomManufacturerFilterActionsComponent
        },
        renderComponent: CustomManufacturerActionComponent
      }
    },
    pager: {
      display: true,
      perPage: this.numberOfItem
    },
  };

  constructor(
    private manufacturerService: ManufacturerService,
    private utilsService: UtilsService
  ) {
  }
  
  ngOnInit() {
    this.manufacturerService.manufacturerChange$
      .pipe(takeUntil(this.unsubscribe))
      .subscribe(() => {
        this.loadManufacturers();
      });
    this.manufacturerService.state$.subscribe((state) => {
      this.state = state;
    })
    this.loadManufacturers()
  }

  loadManufacturers() {
    this.manufacturerService.findAll().subscribe(
      data => {
          const mappedMfts: any[] = data.map(mft => {
            return {
              mftId: mft.mftId,
              mftName: mft.mftName,
              mftAddress: this.utilsService.parseAddressToStr(mft.mftAddress)
            }
          })
          this.source.load(mappedMfts)
      })
  }

  changeCursor(): void {
    const element = document.getElementById("product-table"); // Replace 'myElement' with the ID of your element
    if (element) {
      element.style.cursor = "pointer";
    }
  }

  numberOfItemsChange() {
    localStorage.setItem('itemPerPage', this.numberOfItem.toString())
    this.source.setPaging(1, this.numberOfItem)
  }
}
