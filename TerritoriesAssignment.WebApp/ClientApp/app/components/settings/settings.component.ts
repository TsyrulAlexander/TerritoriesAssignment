import { Component, OnInit } from '@angular/core';
import { SettingsService } from '../../services/settings.service';
import { SettingValue } from '../../models/SettingValue';
import { BaseComponent } from '../base/base.component';

@Component({
    selector: 'ks-settings',
    templateUrl: './settings.component.html',
    styleUrls: ['./settings.component.css'],
    providers: [SettingsService]
})
/** settings component*/
export class SettingsComponent extends BaseComponent implements OnInit {
    /** settings ctor */
    public settings: SettingValue[] = [];

    constructor(private settingService: SettingsService) {
        super();
    }
    ngOnInit(): void {
        this.settingService.getSettingsValue().subscribe((data: SettingValue[]) => {
            this.settings = data;
        })
    }
    settingsChecked(): boolean {
        return true;
    }
}