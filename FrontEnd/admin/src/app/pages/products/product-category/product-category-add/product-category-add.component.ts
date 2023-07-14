import { Component, ViewChild, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { ToastState, UtilsService } from "../../../../@core/services/utils.service";
import { CustomValidator } from "../../../../@core/validators/custom-validator";
import { Category } from "../../../../@core/models/product/category";
import { CategoryService } from "../../../../@core/services/product/category.service";

@Component({
  selector: "ngx-product-category-add",
  templateUrl: "./product-category-add.component.html",
  styleUrls: ["./product-category-add.component.scss"],
})
export class ProductCategoryAddComponent {

  addCategoryFormGroup: FormGroup;
  categories: Category[]

  constructor(
    private categoryService: CategoryService,
    private formBuilder: FormBuilder,
    private utilsService: UtilsService,
  ) {
    this.addCategoryFormGroup = this.formBuilder.group({
      name: ['', [Validators.required, Validators.maxLength(100)]],
      parent: ['']
    })
  }

  createCategory() {
    if (this.addCategoryFormGroup.invalid) {
      this.addCategoryFormGroup.markAllAsTouched();
      this.utilsService.updateToastState(new ToastState('add', 'category', 'danger'))
      return;
    }

    let category: Category = new Category()
    category.categoryName = this.addCategoryFormGroup.get('name').value
    
    this.categoryService.insert(category).subscribe(data => {
      if (data) {
        this.utilsService.updateToastState(new ToastState('add', 'category', 'success'))
        this.addCategoryFormGroup.reset()
        this.categoryService.notifyCategoryChange();
      }
    })
  }
}
