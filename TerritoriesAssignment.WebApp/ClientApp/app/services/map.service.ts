import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import {Guid} from "guid-typescript";
import {MapItem} from "../models/map-item";
import {BaseHttpService} from "./base-http-service";
@Injectable()
export class MapService extends BaseHttpService {
	private url = "/api/map";
	getAreasMap(countryId: Guid): Observable<MapItem[]> {
		return this.castObjects(this.http.get<MapItem[]>(this.url + "/getAreas?countryId=" + countryId.toString()), MapItem);
	}
	getAreaMap(areaId: Guid): Observable<MapItem> {
		return this.castObject(this.http.get<MapItem>(this.url + "/getArea?areaId=" + areaId.toString()), MapItem);
	}
	getAreaMaps(areas: Guid[]): Observable<MapItem[]> {
		return this.castObjects(this.http.post<MapItem[]>(this.url + "/getAreas", areas.map(value => value.toString())), MapItem);
	}
}