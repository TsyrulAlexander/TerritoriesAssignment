import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import {Country} from "../models/country";
import {Area} from "../models/area";
@Injectable()
export class AreaService {
	constructor(private http: HttpClient) {
	}
	private url = "/api/area";
	getAreas(country: Country): Observable<Area[]> {
		return this.http.get<Area[]>(this.url + "/getItems?countryId=" + country.id);
	}
}