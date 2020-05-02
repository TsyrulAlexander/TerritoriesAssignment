import { Input, Output, EventEmitter, Injectable, OnInit } from '@angular/core';
import { BaseComponent } from "../base/base.component";
import { BaseLookup } from "../../models/base-lookup";
import {MessageService} from "../../services/message.service";
import {ListItemType} from "../../models/listItemType";
import {ListItemSelected} from "../../models/list-item-selected";
import {ViewListItem} from "../../models/view-list-item";

@Injectable()
export abstract class BaseListItemComponent<T extends BaseLookup> extends BaseComponent implements OnInit {
    get isSelected(): boolean {
        return this.item.isSelected;
    }
    set isSelected(value: boolean) {
        if (this.item.isSelected !== value) {
            this.item.isSelected = value;
        }
    }
    isExpanded: boolean;
    @Output() add = new EventEmitter();
	@Output() expanded = new EventEmitter<boolean>();
    @Output() selected = new EventEmitter<boolean>();
    @Output() update = new EventEmitter<T>();
    @Output() delete = new EventEmitter<T>();
    @Input() item: ViewListItem<T>;
    constructor(private messageService: MessageService) {
        super()
    }
    ngOnInit(): void {
        super.ngOnInit();
        this.messageService.subscribe(this, this.onListItemSelected, "ListItemSelected");
    }
    onListItemSelected(info: ListItemSelected) {
        if (info.itemType !== this.getItemType()) {
            return;
        }
        this.isSelected = info.item.id == this.item.item.id;
    }
    updateClick() {
        this.updateItem();
    }
    deleteClick() {
        this.deleteItem();
    }
    itemClick(event: MouseEvent) {
        if (this.isSelected && event.ctrlKey) {
            this.isSelected = false;
            this.onSelectedChange(event.ctrlKey);
        } else if (!this.isSelected) {
            this.isSelected = true;
            this.onSelectedChange(event.ctrlKey);
        }
    }
    expandedClick() {
        this.isExpanded = !this.isExpanded;
	}
	updateItem(): void {
        this.update.emit(this.item.item);
    }
    deleteItem(): void {
        this.delete.emit(this.item.item);
    }
    abstract getItemType(): ListItemType;
    onSelectedChange(isMultiSelected: boolean = false) {
        let args = new ListItemSelected();
        args.item = this.item.item;
        args.itemType = this.getItemType();
        args.isMultiSelected = isMultiSelected;
        if (this.isSelected) {
            this.messageService.sendMessages(args, "ListItemSelected");
        } else {
            this.messageService.sendMessages(args, "ListItemUnSelected");
        }
        this.selected.emit(this.isSelected);
    }
}