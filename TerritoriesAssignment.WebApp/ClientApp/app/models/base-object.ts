import {Guid} from "guid-typescript";

export abstract class BaseObject {
    private Id: Guid;
    public set id(value: Guid) {
        if (!value) {
            return;
        }
        if (value instanceof Guid) {
            this.Id = value;
        } else {
            this.Id = Guid.parse(String(value));
        }
    }
    public get id() {
        return this.Id;
    }
    constructor(id: Guid = null) {
        this.id = id;
    }
    public toServerObject() {
        let obj = {};
        Object.keys(this).forEach(key => obj[key] = this.getServerColumnValue(obj, key));
        return obj;
    }
    public fromServerObject(value: any) {
        Object.assign(this, value);
    }
    protected getServerColumnValue(serverObj: any, columnName: string) {
        let value = this[columnName];
        if (value instanceof Guid) {
            value = (value as Guid).toString();
        }
        if (value instanceof BaseObject) {
            value = (value as BaseObject).toServerObject();
        }
        return  value;
    }
}