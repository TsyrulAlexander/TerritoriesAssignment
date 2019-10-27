import {Injectable, QueryList, ViewChildren} from "@angular/core";
import {BaseComponent} from "../base/base.component";
import {BaseLookup} from "../../models/base-lookup";
import {MatDialog} from "@angular/material";

@Injectable()
export abstract class BaseListComponent<T extends BaseLookup> extends BaseComponent {
	public items: T[];
	constructor(protected dialog: MatDialog) {
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