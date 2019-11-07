import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import {CountryListItem} from "../models/country-list-item";
import {Country} from "../models/country";
import {Guid} from "guid-typescript";
import {BaseHttpService} from "./base-http-service";
import {ManagerInfo} from "../models/manager-info";
import {ManagerInfoResponse} from "../models/manager-info-response";
@Injectable()
export class CountryService extends BaseHttpService {
	private url = "/api/country";
	getCountries(): Observable<CountryListItem[]> {
		return this.castObjects(this.http.get<CountryListItem[]>(this.url + "/getItems"), CountryListItem);
	}
	getCountry(id: Guid): Observable<Country> {
		return this.castObject(this.http.get<Country>(this.url + "?id=" + id.toString()), Country);
	}
	addCountry(country: Country) {
		return this.http.post(this.url + "/add", country.toServerObject());
	}
	updateCountry(country: Country) {
		return this.http.post(this.url + "/update", country.toServerObject());
	}
	deleteCountry(countryId: Guid) {
		return this.http.delete(this.url + "/" + countryId.toString());
	}
	managersDistribution(countryId: Guid, managers: ManagerInfo[]): Observable<ManagerInfoResponse[]> {
		return this.castObjects(this.http.post<ManagerInfoResponse[]>(
			this.url + "/managersDistribution?countryId=" + countryId.toString(),
			managers.map(value => value.toServerObject())), ManagerInfoResponse);
	}
}