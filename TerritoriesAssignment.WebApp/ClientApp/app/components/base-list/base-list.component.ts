import {Injectable} from "@angular/core";
import {BaseComponent} from "../base/base.component";

@Injectable()
export abstract class BaseListComponent<T> extends BaseComponent {
	public items: T[];
	abstract createItem(): void;
}