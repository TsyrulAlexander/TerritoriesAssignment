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
    itemClick() {
        this.isSelected = true;
    }
    expandedClick() {
        this.isExpanded = !this.isExpanded;
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