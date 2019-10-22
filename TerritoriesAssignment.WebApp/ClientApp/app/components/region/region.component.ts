import { Component, Input } from '@angular/core';
import {RegionListItem} from "../../models/region-list-item";
import {BaseListItemComponent} from "../base-list-item/base-list-item.component";
import {ListItemType} from "../../models/listItemType";

@Component({
	selector: 'ks-region',
	templateUrl: './region.component.html',
	styleUrls: ['./region.component.css']
})

export class RegionComponent extends BaseListItemComponent<RegionListItem>{
	getItemType(): ListItemType {
		return ListItemType.Region;
	}
}