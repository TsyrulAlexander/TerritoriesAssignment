import {BaseObject} from "./base-object";
import {BaseLookup} from "./base-lookup";
import {Guid} from "guid-typescript";

export class AttributeValue extends BaseObject {
	public doubleValue: number;
	public region: BaseLookup;
	public attribute: BaseLookup;
	constructor(public id: Guid = null) {
		super(id);
	}
}