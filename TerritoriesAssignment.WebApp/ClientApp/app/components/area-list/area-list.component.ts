import {Component, Input, OnInit, QueryList, ViewChildren} from '@angular/core';
import { CountryListItem } from "../../models/country-list-item";
import { AreaListItem } from "../../models/area-list-item";
import { AreaService } from "../../services/area.service";
import {BaseListComponent} from "../base-list/base-list.component";
import {MatDialog} from "@angular/material";
import {AddAreaComponent} from "../add-area/add-area.component";
import {Area} from "../../models/area";
import {AddCountryComponent} from "../add-country/add-country.component";
import {ObjectUtilities} from "../../utilities/object-utilities";

@Component({
    selector: 'ks-area-list',
    templateUrl: 'area-list.component.html',
    providers: [AreaService]
})

export class AreaListComponent extends BaseListComponent<AreaListItem> {
    @Input() isShow: boolean;
    @Input() country: CountryListItem;
    constructor(public areaService: AreaService, dialog: MatDialog) {
        super(dialog);
    }

    loadItems(): void {
        if (this.items != null) {
            return;
        }
        this.areaService.getAreas(this.country).subscribe(date => {
            this.items = date;
        });
    }

    createItem(): void {
        this.openModal(AddAreaComponent).then(value => {
            if (value == null) {
                return;
            }
            this.onAreaCreated(value);
        });
    }

    updateItem(area: AreaListItem) {
        this.openModal(AddAreaComponent, {
            areaId: area.id
        }).then(value => {
            if (value == null) {
                return;
            }
            this.onAreaUpdated(value);
        });
    }

    deleteItem(area: AreaListItem) {
        this.areaService.deleteArea(area.id).subscribe(() => {
            let listItem = ObjectUtilities.findItemFromPath(this.items, "id.value", area.id.toString());
            ObjectUtilities.delete(this.items, listItem);
        });
    }

    onAreaCreated(area: Area) {
        area.country = this.country;
        this.areaService.addArea(area).subscribe(() => {
            this.items.push(area);
        });
    }

    onAreaUpdated(area: Area) {
        this.areaService.updateArea(area).subscribe(() => {
            this.items.push(area);
        });
    }
}