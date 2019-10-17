import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import {AreaListItem} from "../models/area-list-item";
import {RegionListItem} from "../models/region-list-item";
@Injectable()
export class RegionService {
	constructor(private http: HttpClient) {
	}
	private url = "/api/region";
	getRegions(area: AreaListItem): Observable<RegionListItem[]> {
		return this.http.get<AreaListItem[]>(this.url + "/getItems?areaId=" + area.id);
	}
}