import {BaseObject} from "../models/base-object";
import {Observable} from "rxjs";
import {map, mergeAll} from "rxjs/operators";
import {HttpClient} from "@angular/common/http";
import {Injectable} from "@angular/core";

@Injectable()
export abstract class BaseHttpService {
	constructor(protected http: HttpClient) {

	}
	castObject<T extends BaseObject>(observable: Observable<any>, type: {new(): T}): Observable<T> {
		return observable.pipe(map(value => this.mapObjects([value], type)), mergeAll<T>());
	}
	castObjects<T extends BaseObject>(observable: Observable<any>, type: {new(): T}): Observable<T[]> {
		return observable.pipe(map(value => this.mapObjects(value, type)));
	}
	mapObjects<T extends BaseObject>(data: any[], type: {new(): T}): T[] {
		return  data.map<T>(dataItem => {
			let obj = new type();
			obj.fromServerObject(dataItem);
			return obj;
		}, this);
	}
}