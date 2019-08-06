import { Component, Input } from '@angular/core';
import {Area} from "../../models/area";

@Component({
	selector: 'ks-area',
	templateUrl: './area.component.html',
	styleUrls: ['./area.component.css']
})
export class AreaComponent {
	@Input() area: Area;
	public isShowRegion: boolean;
	constructor() {

	}
}