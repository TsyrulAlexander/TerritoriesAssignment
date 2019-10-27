import { Input, Output, EventEmitter, Injectable, OnInit } from '@angular/core';
import { BaseComponent } from "../base/base.component";
import { BaseLookup } from "../../models/base-lookup";
import {MessageService} from "../../services/message.service";
import {ListItemType} from "../../models/listItemType";
import {ListItemSelected} from "../../models/list-item-selected";

@Injectable()
export abstract class BaseListItemComponent<T extends BaseLookup> extends BaseComponent implements OnInit {
    _isSelected: boolean;
    get isSelected(): boolean {
        return this._isSelected;
    }
    set isSelected(value: boolean) {
        if (this._isSelected !== value) {
            this._isSelected = value;
            this.onSelectedChange();
        }
    }
    isExpanded: boolean;
    @Output() add = new EventEmitter();
	@Output() expanded = new EventEmitter<boolean>();
    @Output() selected = new EventEmitter<boolean>();
    @Output() update = new EventEmitter<T>();
    @Output() delete = new EventEmitter<T>();
    @Input() item: T;
    constructor(private messageService: MessageService) {
        super()
    }
    ngOnInit(): void {
        super.ngOnInit();
        this.messageService.subscribe(this, this.onListItemSelected, "ListItemSelected");
    }
    onListItemSelected(info: ListItemSelected) {
        if (info.itemType !== this.getItemType() || info.item.id == this.item.id) {
            return;
        }
        this.isSelected = false;
    }
    updateClick() {
        this.updateItem();
    }
    deleteClick() {
        this.deleteItem();
    }
    itemClick() {
        if (!this.isSelected) {
            this.isSelected = true;
        } else {
            this.onSelectedChange();
        }
    }
    expandedClick() {
        this.isExpanded = !this.isExpanded;
	}
	updateItem(): void {
        this.update.emit(this.item);
    }
    deleteItem(): void {
        this.delete.emit(this.item);
    }
    abstract getItemType(): ListItemType;
    onSelectedChange() {
        let args = new ListItemSelected();
        args.item = this.item;
        args.itemType = this.getItemType();
        if (this.isSelected) {
            this.messageService.sendMessages(args, "ListItemSelected");
        } else {
            this.messageService.sendMessages(args, "ListItemUnSelected");
        }
        this.selected.emit(this.isSelected);
    }
}