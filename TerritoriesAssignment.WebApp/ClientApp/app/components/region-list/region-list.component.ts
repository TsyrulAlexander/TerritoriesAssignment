import {Component, Input} from '@angular/core';
import { RegionListItem } from "../../models/region-list-item";
import { RegionService } from "../../services/region.service";
import { AreaListItem } from "../../models/area-list-item";
import {BaseListComponent} from "../base-list/base-list.component";

@Component({
    selector: 'ks-region-list',
    templateUrl: 'region-list.component.html',
    providers: [RegionService]
})
export class RegionListComponent extends  BaseListComponent<RegionListItem> {
    @Input() isShow: boolean;
    @Input() area: AreaListItem;
    constructor(public regionService: RegionService) {
        super();
    }

    createItem(): void {
    }

    loadItems(): void {
        this.regionService.getRegions(this.area).subscribe(date => {
            this.items = date;
        });
    }
}