import { Component } from '@angular/core';
import {Country} from "../../models/country";
import { BaseListItemComponent } from "../base-list-item/base-list-item.component";

@Component({
    selector: 'ks-country',
    templateUrl: 'country.component.html',
    styleUrls: ['country.component.css']
})
export class CountryComponent extends BaseListItemComponent<Country>{
    
}