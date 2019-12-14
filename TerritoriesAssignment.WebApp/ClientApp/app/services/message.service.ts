import { Injectable } from '@angular/core';
import {SubjectTag} from "../models/subject-tag";
import {ObjectUtilities} from "../utilities/object-utilities";


@Injectable({ providedIn: 'root' })
export class MessageService {
    private subjects: SubjectTag<any>[]  = [];
    sendMessages(body: any, tag: string) {
        let subjects = this.getSubjects(tag);
        subjects.forEach(value => MessageService.callSubscriber(value, body));
    }
    sendMessage(body: any, tag: string) {
        let subject = this.getSubjects(tag)[0];
        return subject && MessageService.callSubscriber(subject, body);
    }
    static callSubscriber(subject: SubjectTag<any>, body: any): any {
        return subject.subscriber.apply(subject.sender, [body]);
    }
    getSubjects(tag: string): SubjectTag<any>[] {
        return ObjectUtilities.where(this.subjects, {tag: tag});
    }
    subscribe(sender: any, next: (value: any) => void, tag: string): void {
        this.subjects.push(new SubjectTag(tag, next, sender));
    }
    unSubscribe(sender: any, tag: string): void {
        this.subjects.forEach(function (subject, index) {
            if (subject.tag === tag && subject.sender === sender) {
               this.subjects.splice(index, 1);
            }
        }, this);
    }
}