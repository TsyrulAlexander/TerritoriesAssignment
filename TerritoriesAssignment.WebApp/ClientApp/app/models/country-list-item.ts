import {BaseLookup} from "./base-lookup";
import {Guid} from "guid-typescript";

export class CountryListItem extends BaseLookup {
    constructor(id: Guid = null, name: string = null) {
        super(id, name);
    }
}