import {Component, OnInit, QueryList, ViewChildren} from '@angular/core';
import {CountryService} from "../../services/country.service";
import {CountryListItem} from "../../models/country-list-item";
import {BaseListComponent} from "../base-list/base-list.component";
import {MatDialog} from "@angular/material";
import {AddCountryComponent} from "../add-country/add-country.component";
import {Country} from "../../models/country";
import {CountryComponent} from "../country/country.component";

@Component({
	selector: "ks-country-list",
	templateUrl: "country-list.component.html",
	styleUrls: ["country-list.component.css"],
	providers: [CountryService]
})
export class CountryListComponent extends BaseListComponent<CountryListItem> implements OnInit {
	constructor(private countryService: CountryService, private dialog: MatDialog) {
		super();
	}
	ngOnInit() {
		this.loadItems();
	}

	createItem() {
		let dialog = this.dialog.open(AddCountryComponent);
		dialog.afterClosed().subscribe(country => {
			if (country == null) {
				return;
			}
			this.onCountryCreated(country);
		});
	}
	onCountryCreated(country: Country) {
		this.countryService.addCountry(country).subscribe(value => {
			debugger;
			this.items.push(country);
		})
	}

	loadItems(): void {
		this.countryService.getCountries().subscribe((data: CountryListItem[])=> {
			this.items = data;
		});
	}
}