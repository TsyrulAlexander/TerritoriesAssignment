import { Component } from '@angular/core';
import {CountryListItem} from "../../models/country-list-item";
import { BaseListItemComponent } from "../base-list-item/base-list-item.component";
import {ListItemType} from "../../models/listItemType";

@Component({
    selector: 'ks-country',
    templateUrl: 'country.component.html',
    styleUrls: ['country.component.css']
})
export class CountryComponent extends BaseListItemComponent<CountryListItem>{
    getItemType(): ListItemType {
        return ListItemType.Country;
    }
}