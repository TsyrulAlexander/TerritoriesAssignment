import {Component, Inject} from '@angular/core';
import {ModalComponent} from "../modal/modal.component";
import {Guid} from "guid-typescript";
import {Country} from "../../models/country";
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material";
import {CountryService} from "../../services/country.service";

@Component({
    selector: 'ks-add-country',
    templateUrl: './add-country.component.html',
    styleUrls: ["./add-country.component.css"],
    providers:[CountryService]
})
export class AddCountryComponent extends ModalComponent<AddCountryComponent> {
    public country: Country = new Country(Guid.create());
    constructor(dialogRef: MatDialogRef<AddCountryComponent> = null, @Inject(MAT_DIALOG_DATA) protected data: any = null, protected countryService: CountryService = null) {
        super(dialogRef);
        let countryId = data && data.countryId;
        if (countryId && Guid.isGuid(countryId)) {
            this.countryService.getCountry(countryId).subscribe(value => {
               this.country = value;
            });
        }
    }
    save() {
        this.close(this.country);
    }
}