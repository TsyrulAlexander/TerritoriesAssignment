import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import {AreaListItem} from "../models/area-list-item";
import {RegionListItem} from "../models/region-list-item";
import {Guid} from "guid-typescript";
import {BaseHttpService} from "./base-http-service";
import {Region} from "../models/region";

@Injectable()
export class RegionService extends BaseHttpService {
	private url = "/api/region";
	getRegions(area: AreaListItem): Observable<RegionListItem[]> {
		return this.castObjects(this.http.get<AreaListItem[]>(this.url + "/getItems?areaId=" + area.id), Region);
	}
	getRegion(id: Guid): Observable<Region> {
		return this.castObject(this.http.get<Region>(this.url + "?id=" + id.toString()), Region);
	}
	addRegion(region: Region) {
		return this.http.post(this.url + "/add", region.toServerObject());
	}
	updateRegion(region: Region) {
		return this.http.post(this.url + "/update", region.toServerObject());
	}
	deleteRegion(regionId: Guid) {
		return this.http.delete(this.url + "/" + regionId.toString());
	}
}