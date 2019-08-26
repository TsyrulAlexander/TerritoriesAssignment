import { Component, Input } from '@angular/core';
import {Country} from "../../models/country";

@Component({
    selector: 'ks-country-item',
    templateUrl: 'country-item.component.html'
})
export class CountryItemComponent {
	@Input() country: Country;
	constructor() {

	}

}