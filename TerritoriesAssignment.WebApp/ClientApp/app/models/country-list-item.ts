import {BaseLookup} from "./base-lookup";
import {Guid} from "guid-typescript";

export class CountryListItem extends BaseLookup {
    constructor(id: Guid, name: string = null) {
        super(id, name);
    }
}