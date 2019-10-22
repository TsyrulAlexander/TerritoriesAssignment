import {Component, OnInit} from "@angular/core";
import {BaseComponent} from "../base/base.component";
import {MessageService} from "../../services/message.service";
import {ListItemSelected} from "../../models/list-item-selected";
import {BaseLookup} from "../../models/base-lookup";
import {ListItemType} from "../../models/listItemType";

@Component({
	selector: "ks-item-info",
	templateUrl: "./item-info.component.html"
})
export class ItemInfoComponent extends BaseComponent implements OnInit {
	item: BaseLookup;
	itemType: ListItemType;
	constructor(private messageService: MessageService) {
		super();
	}
	subscribeMessages() {
		super.subscribeMessages();
		this.messageService.subscribe(this, this.onListItemSelected, "ListItemSelected");
	}
	onListItemSelected(info: ListItemSelected) {
		this.item = info.item;
		this.itemType = info.itemType;
	}
}