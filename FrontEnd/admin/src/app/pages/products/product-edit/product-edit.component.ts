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

@Component({
  selector: 'ngx-product-edit',
  templateUrl: './product-edit.component.html',
  styleUrls: ['./product-edit.component.scss']
})
export class ProductEditComponent implements OnInit {
  @ViewChild(ImagesCarouselComponent) carousel: ImagesCarouselComponent;
  @ViewChildren(NbAccordionItemComponent) accordions: QueryList<NbAccordionItemComponent>;
  Editor = ClassicEditor;
  editorConfig: any = { placeholder: 'Description' };

  edittingProduct: Product;
  edittingProductId: string;
  categories: Category[];

  editProductFormGroup: FormGroup
  images: string[] = []

  constructor(
    private formBuilder: FormBuilder,
    private categoryService: CategoryService,
    private productService: ProductService,
    private activatedRoute: ActivatedRoute,
    private utilsService: UtilsService,
    private router: Router
  ) {
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
  }

  get product() { return this.editProductFormGroup.controls["product"] as FormGroup }
  get variants() { return this.editProductFormGroup.controls["variants"] as FormArray }

  ngOnInit() {
    const category$ = this.categoryService.findAll();

    forkJoin([category$]).subscribe(
      ([categoryData]) => {
        this.categories = categoryData;
        this.fillFormValues();
      },
      error => {
        console.error(error);
      }
    );
  }

  fillFormValues(): void {
    // setting basic information
    this.product.get('id').setValue(this.edittingProduct.productId);
    this.product.get('name').setValue(this.edittingProduct.productName)
    this.product.get('category').setValue(this.edittingProduct.category.categoryName)
    this.product.get('description').setValue(this.edittingProduct.description)
    this.product.get('images').setValue(this.edittingProduct.images)

    // let images: string[] = this.edittingProduct.images.map((img: Image ) => {
    //   return this.utilsService.getImageFromBase64(img.imageUrl);
    // })
    // this.carousel.show(images);
  }

  settingFormGroup(): void {
    this.editProductFormGroup = this.formBuilder.group({
      product: this.formBuilder.group({
        id: [],
        name: ['', [CustomValidator.notBlank, Validators.maxLength(200)]],
        category: [''],
        description: ['', [CustomValidator.notBlank, Validators.maxLength(1000)]],
        images: [this.images] // Initialize with the array of URLs, e.g., this.urls is the array obtained from selectFile method
      }),
      variants: this.formBuilder.array([])
    })
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
    editedProduct.description = this.product.get('description').value;
    editedProduct.isHide = false;
    editedProduct.categoryId = this.categories.find(cate => cate.categoryName == this.product.get('category').value).categoryId;
    editedProduct.images = this.product.get('images').value
    editedProduct.createdAt = new Date();
    editedProduct.updatedAt = new Date();
    return editedProduct
  }

}
