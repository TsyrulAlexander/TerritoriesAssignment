import {Component, Inject} from "@angular/core";
import {ModalComponent} from "../modal/modal.component";
import {Guid} from "guid-typescript";
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material";
import {RegionService} from "../../services/region.service";
import {Region} from "../../models/region";

@Component({
	selector: "ks-add-region",
	templateUrl: "./add-region.component.html",
	providers: [RegionService]
})
export class AddRegionComponent extends ModalComponent<AddRegionComponent>{
	public region: Region = new Region(Guid.create());
	constructor(dialogRef: MatDialogRef<AddRegionComponent> = null, @Inject(MAT_DIALOG_DATA) protected data: any = null, protected regionService: RegionService = null) {
		super(dialogRef);
		let regionId = data && data.regionId;
		if (regionId && Guid.isGuid(regionId)) {
			this.regionService.getRegion(regionId).subscribe(value => {
				this.region = value;
			});
		}
	}
	save() {
		this.close(this.region);
	}
}