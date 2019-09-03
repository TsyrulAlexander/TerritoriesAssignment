import { Component, Input } from '@angular/core';
import {Country} from "../../models/country";

@Component({
    selector: 'ks-country',
    templateUrl: 'country.component.html',
    styleUrls: ['country.component.css']
})
export class CountryComponent {
    isShowArea: boolean;
    @Input() country: Country;
}