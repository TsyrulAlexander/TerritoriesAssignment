import {Injectable, QueryList, ViewChildren} from "@angular/core";
import {BaseComponent} from "../base/base.component";
import {BaseLookup} from "../../models/base-lookup";
import {BaseListItemComponent} from "../base-list-item/base-list-item.component";
import {AreaComponent} from "../area/area.component";

@Injectable()
export abstract class BaseListComponent<T extends BaseLookup> extends BaseComponent {
	public items: T[];

	abstract createItem(): void;
	abstract loadItems(): void;
}