import { BaseLookup } from "./base-lookup";
import { MapPoint } from "./map-point";

export class MapItem implements  BaseLookup {
    public name: string;
    public id: string;
    public points: MapPoint[];
}