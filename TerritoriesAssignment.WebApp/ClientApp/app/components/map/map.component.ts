import {Component, ElementRef, OnInit, ViewChild, AfterViewInit} from '@angular/core';
import {MapItem} from "../../models/map-item";
import {MessageService} from "../../services/message.service";
import {BaseComponent} from "../base/base.component";
import {CountryService} from "../../services/country.service";
import {ObjectUtilities} from "../../utilities/object-utilities";
import {ListItemSelected} from "../../models/list-item-selected";
import {ListItemType} from "../../models/listItemType";
import {AreaService} from "../../services/area.service";

@Component({
    selector: 'ks-map',
    templateUrl: './map.component.html',
    styleUrls: ['./map.component.css'],
    providers: [CountryService, AreaService]
})
export class MapComponent extends BaseComponent implements OnInit, AfterViewInit {
    text: string;
    mapItems: MapItem[] = [];
    @ViewChild('canvas', { static: true })
    canvas: ElementRef<HTMLCanvasElement>;
    context: CanvasRenderingContext2D;
    constructor(private messageService: MessageService, private countryService: CountryService, private areaService: AreaService) {
        super();
    }
    ngAfterViewInit(): void {
        this.context = (<HTMLCanvasElement>this.canvas.nativeElement).getContext('2d');
    }
    draw(): void {
        this.clear();
        this.mapItems.forEach(this.drawMapItem, this);
    }
    clear() {
        this.context.save();
        this.context.setTransform(1, 0, 0, 1, 0, 0);
        this.context.clearRect(0, 0, this.canvas.nativeElement.width, this.canvas.nativeElement.height);
        this.context.restore();
    }
    drawMapItem(item: MapItem) {
        if (!item.points || item.points.length === 0) {
            return;
        }
        let firstPoint = item.points[0];
        this.context.beginPath();
        this.context.moveTo(firstPoint.x, firstPoint.y);
        item.points.forEach((point)=>{
            this.context.lineTo(point.x, point.y);
        }, this);
        this.context.closePath();
        //this.context.lineTo(firstPoint.x, firstPoint.y);
        this.context.stroke();
    }
    subscribeMessages() {
        super.subscribeMessages();
        this.messageService.subscribe(this, this.onListItemSelected, "ListItemSelected");
        this.messageService.subscribe(this, this.onListItemUnSelected, "ListItemUnSelected");
    }
    onListItemUnSelected (body: ListItemSelected) {
        let item = body.item;
        let mapItem = ObjectUtilities.findWhere(this.mapItems, {id: item.id});
        ObjectUtilities.delete(this.mapItems, mapItem);
        this.draw();
    }
    onListItemSelected(body: ListItemSelected) {
        let item = body.item;
        let itemType = body.itemType;
        if (itemType == ListItemType.Country) {
            this.countryService.getCountry(item.id).subscribe(country => {
                this.mapItems.push(country);
                this.draw();
            })
        }
        if (itemType == ListItemType.Area) {
            this.areaService.getArea(item.id).subscribe(area => {
                this.mapItems.push(area);
                this.draw();
            })
        }

    }
}