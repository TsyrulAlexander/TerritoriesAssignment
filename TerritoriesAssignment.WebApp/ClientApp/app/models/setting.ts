import { BaseLookup } from "./base-lookup";
import { Guid } from "guid-typescript";

export class Setting extends BaseLookup {
    constructor(id: Guid = null, public name: string, public code: string) {
        super(id, name);
    }
}