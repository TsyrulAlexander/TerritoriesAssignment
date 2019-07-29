import {BaseLookup} from "./base-lookup";

export class Country implements BaseLookup {
    constructor(public id: string, public name: string) {}
}