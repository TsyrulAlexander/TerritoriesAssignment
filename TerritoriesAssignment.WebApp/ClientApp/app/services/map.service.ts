import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import {Guid} from "guid-typescript";
import {MapItem} from "../models/map-item";
@Injectable()
export class MapService {
	constructor(private http: HttpClient) {
	}
	private url = "/api/map";
	getAreasMap(countryId: Guid): Observable<MapItem[]> {
		return this.http.get<MapItem[]>(this.url + "/getAreas?countryId=" + countryId.toString());
	}
	getAreaMap(areaId: Guid): Observable<MapItem> {
		return this.http.get<MapItem>(this.url + "/getArea?areaId=" + areaId.toString());
	}
}