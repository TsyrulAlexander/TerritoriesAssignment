import { Input, Output, EventEmitter, Injectable } from '@angular/core';
import { BaseComponent } from "../base/base.component";
import { BaseLookup } from "../../models/base-lookup";

@Injectable()
export abstract class BaseListItemComponent<T extends BaseLookup> extends BaseComponent {
    isSelected: boolean;
    @Output() add = new EventEmitter();
    @Output() selected = new EventEmitter<boolean>();
    @Input() item: T;
    itemClick() {
        this.isSelected = !this.isSelected;
        this.selected.emit(this.isSelected);
    }
    addClick() {
        this.add.emit();
    }
}