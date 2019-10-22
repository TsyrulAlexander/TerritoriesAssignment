import { Component} from '@angular/core';
import { AreaListItem } from "../../models/area-list-item";
import {BaseListItemComponent} from "../base-list-item/base-list-item.component";
import {ListItemType} from "../../models/listItemType";

@Component({
	selector: 'ks-area',
	templateUrl: './area.component.html',
    styleUrls: ['./area.component.css'],
})
export class AreaComponent extends BaseListItemComponent<AreaListItem>{
	getItemType(): ListItemType {
		return ListItemType.Area;
	}
}