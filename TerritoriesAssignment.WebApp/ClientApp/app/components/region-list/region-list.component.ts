import {Component, OnInit, Input, ViewChildren, QueryList} from '@angular/core';
import { RegionListItem } from "../../models/region-list-item";
import { RegionService } from "../../services/region.service";
import { AreaListItem } from "../../models/area-list-item";
import {BaseListComponent} from "../base-list/base-list.component";
import {RegionComponent} from "../region/region.component";

@Component({
    selector: 'ks-region-list',
    templateUrl: 'region-list.component.html',
    providers: [RegionService]
})
export class RegionListComponent extends  BaseListComponent<RegionListItem> {
    @Input() isShow: boolean;
    @Input() area: AreaListItem;
    @ViewChildren(RegionComponent) itemComponents !: QueryList<RegionComponent>;
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

    selectAllItems(): void {
        super.selectAllItems();
        this.itemComponents.forEach(itemComponent=> {
            itemComponent.isSelected = true;
        })
    }
}