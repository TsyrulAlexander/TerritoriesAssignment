import {Injectable} from "@angular/core";
import {BaseComponent} from "../base/base.component";
import {BaseLookup} from "../../models/base-lookup";
import {MatDialog} from "@angular/material";
import {ViewListItem} from "../../models/view-list-item";

@Injectable()
export abstract class BaseListComponent<T extends BaseLookup> extends BaseComponent {
	public items: ViewListItem<T>[];
	protected constructor(protected dialog: MatDialog) {
		super();
	}
	openModal<T>(type: {new (): T}, data: any = null): Promise<any> {
		let dialog = this.dialog.open(type,{
			data: data
		});
		return new Promise(resolve => {
			dialog.afterClosed().subscribe(response => {
				resolve(response);
			});
		});
	}
	abstract createItem(): void;
	abstract loadItems(): void;
}