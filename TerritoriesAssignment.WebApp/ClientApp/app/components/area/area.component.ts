import {Component, ViewChild} from '@angular/core';
import { AreaListItem } from "../../models/area-list-item";
import {BaseListItemComponent} from "../base-list-item/base-list-item.component";
import {ListItemType} from "../../models/listItemType";
import {RegionListComponent} from "../region-list/region-list.component";
import {ListItemSelected} from "../../models/list-item-selected";

@Component({
	selector: 'ks-area',
	templateUrl: './area.component.html',
    styleUrls: ['./area.component.css'],
})
export class AreaComponent extends BaseListItemComponent<AreaListItem>{
	@ViewChild('regionList', { static: true })
	regionList: RegionListComponent;
	getItemType(): ListItemType {
		return ListItemType.Area;
	}
	onListItemSelected(info: ListItemSelected) {
		if (info.itemType !== this.getItemType()) {
			return;
		}
		let isCurrentItem = info.item.id == this.item.item.id;
		if (isCurrentItem && this.isSelected) {
			return;
		} else if (isCurrentItem) {
			this.isSelected = true;
			this.onSelectedChange(info.isMultiSelected);
		} else if (!isCurrentItem && this.isSelected && !info.isMultiSelected) {
			this.isSelected = false;
			this.onSelectedChange(info.isMultiSelected);
		}
	}
	itemClick(event: MouseEvent) {
		super.itemClick(event);
		this.regionList.loadItems();
	}
}