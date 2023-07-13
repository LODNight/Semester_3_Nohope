import { AfterViewInit, Component, OnInit, QueryList, ViewChild, ViewChildren } from '@angular/core';
import { NbAccordionItemComponent } from '@nebular/theme';
import ClassicEditor from '@ckeditor/ckeditor5-build-classic';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ProductService } from '../../../@core/services/product/product.service';
import { Product } from '../../../@core/models/product/product.model';
import { CustomValidator } from '../../../@core/validators/custom-validator';
import { ImagesCarouselComponent } from '../images-carousel.component';
import { ToastState, UtilsService } from '../../../@core/services/utils.service';
import { Router } from '@angular/router';
import { CategoryService } from '../../../@core/services/product/category.service';
import { Category } from '../../../@core/models/product/category';

@Component({
  selector: 'ngx-product-add',
  templateUrl: './product-add.component.html',
  styleUrls: ['./product-add.component.scss']
})
export class ProductAddComponent implements OnInit, AfterViewInit {
  @ViewChild(ImagesCarouselComponent) carousel: ImagesCarouselComponent;
  @ViewChildren(NbAccordionItemComponent) accordions: QueryList<NbAccordionItemComponent>;
  Editor = ClassicEditor;
  editorConfig: any = { placeholder: 'Description' };

  categories: Category[];

  // form chosen values
  addProductFormGroup: FormGroup
  descriptionContent: string;
  images: string[] = []

  constructor(
    private formBuilder: FormBuilder,
    private categoryService: CategoryService,
    private productService: ProductService,
    private utilsService: UtilsService,
    private router: Router
  ) {
  }

  ngOnInit() {
    this.categoryService.findAll().subscribe(data => {this.categories = data})
    this.settingFormGroup()
  }

  ngAfterViewInit(): void {
    this.accordions.first.toggle()
  }

  settingFormGroup(): void {
    this.addProductFormGroup = this.formBuilder.group({
      product: this.formBuilder.group({
        name: ['', [CustomValidator.notBlank, Validators.maxLength(200)]],
        category: [''],
        price: [''],
        quantity: [''],
        detail: [''],
        expireDate: [''],
        manufacturer: [''],
        description: ['', [CustomValidator.notBlank, Validators.maxLength(1000)]],
        images: [this.images]
      }),
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

  get product() { return this.addProductFormGroup.controls["product"] as FormGroup }

  onSubmit() {
    if (this.addProductFormGroup.invalid) {
      this.addProductFormGroup.markAllAsTouched();
      this.utilsService.updateToastState(new ToastState('add', 'product', 'danger'))
      return;
    }

    const insertProduct: Product = this.mapFormValue()
    console.log(insertProduct);
    this.productService.insert(insertProduct).subscribe(data => {

      this.utilsService.updateToastState(new ToastState('add', 'product', 'success'))
      this.router.navigate(['/admin/product/list'])
    })
  }

  mapFormValue(): Product {
    let insertProduct: any = new Product();
    insertProduct.productName = this.product.get('name').value;
    insertProduct.description = this.product.get('description').value;
    insertProduct.isHide = false;
    insertProduct.categoryId = this.categories.find(cate => cate.categoryName = this.product.get('category').value).categoryId;
    insertProduct.images = this.product.get('images').value
    insertProduct.createdAt = new Date();
    insertProduct.updatedAt = new Date();
    return insertProduct;
  }
}

