import {Component, EventEmitter, Input, Output} from '@angular/core';
import {Country} from "../../models/country";

@Component({
    selector: 'ks-add-country',
    templateUrl: './add-country.component.html',
    styleUrls: ['./add-country.component.css']
})
export class AddCountryComponent {
    country: Country;
    @Output() countryCreated = new EventEmitter<Country>();
}