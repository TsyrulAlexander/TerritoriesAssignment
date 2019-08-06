import { Component, Input } from '@angular/core';
import {Region} from "../../models/region";

@Component({
	selector: 'ks-region',
	templateUrl: './region.component.html',
	styleUrls: ['./region.component.css']
})

export class RegionComponent {
    @Input() region: Region;
	constructor() {}
}