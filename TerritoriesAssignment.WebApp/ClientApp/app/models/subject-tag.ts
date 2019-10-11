import {Subscriber} from "rxjs";
export class SubjectTag<T> {
	public tag: string;
	public subscriber: Subscriber<T>;
	public sender: any;
	constructor(tag: string, subscriber: Subscriber<T>, sender: any) {
		this.tag = tag;
		this.subscriber = subscriber;
		this.sender = sender;
	}
}