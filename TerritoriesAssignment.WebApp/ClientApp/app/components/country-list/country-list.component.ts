import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {CountryService} from "../../services/country.service";
import {CountryListItem} from "../../models/country-list-item";
import {BaseListComponent} from "../base-list/base-list.component";
import {MatDialog} from "@angular/material";
import {AddCountryComponent} from "../add-country/add-country.component";
import {Country} from "../../models/country";
import {ObjectUtilities} from "../../utilities/object-utilities";
import {ViewListItem} from "../../models/view-list-item";
import {ManagerDistributionComponent} from "../manager-distribution/manager-distribution.component";
import {AreaListItem} from "../../models/area-list-item";
import {AreaService} from "../../services/area.service";
import {MapService} from "../../services/map.service";
import {MapItem} from "../../models/map-item";
import {ManagerInfo} from "../../models/manager-info";
import {Guid} from "guid-typescript";

@Component({
	selector: "ks-country-list",
	templateUrl: "country-list.component.html",
	styleUrls: ["country-list.component.css"],
	providers: [CountryService, MapService]
})
export class CountryListComponent extends BaseListComponent<CountryListItem> implements OnInit {

	constructor(private countryService: CountryService, private mapService: MapService, dialog: MatDialog) {
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
			this.items.push(new ViewListItem<CountryListItem>(country));
		});
	}

	onCountryUpdated(country: Country) {
		this.countryService.updateCountry(country).subscribe(() => {
			let listItem = ObjectUtilities.findItemFromPath(this.items, "item.id.value", country.id.toString());
			listItem.item.name = country.name;
		});
	}

	loadItems(): void {
		this.countryService.getCountries().subscribe((data: CountryListItem[])=> {
			this.items = [];
			data.forEach(listItem => {
				this.items.push(new ViewListItem<CountryListItem>(listItem))
			}, this);
		});
	}

	managersDistribution(country: CountryListItem, areas: AreaListItem[]) {
		if (!areas || areas.length > 1) {
			this.mapService.getAreaMaps(areas.map(value => value.id)).subscribe(mapAreas => {
				this.openManagerDistributionDialog(country, mapAreas);
			});
		} else {
			this.openManagerDistributionDialog(country, null);
		}
	}

	openManagerDistributionDialog(country: CountryListItem, areas: MapItem[]) {
		let dialog = this.dialog.open(ManagerDistributionComponent, {
			data: {
				country,
				areas
			}
		});
		dialog.afterClosed().subscribe((managers: ManagerInfo[]) => {
			if (!managers || managers.length < 2) {
				return;
			}
			this.callManagersDistribution(country.id, managers);
		});
	}
	callManagersDistribution(countryId: Guid, managers: ManagerInfo[]) {
		this.countryService.managersDistribution(countryId, managers).subscribe(this.callManagersDistributionResponse);
	}
	callManagersDistributionResponse() {
		this.openManagerDistributionDialogResult();
	}
	openManagerDistributionDialogResult() {

	}
}