import { BaseObject } from "./base-object";
import { Guid } from "guid-typescript";
import { Setting } from "./setting";
import {SettingValueType} from "./enums/setting-value-type";

export class SettingValue extends BaseObject {
	public textValue: string;
	public doubleValue: number;
	public integerValue: number;
	public boolValue: boolean;
	public dateValue: Date;
	public setting: Setting;
	public settingValueType: SettingValueType;
	constructor(public id: Guid = null) {
		super(id);
	}
}