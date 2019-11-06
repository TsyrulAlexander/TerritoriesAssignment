import {Component, EventEmitter, Output, ViewChild} from '@angular/core';
import {CountryListItem} from "../../models/country-list-item";
import { BaseListItemComponent } from "../base-list-item/base-list-item.component";
import {ListItemType} from "../../models/listItemType";
import {AreaListComponent} from "../area-list/area-list.component";
import {MatDialog} from "@angular/material";
import {MessageService} from "../../services/message.service";
import {ManagerDistributionComponent} from "../manager-distribution/manager-distribution.component";
import {AreaListItem} from "../../models/area-list-item";
import {ObjectUtilities} from "../../utilities/object-utilities";

@Component({
    selector: 'ks-country',
    templateUrl: 'country.component.html',
    styleUrls: ['country.component.css']
})
export class CountryComponent extends BaseListItemComponent<CountryListItem>{
    @Output() onManagersDistribution = new EventEmitter<AreaListItem[]>();
    @ViewChild('areaList', { static: true })
    areaList: AreaListComponent;
    constructor(protected dialog: MatDialog, messageService: MessageService) {
        super(messageService)
    }
    getItemType(): ListItemType {
        return ListItemType.Country;
    }
    itemClick(event: MouseEvent) {
        super.itemClick(event);
        this.areaList.loadItems();
    }
    managersDistribution() {
        let areas = ObjectUtilities.where(this.areaList.items, {isSelected: true});
       this.onManagersDistribution.emit(areas.map(value => value.item));
    }
}