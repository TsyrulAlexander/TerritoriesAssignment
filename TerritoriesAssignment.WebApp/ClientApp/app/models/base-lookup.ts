import {BaseObject} from "./base-object";

export class BaseLookup implements BaseObject {
    id: string;

    constructor(id: string, public name: string) {}
}