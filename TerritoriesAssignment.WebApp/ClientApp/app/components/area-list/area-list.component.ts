import { Component, Input, OnInit } from '@angular/core';
import { Country } from "../../models/country";
import { Area } from "../../models/area";
import { AreaService } from "../../services/area.service";

@Component({
    selector: 'ks-area-list',
    templateUrl: 'area-list.component.html',
    providers: [AreaService]
})

export class AreaListComponent implements OnInit {
    @Input() isShow: boolean;
    @Input() country: Country;
    public areas: Area[];
    constructor(public areaService: AreaService) {

    }
    ngOnInit(): void {
        this.areaService.getAreas(this.country).subscribe(date => {
            this.areas = date;
        });
    }
}