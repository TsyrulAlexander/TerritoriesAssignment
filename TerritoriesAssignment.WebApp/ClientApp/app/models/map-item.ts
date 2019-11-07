import { BaseLookup } from "./base-lookup";
import { IMapItem } from "./imap-item";
import {Guid} from "guid-typescript";

export class MapItem extends BaseLookup implements IMapItem {
    public color: string;
    constructor(id: Guid = null, name: string = null, public path: string = null) {
        super(id, name);
    }
}