import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import {Country} from "../models/country";
@Injectable()
export class CountryService {
	constructor(private http: HttpClient) {
	}
	private url = "/api/country";
	getCountries(): Observable<Country[]> {
		return this.http.get<Country[]>(this.url + "/getItems");
	}
}