import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import {SubjectTag} from "../models/subject-tag";

@Injectable({ providedIn: 'root' })
export class MessageService {
    private subjects: SubjectTag<any>[]  = [];

    sendMessages(body: any, tag: string) {
        this.subjects.forEach(subject => {
            if (subject.tag == tag) {
                subject.subscriber.next(body);
            }
        }, this);
    }
    sendMessage(body: any, tag: string) {
        let result: any = null;
        this.subjects.every(((subject: any) => {
            if (subject.tag === tag) {
                result = subject.subscriber.next(body);
                return false;
            }
            return true;
        }) as any, this);
        return result;
    }

    subscribe(sender: any, next: (value: any) => void, tag: string): void {
        var subscribers = new Observable((observer) => {
            this.subjects.push(new SubjectTag(tag, observer, sender));
        }).subscribe(next);
    }

    unSubscribe(sender: any, tag: string): void {
        this.subjects.forEach(function (subject) {
            if (subject.tag === tag && subject.sender === sender) {
                subject.subscriber.complete();
            }
        }, this);
    }
}