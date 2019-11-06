import {Injectable} from "@angular/core";
import * as _ from "underscore";

@Injectable({ providedIn: 'root' })
export class ObjectUtilities {
	static where<T>(array: T[], findObject: any) {
		return _.where(array, findObject);
	}
	static findItem<T>(array: T[], findObject: any): T {
		return _.findWhere(array, findObject);
	}
	static findItemFromPath<T>(array: T[], path: string, object: any): T {
		if (!array) {
			return null;
		}
		for (let i = 0; i < array.length; i++) {
			let value = this.deepFind(array[i], path);
			if (value === object) {
				return array[i];
			}
		}
		return null;
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
	static deepFind(obj: any, path: string) {
		let paths = path.split('.');
		let current = obj;
		for (let i = 0; i < paths.length; ++i) {
			if (current[paths[i]] == undefined) {
				return undefined;
			} else {
				current = current[paths[i]];
			}
		}
		return current;
	}
}