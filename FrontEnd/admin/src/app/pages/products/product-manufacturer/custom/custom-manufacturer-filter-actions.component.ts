import { Component, OnChanges, OnInit, SimpleChanges, Output, EventEmitter } from '@angular/core';
import { DefaultFilter } from 'ng2-smart-table';
import { CategoryService } from '../../../../@core/services/product/category.service';

@Component({
    template: `
        <button nbButton fullWidth="" status="primary" 
            (click)="onAdd()">
            <nb-icon icon="plus-square-outline"></nb-icon>
        </button>
    `,
})
export class CustomManufacturerFilterActionsComponent extends DefaultFilter implements OnInit, OnChanges {

    constructor(private categoryService: CategoryService) {
        super()
    }

    onAdd() {
        this.categoryService.updateHandleAndRowData('add');
    }

    ngOnInit() {let x}

    ngOnChanges(changes: SimpleChanges) {let x}
}