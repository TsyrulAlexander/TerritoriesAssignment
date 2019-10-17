import { Input, Output, EventEmitter, Injectable } from '@angular/core';
import { BaseComponent } from "../base/base.component";
import { BaseLookup } from "../../models/base-lookup";
import {MessageService} from "../../services/message.service";
import {ListItemType} from "../../models/listItemType";
import {ListItemSelected} from "../../models/list-item-selected";

@Injectable()
export abstract class BaseListItemComponent<T extends BaseLookup> extends BaseComponent {
    isSelected: boolean;
    @Output() add = new EventEmitter();
    @Output() selected = new EventEmitter<boolean>();
    @Input() item: T;
    constructor(private messageService: MessageService) {
        super()
    }
    itemClick() {
        this.isSelected = !this.isSelected;
        this.selected.emit(this.isSelected);
        this.onSelectedChange();
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
    }
}