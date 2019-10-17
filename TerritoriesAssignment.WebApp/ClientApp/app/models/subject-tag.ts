export class SubjectTag<T> {
	public tag: string;
	public subscriber: (body: any) => void;
	public sender: any;
	constructor(tag: string, subscriber: (body: any) => void, sender: any) {
		this.tag = tag;
		this.subscriber = subscriber;
		this.sender = sender;
	}
}