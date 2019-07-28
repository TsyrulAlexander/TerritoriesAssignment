import { Component, OnInit } from '@angular/core';
import {CountryService} from "../../services/country.service";
import {Country} from "../../models/country";

@Component({
	selector: 'ks-country',
	templateUrl: 'country.component.html',
	providers: [CountryService]
})
export class CountryComponent implements OnInit {

	public Countries: Country[];

	constructor(private countryService: CountryService) {}

	ngOnInit() {
		this.countryService.getCountries().subscribe((data: Country[])=> {
			this.Countries = data;
		});
	}
}