import {Injectable } from "@angular/core";
import {HttpClient} from "@angular/common/http"
import {Country} from "../models/country";
import {Observable} from "rxjs"
import {City} from "../models/city";
@Injectable()
export class CityService {
	private url = "api/city";
	constructor(private http: HttpClient ) {

	}

	getCities(country: Country) {
		// : Observable<City[]>
		if (country == null) {
			//todo
		}
		return this.http.get(this.url + "?country_id=" + country.id);
	}
}