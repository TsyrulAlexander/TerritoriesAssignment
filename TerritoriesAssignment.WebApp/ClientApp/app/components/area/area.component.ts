import { Component} from '@angular/core';
import { Area } from "../../models/area";
import {BaseListItemComponent} from "../base-list-item/base-list-item.component";

@Component({
	selector: 'ks-area',
	templateUrl: './area.component.html',
    styleUrls: ['./area.component.css'],
})
export class AreaComponent extends BaseListItemComponent<Area>{
}