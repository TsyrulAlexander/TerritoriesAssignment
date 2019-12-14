import {Component, Inject} from "@angular/core";
import {ModalComponent} from "../modal/modal.component";
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material";
import {ViewListItem} from "../../models/view-list-item";
import {MapItem} from "../../models/map-item";
import {ManagerInfo} from "../../models/manager-info";
import {ManagerInfoResponse} from "../../models/manager-info-response";
import {ObjectUtilities} from "../../utilities/object-utilities";

@Component({
	selector: "ks-manager-distribution-result",
	templateUrl: "./manager-distribution-result.component.html"
})
export class ManagerDistributionResultComponent extends ModalComponent<ManagerDistributionResultComponent> {
	mapItems: ViewListItem<MapItem>[] = [];
	constructor(dialogRef: MatDialogRef<ManagerDistributionResultComponent> = null, @Inject(MAT_DIALOG_DATA) protected data: any = null) {
		super(dialogRef);
		let managers: ManagerInfo[] = data.managers;
		let managersResponse: ManagerInfoResponse[] = data.territorySolution;
		managersResponse.forEach(managerResponse => {
			let manager = ObjectUtilities.findItemFromPath(managers, "id.value", managerResponse.id.toString());
			managerResponse.areas.forEach(area => {
				area.color = manager.color;
				this.mapItems.push(new ViewListItem<MapItem>(area));
			}, this);
		}, this);
	}
}