import {Component, ViewChild} from '@angular/core';
import {CountryListItem} from "../../models/country-list-item";
import { BaseListItemComponent } from "../base-list-item/base-list-item.component";
import {ListItemType} from "../../models/listItemType";
import {AreaListComponent} from "../area-list/area-list.component";

@Component({
    selector: 'ks-country',
    templateUrl: 'country.component.html',
    styleUrls: ['country.component.css']
})
export class CountryComponent extends BaseListItemComponent<CountryListItem>{
    @ViewChild('areaList', { static: true })
    areaList: AreaListComponent;
    getItemType(): ListItemType {
        return ListItemType.Country;
    }
    itemClick() {
        super.itemClick();
        this.areaList.loadItems();
    }
}