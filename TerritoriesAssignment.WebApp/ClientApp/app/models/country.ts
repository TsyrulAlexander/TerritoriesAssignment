import {BaseLookup} from "./base-lookup";

export class Country implements BaseLookup {
    id: string;
    name: string;

    constructor(id: string, name: string) {}
}