import {BaseObject} from "./base-object";
import {MapItem} from "./map-item";

export class ManagerInfoResponse extends BaseObject {
	public areas: MapItem[];
}