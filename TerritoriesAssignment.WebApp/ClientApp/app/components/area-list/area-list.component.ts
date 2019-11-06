import {Component, Input} from '@angular/core';
import { CountryListItem } from "../../models/country-list-item";
import { AreaListItem } from "../../models/area-list-item";
import { AreaService } from "../../services/area.service";
import {BaseListComponent} from "../base-list/base-list.component";
import {MatDialog} from "@angular/material";
import {AddAreaComponent} from "../add-area/add-area.component";
import {Area} from "../../models/area";
import {ObjectUtilities} from "../../utilities/object-utilities";
import {ViewListItem} from "../../models/view-list-item";

@Component({
    selector: 'ks-area-list',
    templateUrl: 'area-list.component.html',
    providers: [AreaService]
})

export class AreaListComponent extends BaseListComponent<AreaListItem> {
    @Input() isShow: boolean;
    @Input() country: ViewListItem<CountryListItem>;
    constructor(public areaService: AreaService, dialog: MatDialog) {
        super(dialog);
    }

    loadItems(): void {
        if (this.items != null) {
            return;
        }
        this.areaService.getAreas(this.country.item).subscribe((data: AreaListItem[]) => {
            this.items = [];
            data.forEach(listItem => {
                this.items.push(new ViewListItem<CountryListItem>(listItem))
            }, this);
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
        area.country = this.country.item;
        this.areaService.addArea(area).subscribe(() => {
            this.items.push(new ViewListItem<AreaListItem>(area));
        });
    }

    onAreaUpdated(area: Area) {
        this.areaService.updateArea(area).subscribe(() => {
            let listItem = ObjectUtilities.findItemFromPath(this.items, "item.id.value", area.id.toString());
            listItem.item.name = area.name;
        });
    }
}