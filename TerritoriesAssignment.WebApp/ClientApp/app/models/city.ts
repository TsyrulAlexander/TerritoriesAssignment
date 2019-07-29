import {BaseLookup} from "./base-lookup";

export class City implements BaseLookup {
	constructor(public id: string, public name: string) {}
}