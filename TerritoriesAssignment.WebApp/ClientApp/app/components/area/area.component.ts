import {Component, ViewChild} from '@angular/core';
import { AreaListItem } from "../../models/area-list-item";
import {BaseListItemComponent} from "../base-list-item/base-list-item.component";
import {ListItemType} from "../../models/listItemType";
import {RegionListComponent} from "../region-list/region-list.component";

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
	itemClick() {
		super.itemClick();
		this.regionList.loadItems();
	}
}