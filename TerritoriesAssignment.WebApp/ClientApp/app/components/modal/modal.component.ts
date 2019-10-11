import {Component, EventEmitter, Input, Output} from '@angular/core';

@Component({
    selector: 'ks-modal',
    templateUrl: './modal.component.html',
    styleUrls: ['./modal.component.css']
})
export class ModalComponent {
    @Input() name: string;
    isShowing: boolean = false;
    @Input()show() {
        this.isShowing = true;
    }
    @Input()close() {
        this.isShowing = false;
    }
    @Output()onClosed = new EventEmitter();
}