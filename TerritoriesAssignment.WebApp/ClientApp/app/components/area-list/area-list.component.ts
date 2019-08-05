import { Component, Input } from '@angular/core';
import {Country} from "../../models/country";
import {Area} from "../../models/area";
import {AreaService} from "../../services/area.service";

@Component({
	selector: 'ks-area-list',
	templateUrl: 'area-list.component.html',
	styleUrls: ['area-list.component.css'],
	providers: [AreaService]
})
export class AreaListComponent {
	private _isShow: boolean = false;
	@Input() country: Country;
	@Input() set isShow (value: boolean) {
		this._isShow = value;
		if (value) {
			this.initAreas();
		}
	}
	get isShow() {
		return this._isShow;
	}
	public areas: Area[];
	constructor(private areaService: AreaService) {}

	initAreas(): Area[] {
		if (this.areas) {
			return this.areas;
		}
		this.areaService.getAreas(this.country).subscribe((data: Area[])=> {
			this.areas = data;
		});
	}
}