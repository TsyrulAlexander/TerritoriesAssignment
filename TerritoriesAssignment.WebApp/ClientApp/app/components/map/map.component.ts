import {Component, ElementRef, Input, ViewChild} from '@angular/core';
import {MapItem} from "../../models/map-item";
import {MessageService} from "../../services/message.service";
import {BaseComponent} from "../base/base.component";
import {ObjectUtilities} from "../../utilities/object-utilities";
import {ListItemSelected} from "../../models/list-item-selected";
import {ListItemType} from "../../models/listItemType";
import {MapService} from "../../services/map.service";
import {ViewListItem} from "../../models/view-list-item";

@Component({
    selector: 'ks-map',
    templateUrl: './map.component.html',
    styleUrls: ['./map.component.css'],
    providers: [MapService]
})
export class MapComponent extends BaseComponent{
    height: 100;
    width: 100;
    @Input()mapItems: ViewListItem<MapItem>[] = [];
    defaultColorName: string = "blue";
    selectColorName: string = "red";
    @ViewChild('mySvg', {static: true}) mySvg: ElementRef;

    constructor(private messageService: MessageService, private mapService: MapService) {
        super();
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
            let mapItem = ObjectUtilities.findItemFromPath<ViewListItem<MapItem>>(this.mapItems, "item.id.value", item.id.toString());
            if (mapItem) {
                mapItem.isSelected = false;
            }
        }
    }
    selectMap(mouseEvent: MouseEvent, mapItem: ViewListItem<MapItem>) {
        let args = new ListItemSelected();
        args.item = mapItem.item;
        args.itemType = ListItemType.Area;
        args.isMultiSelected = mouseEvent.ctrlKey;
        this.messageService.sendMessages(args, "ListItemSelected");
    }
    onListItemSelected(body: ListItemSelected) {
        let item = body.item;
        let itemType = body.itemType;
        if (itemType === ListItemType.Country) {
            this.mapItems = [];
            this.mapService.getAreasMap(item.id).subscribe(mapItems => {
                mapItems.forEach(mapItem => {
                    this.mapItems.push(new ViewListItem<MapItem>(mapItem));
                });
            });
        }
        if (itemType === ListItemType.Area) {
            let selectedItem = ObjectUtilities.findItemFromPath<ViewListItem<MapItem>>(this.mapItems, "item.id.value", item.id.toString());
            if (selectedItem) {
                selectedItem.isSelected = true;
            }
        }
    }
    resize() {
        let maxX = 0;
        let maxY = 0;
        let paths = this.mySvg.nativeElement.getElementsByTagName('path');
        paths.forEach((path: any) => {
            for (let i = 0; i <= path.getTotalLength(); i++ ) {
                let point = path.getPointAtLength(i);
                if (point.x > maxX) {
                    maxX = point.x;
                }
                if (point.y > maxY) {
                    maxY = point.y;
                }
            }
        }, this);
        this.mySvg.nativeElement.setAttribute("height", maxY + "px");
        this.mySvg.nativeElement.setAttribute("width", maxX + "px");
    }
}