import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import {Area} from "../models/area";
import {Region} from "../models/region";
@Injectable()
export class RegionService {
	constructor(private http: HttpClient) {
	}
	private url = "/api/region";
	getRegions(area: Area): Observable<Region[]> {
		return this.http.get<Area[]>(this.url + "/getItems?areaId=" + area.id);
	}
}