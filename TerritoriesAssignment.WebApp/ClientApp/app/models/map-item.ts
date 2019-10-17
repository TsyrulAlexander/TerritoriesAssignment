import { BaseLookup } from "./base-lookup";
import { MapPoint } from "./map-point";
import { IMapItem } from "./imap-item";
import {Guid} from "guid-typescript";

export class MapItem extends BaseLookup implements IMapItem {
    constructor(id: Guid, name: string = null, public points: MapPoint[] = null) {
        super(id, name);
    }
}