import { Component, ViewChild, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { ToastState, UtilsService } from "../../../../@core/services/utils.service";
import { CustomValidator } from "../../../../@core/validators/custom-validator";
import { Category } from "../../../../@core/models/product/category";
import { CategoryService } from "../../../../@core/services/product/category.service";
import { Province } from "../../../../@core/models/address/provinces.model";
import { District } from "../../../../@core/models/address/districts.model";
import { Ward } from "../../../../@core/models/address/wards.model";
import { AddressService } from "../../../../@core/services/account/address.service";

@Component({
  selector: "ngx-product-manufacturer-add",
  templateUrl: "./product-manufacturer-add.component.html",
  styleUrls: ["./product-manufacturer-add.component.scss"],
})
export class ProductManufacturerAddComponent {

  addMftFormGroup: FormGroup;
  provinces: Province[]
  districts: District[]
  wards: Ward[]

  constructor(
    private categoryService: CategoryService,
    private formBuilder: FormBuilder,
    private utilsService: UtilsService,
    private addressService: AddressService
  ) {
    this.addMftFormGroup = this.formBuilder.group({
      name: ['', [Validators.required, Validators.maxLength(100)]],
      province: [, [Validators.required]],
      district: [, [Validators.required]],
      ward: [, [Validators.required]],
      address: [, [Validators.required], Validators.maxLength(50)],
    })
  }

  createCategory() {
    if (this.addMftFormGroup.invalid) {
      this.addMftFormGroup.markAllAsTouched();
      this.utilsService.updateToastState(new ToastState('add', 'category', 'danger'))
      return;
    }

    let category: Category = new Category()
    category.categoryName = this.addMftFormGroup.get('name').value
    
    this.categoryService.insert(category).subscribe(data => {
      if (data) {
        this.utilsService.updateToastState(new ToastState('add', 'category', 'success'))
        this.addMftFormGroup.reset()
        this.categoryService.notifyCategoryChange();
      }
    })
  }

  loadProvinces() {
    this.addressService.findAllProvinces().subscribe(
      data => {
        this.provinces = data.provinces
        this.addMftFormGroup.get('district').setValue({})
        this.addMftFormGroup.get('ward').setValue({})
      }
    )
  }

  loadDistricts(event: any) {
    const selectedProvince: Province = this.addMftFormGroup.get('province').value
    this.addressService.findAllDistrictByProvince(selectedProvince.code).subscribe(
      data => {
        this.districts = data.districts
      }
    );
  }

  loadWards(event: any) {
    const selectedDistrict: District = this.addMftFormGroup.get('district').value
    this.addressService.findAllWardByDistrict(selectedDistrict.code).subscribe(
      data => {
        this.wards = data.wards
        this.addMftFormGroup.get('ward').setValue({})
      }
    );
  }
}
