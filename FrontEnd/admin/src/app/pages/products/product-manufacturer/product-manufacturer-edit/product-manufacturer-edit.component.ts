import { Component, ViewChild, OnInit } from "@angular/core";
import { LocalDataSource } from "ng2-smart-table";
import { Router } from "@angular/router";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { CategoryService } from "../../../../@core/services/product/category.service";
import { ToastState, UtilsService } from "../../../../@core/services/utils.service";
import { CustomValidator } from "../../../../@core/validators/custom-validator";
import { Category } from "../../../../@core/models/product/category";

@Component({
  selector: "ngx-product-manufacturer-edit",
  templateUrl: "./product-manufacturer-edit.component.html",
  styleUrls: ["./product-manufacturer-edit.component.scss"],
})
export class ProductManufacturerEditComponent implements OnInit {
  
  editCategoryFormGroup: FormGroup;
  constructor(
    private categoryService: CategoryService,
    private formBuilder: FormBuilder,
    private utilsService: UtilsService,
    private router: Router
  ) {
    this.editCategoryFormGroup = this.formBuilder.group({
      id: [],
      name: ['', [CustomValidator.notBlank, Validators.maxLength(100)]],
    })
  }
  
  ngOnInit() {
    this.categoryService.rowData$.subscribe((rowData) => {
      if (rowData) {
        this.editCategoryFormGroup.get('id').setValue(rowData.categoryId);
        this.editCategoryFormGroup.get('name').setValue(rowData.categoryName);
      }
    });
  }
  
  editCategory() {
    if(this.editCategoryFormGroup.invalid) {
      this.editCategoryFormGroup.markAllAsTouched();
      this.utilsService.updateToastState(new ToastState('edit', 'category', 'danger'))
      return;
    }

    let category: Category = new Category()
    category.categoryId = this.editCategoryFormGroup.get('id').value
    category.categoryName = this.editCategoryFormGroup.get('name').value

    this.categoryService.update(category).subscribe(
      data => {
        if(data) {
          this.utilsService.updateToastState(new ToastState('edit', 'category', 'success'))
          this.categoryService.updateHandleAndRowData('add');
          this.categoryService.notifyCategoryChange();
        }
      },
      error => console.log(error)
    )
  }
}
