import {BaseLookup} from "./base-lookup";
import {ListItemType} from "./listItemType";

export class ListItemSelected {
	public item: BaseLookup;
	public itemType: ListItemType;
	public isMultiSelected: boolean = false;
	public isSilend: boolean = false;
}