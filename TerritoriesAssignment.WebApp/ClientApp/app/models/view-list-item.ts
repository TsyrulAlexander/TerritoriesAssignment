export class ViewListItem<T> {
	public isVisible: boolean = true;
	public isSelected: boolean = false;
	constructor(public item: T) {}
}