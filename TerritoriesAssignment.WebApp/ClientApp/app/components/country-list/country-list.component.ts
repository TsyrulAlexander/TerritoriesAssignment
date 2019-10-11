import { Component, OnInit } from '@angular/core';
import {CountryService} from "../../services/country.service";
import {Country} from "../../models/country";
import {MessageService} from "../../services/message.service";

@Component({
	selector: "ks-country-list",
	templateUrl: "country-list.component.html",
	styleUrls: ["country-list.component.css"],
	providers: [CountryService]
})
export class CountryListComponent implements OnInit {

	public Countries: Country[];

	constructor(private countryService: CountryService, private messageService: MessageService) {}
	countryCreated(country: Country) {
		console.log(country);
	}
	countrySelected(country: Country, isSelected: boolean) {
		this.messageService.sendMessage(country, "SelectCountry");
	}
	addCountry() {

	}
	ngOnInit() {
		this.countryService.getCountries().subscribe((data: Country[])=> {
			this.Countries = data;
		});
	}
}