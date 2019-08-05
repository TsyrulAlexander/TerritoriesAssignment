import { Component, Input } from '@angular/core';
import {RegionService} from "../../services/region.service";
import {Area} from "../../models/area";
import {Region} from "../../models/region";

@Component({
    selector: 'ks-region-list',
    templateUrl: 'region-list.component.html',
    styleUrls: ['region-list.component.css'],
	providers: [RegionService]
})
export class RegionListComponent {
	private _isShow: boolean = false;
	@Input() area: Area;
	@Input() set isShow(value: boolean) {
		this._isShow = value;
		if (value) {
			this.initRegions();
		}
	}
	get isShow() {
		return this._isShow;
	}
	public regions: Region[];

	constructor(private regionService: RegionService) {}

	initRegions() {
		if (!this.area) {
			return;
		}
		this.regionService.getRegions(this.area).subscribe((data)=> {
			this.regions = data;
		});
	}
}