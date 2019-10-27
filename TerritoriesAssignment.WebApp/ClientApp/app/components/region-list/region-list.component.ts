import {Component, Input} from '@angular/core';
import { RegionListItem } from "../../models/region-list-item";
import { RegionService } from "../../services/region.service";
import { AreaListItem } from "../../models/area-list-item";
import {BaseListComponent} from "../base-list/base-list.component";
import {MatDialog} from "@angular/material";
import {AddCountryComponent} from "../add-country/add-country.component";
import {CountryListItem} from "../../models/country-list-item";
import {ObjectUtilities} from "../../utilities/object-utilities";
import {AddRegionComponent} from "../add-region/add-region.component";
import {Region} from "../../models/region";

@Component({
    selector: 'ks-region-list',
    templateUrl: 'region-list.component.html',
    providers: [RegionService]
})
export class RegionListComponent extends  BaseListComponent<RegionListItem> {
    @Input() isShow: boolean;
    @Input() area: AreaListItem;
    constructor(public regionService: RegionService, dialog: MatDialog) {
        super(dialog);
    }

    createItem() {
        this.openModal(AddRegionComponent).then(value => {
            if (value == null) {
                return;
            }
            this.onRegionCreated(value);
        });
    }

    deleteItem(region: RegionListItem) {
        this.regionService.deleteRegion(region.id).subscribe(() => {
            let listItem = ObjectUtilities.findItemFromPath(this.items, "id.value", region.id.toString());
            ObjectUtilities.delete(this.items, listItem);
        });
    }

    updateItem(region: RegionListItem) {
        this.openModal(AddRegionComponent, {
            regionId: region.id
        }).then(value => {
            if (value == null) {
                return;
            }
            this.onRegionUpdate(value);
        });
    }

    onRegionCreated(region: Region) {
        region.area = this.area;
        this.regionService.addRegion(region).subscribe(() => {
            this.items.push(region);
        });
    }

    onRegionUpdate(region: Region) {
        this.regionService.updateRegion(region).subscribe(() => {
            let listItem = ObjectUtilities.findItemFromPath(this.items, "id.value", region.id.toString());
            listItem.name = region.name;
        });
    }

    loadItems(): void {
        this.regionService.getRegions(this.area).subscribe(date => {
            this.items = date;
        });
    }
}