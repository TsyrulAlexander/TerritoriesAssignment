import { Injectable } from '@angular/core';
import { BaseHttpService } from './base-http-service';
import { Observable } from 'rxjs';
import { SettingValue } from '../models/SettingValue';

@Injectable()
export class SettingsService extends BaseHttpService {
    private url = "/api/setting";
    getSettingsValue(): Observable<SettingValue[]> {
        return this.castObjects(this.http.get<SettingValue>(this.url + "/getSettings"), SettingValue);
    }
    setSettingsValue(value: SettingValue[]) {
        return this.http.post(this.url + "/setSettings", value.map(obj => obj.toServerObject()));
    }
}