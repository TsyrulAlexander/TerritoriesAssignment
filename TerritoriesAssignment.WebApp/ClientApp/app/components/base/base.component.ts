import {Injectable, OnInit} from '@angular/core';

@Injectable()
export abstract class BaseComponent implements OnInit {
	private static _isEdit: boolean;
	protected keyboardEvent: KeyboardEvent;
	get isEdit(): boolean {
		return BaseComponent._isEdit;
	}
	set isEdit(value: boolean) {
		BaseComponent._isEdit = value;
	}
	ngOnInit(): void {
		this.subscribeMessages();
	}
	subscribeMessages() {

	}
}