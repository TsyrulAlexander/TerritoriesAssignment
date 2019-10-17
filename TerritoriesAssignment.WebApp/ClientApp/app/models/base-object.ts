import {Guid} from "guid-typescript";

export abstract class BaseObject {
    protected constructor(public id: Guid) {

    }
    toServerObject() {
        let obj = {};
        Object.keys(this).forEach(key => this.setServerColumnValue(obj, key));
        return obj;
    }
    protected setServerColumnValue(serverObj: any, columnName: string) {
        let value = this[columnName];
        if (value instanceof Guid) {
            value = (value as Guid).toString();
        }
        serverObj[columnName] = value;
    }
}