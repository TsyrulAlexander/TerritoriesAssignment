import {AfterViewInit, Component, OnInit} from '@angular/core';
import {MapItem} from "../../models/map-item";
import {MessageService} from "../../services/message.service";
import {BaseComponent} from "../base/base.component";
import {ObjectUtilities} from "../../utilities/object-utilities";
import {ListItemSelected} from "../../models/list-item-selected";
import {ListItemType} from "../../models/listItemType";
import {MapService} from "../../services/map.service";

@Component({
    selector: 'ks-map',
    templateUrl: './map.component.html',
    styleUrls: ['./map.component.css'],
    providers: [MapService]
})
export class MapComponent extends BaseComponent implements OnInit, AfterViewInit {
    mapItems: MapItem[] = [];
    selectItem: MapItem = null;
    defaultColorName: string = "blue";
    selectColorName: string = "red";
    constructor(private messageService: MessageService, private mapService: MapService) {
        super();
    }
    ngAfterViewInit(): void {
    }
    draw(): void {

    }
    clear() {

    }
    drawMapItem(item: MapItem) {

    }
    subscribeMessages() {
        super.subscribeMessages();
        this.messageService.subscribe(this, this.onListItemSelected, "ListItemSelected");
        this.messageService.subscribe(this, this.onListItemUnSelected, "ListItemUnSelected");
    }
    onListItemUnSelected (body: ListItemSelected) {
        let item = body.item;
        if (body.itemType === ListItemType.Country) {
            this.mapItems = [];
        }
        if (body.itemType === ListItemType.Area) {
            let mapItem = ObjectUtilities.findItem(this.mapItems, {id: item.id});
            if (this.selectItem && this.selectItem.id === mapItem.id) {
                this.selectItem = null;
            }
        }
        this.draw();
    }
    onListItemSelected(body: ListItemSelected) {
        let item = body.item;
        let itemType = body.itemType;
        if (itemType === ListItemType.Country) {
            this.mapService.getAreasMap(item.id).subscribe(mapItems => {
                mapItems.forEach(mapItem => {
                    this.mapItems.push(mapItem);
                });
                this.draw();
            });
        }
        if (itemType === ListItemType.Area) {
            this.selectItem = ObjectUtilities.findItem<MapItem>(this.mapItems, {id: item.id});
        }
    }
}