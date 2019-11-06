import {ModalComponent} from "../modal/modal.component";
import {Component, Inject} from "@angular/core";
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material";
import {ManagerInfo} from "../../models/manager-info";
import {CountryListItem} from "../../models/country-list-item";
import {MapItem} from "../../models/map-item";
import {Guid} from "guid-typescript";
@Component({
	selector: "ks-manager-distribution",
	templateUrl: "./manager-distribution.component.html"
})
export class ManagerDistributionComponent extends ModalComponent<ManagerDistributionComponent> {
	managers: ManagerInfo[] = [];
	country: CountryListItem;
	isUseStartAreas = false;
	constructor(dialogRef: MatDialogRef<ManagerDistributionComponent> = null,
				@Inject(MAT_DIALOG_DATA) protected data: any = null) {
		super(dialogRef);
		this.country = data && data.country;
		if (data && data.areas) {
			this.isUseStartAreas = true;
			data.areas.forEach((area: MapItem) => {
				this.addManager(null, area);
			}, this);
		}
	}
	addManager(number: number = null, area: MapItem = null, color: string = null) {
		let managerInfo = new ManagerInfo();
		managerInfo.id = Guid.create();
		managerInfo.area = area;
		managerInfo.color = color;
		if (!number) {
			managerInfo.number = this.managers.length + 1;
		}
		this.managers.push(managerInfo);
	}
	start() {
		this.close(this.managers);
	}
}