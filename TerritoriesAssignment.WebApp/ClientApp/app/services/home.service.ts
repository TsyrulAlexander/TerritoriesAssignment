import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class HomeService {
	constructor(private http: HttpClient) {
	}
	private url = "/api/home";
}