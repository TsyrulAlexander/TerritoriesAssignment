import {BaseObject} from "./base-object";

export abstract class BaseLookup implements BaseObject {
    id: string;

    constructor(id: string, public name: string) {}
}