import {Component, Input, OnInit, QueryList, ViewChildren} from '@angular/core';
import { CountryListItem } from "../../models/country-list-item";
import { AreaListItem } from "../../models/area-list-item";
import { AreaService } from "../../services/area.service";
import {BaseListComponent} from "../base-list/base-list.component";
import {AddCountryComponent} from "../add-country/add-country.component";
import {MatDialog} from "@angular/material";
import {AddAreaComponent} from "../add-area/add-area.component";
import {Area} from "../../models/area";
import {AreaComponent} from "../area/area.component";

@Component({
    selector: 'ks-area-list',
    templateUrl: 'area-list.component.html',
    providers: [AreaService]
})

export class AreaListComponent extends BaseListComponent<AreaListItem> {
    @Input() isShow: boolean;
    @Input() country: CountryListItem;
    @ViewChildren(AreaComponent) itemComponents !: QueryList<AreaComponent>;
    constructor(public areaService: AreaService, private dialog: MatDialog) {
        super();
    }

    loadItems(): void {
        if (this.items != null) {
            return;
        }
        this.areaService.getAreas(this.country).subscribe(date => {
            this.items = date;
        });
    }
    selectAllItems(): void {
        super.selectAllItems();
        this.itemComponents.forEach(itemComponent=> {
            itemComponent.isSelected = true;
        })
    }

    createItem(): void {
        let dialog = this.dialog.open(AddAreaComponent);
        dialog.afterClosed().subscribe(country => {
            if (country == null) {
                return;
            }
            this.onAreaCreated(country);
        });
    }

    onAreaCreated(area: Area) {
        this.areaService.addArea(area).subscribe(() => {
            this.items.push(area);
        });
    }
}