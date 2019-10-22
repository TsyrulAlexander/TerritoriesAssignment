import {BaseLookup} from "./base-lookup";
import {Guid} from "guid-typescript";

export class AreaListItem extends BaseLookup {
	constructor(id: Guid, name: string) {
		super(id, name);
	}
}