import {BaseLookup} from "./base-lookup";
import {Guid} from "guid-typescript";

export class RegionListItem extends BaseLookup {
	constructor(id: Guid, name: string) {
		super(id, name)
	}
}