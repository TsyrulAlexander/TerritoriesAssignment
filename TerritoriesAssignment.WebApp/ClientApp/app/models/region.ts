import {MapItem} from "./map-item";
import {RegionListItem} from "./region-list-item";
import {BaseLookup} from "./base-lookup";

export class Region extends MapItem implements RegionListItem {
	public area: BaseLookup;
}