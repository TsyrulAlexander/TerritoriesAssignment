import {MapItem} from "./map-item";
import {AreaListItem} from "./area-list-item";
import {CountryListItem} from "./country-list-item";

export class Area extends MapItem implements AreaListItem {
	public country: CountryListItem;
}