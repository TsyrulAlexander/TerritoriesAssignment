import {BaseLookup} from "./base-lookup";

export class Region implements BaseLookup {
	constructor(public id: string, public name: string) {}
}