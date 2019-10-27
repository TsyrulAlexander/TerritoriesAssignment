import {Injectable} from "@angular/core";
import {Observable} from "rxjs";
import {Attribute} from "../models/attribute";
import {AttributeValue} from "../models/attribute-value";
import {Guid} from "guid-typescript";
import {BaseHttpService} from "./base-http-service";

@Injectable()
export class AttributeService extends BaseHttpService {
	url: string = "api/attribute";
	getAttributes(): Observable<Attribute[]> {
		return this.http.get<Attribute[]>(this.url + "/getAttributes");
	}
	getAttributeValues(regionId: Guid): Observable<AttributeValue[]> {
		return this.castObjects(this.http.get<AttributeValue[]>(this.url + "/getAttributeValues/" + regionId.toString()), AttributeValue);
	}
	getAttributeValuesFromArea(areaId: Guid): Observable<AttributeValue[]> {
		return this.castObjects(this.http.get<AttributeValue[]>(this.url + "/getAttributeValuesFromArea/" + areaId.toString()), AttributeValue);
	}
	getAttributeValuesFromCountry(countryId: Guid): Observable<AttributeValue[]> {
		return this.castObjects(this.http.get<AttributeValue[]>(this.url + "/getAttributeValuesFromCountry/" + countryId.toString()), AttributeValue);
	}
	createAttributeValue(value: AttributeValue) {
		return this.http.post(this.url + "/addAttributeValue",  value.toServerObject());
	}
	updateAttributeValue(value: AttributeValue) {
		return this.http.post(this.url + "/updateAttributeValue",value.toServerObject());
	}
}