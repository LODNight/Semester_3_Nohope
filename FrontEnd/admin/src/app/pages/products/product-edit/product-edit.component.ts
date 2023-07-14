import { AfterViewInit, Component, OnInit, QueryList, ViewChild, ViewChildren } from '@angular/core';
import { NbAccordionItemComponent } from '@nebular/theme';
import ClassicEditor from '@ckeditor/ckeditor5-build-classic';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ProductService } from '../../../@core/services/product/product.service';
import { Product } from '../../../@core/models/product/product.model';
import { CustomValidator } from '../../../@core/validators/custom-validator';
import { ImagesCarouselComponent } from '../images-carousel.component';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastState, UtilsService } from '../../../@core/services/utils.service';
import { forkJoin } from 'rxjs';
import { CategoryService } from '../../../@core/services/product/category.service';
import { Category } from '../../../@core/models/product/category';
import { Manufacturer } from '../../../@core/models/product/manufacturer';
import { ManufacturerService } from '../../../@core/services/product/manufacturer.service';

@Component({
  selector: 'ngx-product-edit',
  templateUrl: './product-edit.component.html',
  styleUrls: ['./product-edit.component.scss']
})
export class ProductEditComponent implements OnInit {
  @ViewChild(ImagesCarouselComponent) carousel: ImagesCarouselComponent;
  Editor = ClassicEditor;
  editorConfig: any = { placeholder: 'Description' };

  edittingProduct: Product;
  edittingProductId: string;

  categories: Category[];
  manufacturers: Manufacturer[]

  editProductFormGroup: FormGroup
  images: string[] = []

  constructor(
    private formBuilder: FormBuilder,
    private categoryService: CategoryService,
    private productService: ProductService,
    private activatedRoute: ActivatedRoute,
    private utilsService: UtilsService,
    private router: Router,
    private mftService: ManufacturerService,
  ) { }

  get product() { return this.editProductFormGroup.controls["product"] as FormGroup }

  ngOnInit() {
    this.settingFormGroup()
    this.activatedRoute.params.subscribe(
      params => {
        this.edittingProductId = params['id']
        this.productService.findById(+this.edittingProductId).subscribe(
          data => {
            this.edittingProduct = data[0] as Product;
          }
        )
      }
    )

    const category$ = this.categoryService.findAll();
    const mft$ = this.mftService.findAll()
    forkJoin([category$, mft$]).subscribe(
      ([categoryData, mftData]) => {
        this.categories = categoryData;
        this.manufacturers = mftData
        this.fillFormValues();
      },
      error => console.error(error)
    );
  }

  settingFormGroup(): void {
    this.editProductFormGroup = this.formBuilder.group({
      product: this.formBuilder.group({
        id: [],
        name: ['', [Validators.required]],
        category: [''],
        manufacturer: [''],
        price: ['', [Validators.required]],
        quantity: ['', [Validators.required]],
        detail: ['', [Validators.required]],
        description: ['', [Validators.required]],
        expireDate: [''],
        images: [this.images]
      }),
    })
  }

  fillFormValues(): void {
    // setting basic information
    this.product.get('id').setValue(this.edittingProduct.productId);
    this.product.get('name').setValue(this.edittingProduct.productName)
    this.product.get('category').setValue(this.edittingProduct?.category)
    this.product.get('manufacturer').setValue(this.edittingProduct?.manufacturer)
    this.product.get('quantity').setValue(this.edittingProduct.quantity)
    this.product.get('price').setValue(this.edittingProduct.price)
    this.product.get('description').setValue(this.edittingProduct.description)
    this.product.get('detail').setValue(this.edittingProduct.detail)
    this.product.get('images').setValue(this.edittingProduct.images)
  }

  selectFiles(event: any) {
    if (event.target.files) {
      for (let i = 0; i < event.target.files.length; i++) {
        const reader = new FileReader();
        reader.onload = (event: any) => {
          this.images.push(event.target.result);
        };

        reader.readAsDataURL(event.target.files[i]);
      }
    }
    this.carousel.show(this.images);
  }

  onSubmit() {
    if (this.editProductFormGroup.invalid) {
      this.editProductFormGroup.markAllAsTouched();
      this.utilsService.updateToastState(new ToastState('edit', 'product', 'danger'))
      return;
    }

    const editedProduct: Product = this.mapFormValue()
    console.log(editedProduct)

    this.productService.update(editedProduct).subscribe(data => {
      if (data) {
        this.utilsService.updateToastState(new ToastState('edit', 'product', 'success'))
        this.router.navigate(['/admin/product/list'])
      } else {
        this.utilsService.updateToastState(new ToastState('edit', 'product', 'danger'))
      }
    })
  }

  mapFormValue(): Product {
    let editedProduct: any = new Product();
    editedProduct.productId = this.product.get('id').value;
    editedProduct.productName = this.product.get('name').value;
    editedProduct.categoryId = this.product.get('category').value?.categoryId;
    editedProduct.manufacturerId = this.product.get('manufacturer').value?.mftId;
    editedProduct.quantity = this.product.get('quantity').value
    editedProduct.price = this.product.get('price').value
    editedProduct.description = this.product.get('description').value;
    editedProduct.detail = this.product.get('detail').value;
    editedProduct.images = this.product.get('images').value
    return editedProduct
  }

}
