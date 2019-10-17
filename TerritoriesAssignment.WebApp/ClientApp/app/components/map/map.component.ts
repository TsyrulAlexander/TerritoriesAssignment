import {Component, OnInit} from '@angular/core';
import {MapItem} from "../../models/map-item";
import {MessageService} from "../../services/message.service";
import {BaseComponent} from "../base/base.component";
import {CountryService} from "../../services/country.service";
import {ObjectUtilities} from "../../utilities/object-utilities";
import {ListItemSelected} from "../../models/list-item-selected";
import {ListItemType} from "../../models/listItemType";
import {AreaService} from "../../services/area.service";

@Component({
    selector: 'ks-map',
    templateUrl: './map.component.html',
    styleUrls: ['./map.component.css'],
    providers: [CountryService, AreaService]
})
export class MapComponent extends BaseComponent implements OnInit {
    text: string;
    mapItems: MapItem[] = [];
    constructor(private messageService: MessageService, private countryService: CountryService, private areaService: AreaService) {
        super();
    }
    subscribeMessages() {
        super.subscribeMessages();
        this.messageService.subscribe(this, this.onListItemSelected, "ListItemSelected");
        this.messageService.subscribe(this, this.onListItemUnSelected, "ListItemUnSelected");
    }
    onListItemUnSelected (body: ListItemSelected) {
        let item = body.item;
        let mapItem = ObjectUtilities.findWhere(this.mapItems, {id: item.id});
        ObjectUtilities.delete(this.mapItems, mapItem);
    }
    onListItemSelected(body: ListItemSelected) {
        let item = body.item;
        let itemType = body.itemType;
        if (itemType == ListItemType.Country) {
            this.countryService.getCountry(item.id).subscribe(country => {
                this.mapItems.push(country);
            })
        }
        if (itemType == ListItemType.Area) {
            this.areaService.getArea(item.id).subscribe(area => {
                this.mapItems.push(area);
            })
        }
    }
}