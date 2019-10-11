import { Component, OnInit } from '@angular/core';
import { MapItem } from "../../models/map-item";
import {MessageService} from "../../services/message.service";

@Component({
    selector: 'ks-map',
    templateUrl: './map.component.html',
    styleUrls: ['./map.component.css']
})
export class MapComponent implements OnInit{
    text: string;
    mapItems: MapItem[];
    constructor(private messageService: MessageService) {
    }

    ngOnInit(): void {
        this.messageService.subscribe(this, value => {
            this.text = value.name;
        }, "SelectCountry");
    }
}