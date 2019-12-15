import { BaseLookup } from "./base-lookup";
import { Guid } from "guid-typescript";
import {SettingValueType} from "./enums/setting-value-type";

export class Setting extends BaseLookup {
    constructor(id: Guid = null, public name: string, public type: SettingValueType) {
        super(id, name);
    }
}