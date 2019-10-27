import {Component, OnInit, QueryList, ViewChildren} from '@angular/core';
import {CountryService} from "../../services/country.service";
import {CountryListItem} from "../../models/country-list-item";
import {BaseListComponent} from "../base-list/base-list.component";
import {MatDialog} from "@angular/material";
import {AddCountryComponent} from "../add-country/add-country.component";
import {Country} from "../../models/country";
import {CountryComponent} from "../country/country.component";
import {ObjectUtilities} from "../../utilities/object-utilities";

@Component({
	selector: "ks-country-list",
	templateUrl: "country-list.component.html",
	styleUrls: ["country-list.component.css"],
	providers: [CountryService]
})
export class CountryListComponent extends BaseListComponent<CountryListItem> implements OnInit {
	constructor(private countryService: CountryService, dialog: MatDialog) {
		super(dialog);
	}

	ngOnInit() {
		this.loadItems();
	}

	createItem() {
		this.openModal(AddCountryComponent).then(value => {
			if (value == null) {
				return;
			}
			this.onCountryCreated(value);
		});
	}

	deleteItem(country: CountryListItem) {
		this.countryService.deleteCountry(country.id).subscribe(() => {
			let listItem = ObjectUtilities.findItemFromPath(this.items, "id.value", country.id.toString());
			ObjectUtilities.delete(this.items, listItem);
		});
	}

	updateItem(country: CountryListItem) {
		this.openModal(AddCountryComponent, {
			countryId: country.id
		}).then(value => {
			if (value == null) {
				return;
			}
			this.onCountryUpdated(value);
		});
	}

	onCountryCreated(country: Country) {
		this.countryService.addCountry(country).subscribe(() => {
			this.items.push(country);
		});
	}

	onCountryUpdated(country: Country) {
		this.countryService.updateCountry(country).subscribe(() => {
			let listItem = ObjectUtilities.findItemFromPath(this.items, "id.value", country.id.toString());
			listItem.name = country.name;
		});
	}

	loadItems(): void {
		this.countryService.getCountries().subscribe((data: CountryListItem[])=> {
			this.items = data;
		});
	}
}