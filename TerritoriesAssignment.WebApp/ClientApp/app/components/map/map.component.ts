import { Component } from '@angular/core';
import { MapItem } from "../../models/map-item";

@Component({
    selector: 'ks-map',
    templateUrl: './map.component.html',
    styleUrls: ['./map.component.css']
})
export class MapComponent {
    mapItems: MapItem[];
    constructor() {

    }
}