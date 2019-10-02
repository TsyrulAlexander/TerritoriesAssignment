import { Component, OnInit, Input } from '@angular/core';
import { Region } from "../../models/region";
import { RegionService } from "../../services/region.service";
import { Area } from "../../models/area";

@Component({
    selector: 'ks-region-list',
    templateUrl: 'region-list.component.html',
    providers: [RegionService]
})
export class RegionListComponent implements OnInit {
    @Input() isShow: boolean;
    @Input() area: Area;
    public regions: Region[];
    constructor(public regionService: RegionService) {

    }
    ngOnInit(): void {
        this.regionService.getRegions(this.area).subscribe(date => {
            this.regions = date;
        });
    }
}