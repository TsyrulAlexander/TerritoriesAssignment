import { Component, OnInit } from '@angular/core';
import { SettingsService } from '../../services/settings.service';
import { SettingValue } from '../../models/setting-value';
import { BaseComponent } from '../base/base.component';
import {SettingValueType} from "../../models/enums/setting-value-type";
import { Router } from '@angular/router';

@Component({
    selector: 'ks-settings',
    templateUrl: './settings.component.html',
    styleUrls: ['./settings.component.css'],
    providers: [SettingsService]
})
/** settings component*/
export class SettingsComponent extends BaseComponent implements OnInit {
    settingType = SettingValueType;
    public settings: SettingValue[] = [];

    constructor(private settingService: SettingsService, private router: Router) {
        super();
    }
    ngOnInit(): void {
        this.settingService.getSettingsValue().subscribe((data: SettingValue[]) => {
            this.settings = data;
        });
    }
    onSave(): void {
        this.settingService.setSettingsValue(this.settings).subscribe(() => {/*alert("Saved")*/});
        this.router.navigate(['/territory']);
    }
}