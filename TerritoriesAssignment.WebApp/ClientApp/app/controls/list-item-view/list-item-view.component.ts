import {BaseLookup} from "../../models/base-lookup";
import {Component, EventEmitter, Input, Output} from "@angular/core";
import {BaseComponent} from "../../components/base/base.component";

@Component({
	selector: "ks-list-item-view",
	templateUrl: "./list-item-view.component.html"
})
export class ListItemViewComponent extends BaseComponent {
	@Input() item: BaseLookup;
	@Input() isUseExpanded: boolean;
	@Output() isSelected: boolean;
	@Output() isExpanded: boolean;
	@Output() onItemClick = new EventEmitter;
	@Output() onExpandedClick = new EventEmitter;
	expandedClick() {
		this.isExpanded = !this.isExpanded;
		this.onExpandedClick.emit();
	}
	itemClick() {
		this.isSelected = !this.isSelected;
		this.onItemClick.emit();
	}
}