import {Component} from "@angular/core";
import {Guid} from "guid-typescript";
import {Area} from "../../models/area";
import {ModalComponent} from "../modal/modal.component";

@Component({
	selector: 'ks-add-area',
	templateUrl: './add-area.component.html'
})
export class AddAreaComponent extends ModalComponent<AddAreaComponent> {
	public area: Area = new Area(Guid.create());
	save() {
		this.close(this.area);
	}
}