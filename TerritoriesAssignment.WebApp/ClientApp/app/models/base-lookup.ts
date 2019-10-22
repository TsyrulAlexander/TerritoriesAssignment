import {BaseObject} from "./base-object";
import {Guid} from "guid-typescript";

export abstract class BaseLookup extends BaseObject {
    constructor(id: Guid, public name: string) {
        super(id);
    }
}