import { Component, OnInit } from '@angular/core';
import {CountryService} from "../../services/country.service";
import {Country} from "../../models/country";

@Component({
	selector: 'ks-country-list',
	templateUrl: 'country-list.component.html',
	providers: [CountryService]
})
export class CountryListComponent implements OnInit {

	public Countries: Country[];

	constructor(private countryService: CountryService) {}

	ngOnInit() {
		this.countryService.getCountries().subscribe((data: Country[])=> {
			this.Countries = data;
		});
	}
}