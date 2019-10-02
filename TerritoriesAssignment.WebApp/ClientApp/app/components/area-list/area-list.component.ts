import { Component, Input } from '@angular/core';
import { Country } from "../../models/country";

@Component({
    selector: 'ks-area-list',
    templateUrl: 'area-list.component.html',
})

export class AreaListComponent {
    @Input() isShow: boolean;
    @Input() country: Country;
    constructor() {

    }
}