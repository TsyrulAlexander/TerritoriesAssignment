import { BaseLookup } from "./base-lookup";
import { MapPoint } from "./map-point";
import { IMapItem } from "./imap-item";

export class MapItem implements BaseLookup, IMapItem {
    public points: MapPoint[];
    public name: string;
    public id: string;
}