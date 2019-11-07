import {Component, Inject} from "@angular/core";
import {Guid} from "guid-typescript";
import {Area} from "../../models/area";
import {ModalComponent} from "../modal/modal.component";
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material";
import {AreaService} from "../../services/area.service";

@Component({
	selector: 'ks-add-area',
	templateUrl: './add-area.component.html',
	providers: [AreaService]
})
export class AddAreaComponent extends ModalComponent<AddAreaComponent> {
	public area: Area = new Area(Guid.create());
	constructor(dialogRef: MatDialogRef<AddAreaComponent> = null, @Inject(MAT_DIALOG_DATA) protected data: any = null,
				protected areaService: AreaService = null) {
		super(dialogRef);
		let areaId = data && data.areaId;
		if (areaId && Guid.isGuid(areaId)) {
			this.areaService.getArea(areaId).subscribe(value => {
				this.area = value;
			});
		}
	}
	save() {
		this.close(this.area);
	}
}