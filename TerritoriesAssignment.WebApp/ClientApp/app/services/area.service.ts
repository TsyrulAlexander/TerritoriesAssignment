import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { CountryListItem } from "../models/country-list-item";
import { AreaListItem } from "../models/area-list-item";
import {Area} from "../models/area";
import {Guid} from "guid-typescript";
@Injectable()
export class AreaService {
	constructor(private http: HttpClient) {
	}
	private url = "/api/area";
	getAreas(country: CountryListItem): Observable<AreaListItem[]> {
		return this.http.get<AreaListItem[]>(this.url + "/getItems?countryId=" + country.id);
	}

	addArea(area: Area) {
		return this.http.post(this.url + "/add", area);
	}

	updateArea(area: Area) {
		return this.http.post(this.url + "/update", area);
	}

	getArea(id: Guid): Observable<Area> {
		return this.http.get<Area>(this.url + "?id=" + id.toString());
	}

	deleteArea(areaId: Guid) {
		return this.http.delete(this.url + "/" + areaId.toString());
	}
}