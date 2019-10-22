import {Injectable} from "@angular/core";
import * as _ from "underscore";
//const _ = require('underscore');

@Injectable({ providedIn: 'root' })
export class ObjectUtilities {
	static where(array: any[], findObject: any) {
		return _.where(array, findObject);
	}
	static findWhere(array: any[], findObject: any) {
		return _.findWhere(array, findObject);
	}
	static delete(array: any[], item: any) {
		const index = array.indexOf(item, 0);
		if (index > -1) {
			array.splice(index, 1);
		}
	}
}