import {Component, EventEmitter, Input, Output} from '@angular/core';
import {BaseComponent} from "../base/base.component";
import {MatDialogRef} from "@angular/material/dialog";

@Component({
    selector: 'ks-modal',
    templateUrl: './modal.component.html',
    styleUrls: ['./modal.component.css']
})
export class ModalComponent<T> extends BaseComponent {
    constructor(public dialogRef: MatDialogRef<T>) {
        super();
    }
    @Input()close(result: any = null) {
        this.dialogRef.close(result);
        this.onClosed.emit();
    }
    @Output()onClosed = new EventEmitter();
}