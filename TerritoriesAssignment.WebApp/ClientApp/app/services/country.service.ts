import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import {CountryListItem} from "../models/country-list-item";
import {Country} from "../models/country";
import {Guid} from "guid-typescript";
@Injectable()
export class CountryService {
	constructor(private http: HttpClient) {
	}
	private url = "/api/country";
	getCountries(): Observable<CountryListItem[]> {
		return this.http.get<CountryListItem[]>(this.url + "/getItems");
	}
	getCountry(id: Guid): Observable<Country> {
		return this.http.get<Country>(this.url + "?id=" + id.toString());
	}
	addCountry(country: Country) {
		return this.http.post(this.url, country.toServerObject());
	}
}