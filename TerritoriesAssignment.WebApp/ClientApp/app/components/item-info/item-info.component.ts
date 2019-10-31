import {Component, OnInit} from "@angular/core";
import {BaseComponent} from "../base/base.component";
import {MessageService} from "../../services/message.service";
import {ListItemSelected} from "../../models/list-item-selected";
import {BaseLookup} from "../../models/base-lookup";
import {ListItemType} from "../../models/listItemType";
import {AttributeService} from "../../services/attribute.service";
import {AttributeValue} from "../../models/attribute-value";
import {Attribute} from "../../models/attribute";
import {ViewListItem} from "../../models/view-list-item";
import {Guid} from "guid-typescript";
import {ObjectUtilities} from "../../utilities/object-utilities";

@Component({
	selector: "ks-item-info",
	templateUrl: "./item-info.component.html",
	providers: [AttributeService]
})
export class ItemInfoComponent extends BaseComponent implements OnInit {
	_attributeValues: AttributeValue[];
	item: BaseLookup;
	itemType: ListItemType;
	isAddAttributeValue: boolean;
	get attributeValues(): AttributeValue[] {
		return this._attributeValues;
	}
	set attributeValues(value) {
		this._attributeValues = value;
		this.attributes.forEach(attribute => {
			attribute.isVisible = !value.find((attributeValue) => {
				return attributeValue.attribute.id === attribute.item.id;
			});
		}, this);
	};
	attributes: ViewListItem<Attribute>[];
	constructor(private messageService: MessageService, private attributeService: AttributeService) {
		super();
	}
	ngOnInit(): void {
		super.ngOnInit();
		this.attributeService.getAttributes().subscribe(attributes => {
			this.attributes = attributes.map(value => new ViewListItem<Attribute>(value));
		})
	}

	addAttributeToList(value: Guid) {
		let attribute = ObjectUtilities.findItemFromPath(this.attributes, "item.id", value);
		let attributeValue = new AttributeValue(Guid.create());
		attributeValue.attribute = attribute.item;
		attributeValue.region = this.item;
		this.attributeService.createAttributeValue(attributeValue).subscribe(() => {
			this.attributeValues.push(attributeValue);
			attribute.isVisible = false;
		});
	}
	attributeValueChange(attributeValue: AttributeValue) {
		this.updateAttributeValue(attributeValue);
	}
	updateAttributeValue(attributeValue: AttributeValue) {
		this.attributeService.updateAttributeValue(attributeValue).subscribe(value => {}, error => {
			console.error(error);
		})
	}

	subscribeMessages() {
		super.subscribeMessages();
		this.messageService.subscribe(this, this.onListItemSelected, "ListItemSelected");
	}
	onListItemSelected(info: ListItemSelected) {
		if (this.item && this.item.id === info.item.id) {
			return;
		}
		this.item = info.item;
		this.itemType = info.itemType;
		this.isAddAttributeValue = false;
		if (this.itemType === ListItemType.Country) {
			this.attributeService.getAttributeValuesFromCountry(this.item.id).subscribe(value => {
				this.attributeValues = value;
			});
		}
		if (this.itemType === ListItemType.Area) {
			this.attributeService.getAttributeValuesFromArea(this.item.id).subscribe(value => {
				this.attributeValues = value;
			});
		}
		if (this.itemType === ListItemType.Region) {
			this.attributeService.getAttributeValues(this.item.id).subscribe(value => {
				this.attributeValues = value;
				this.isAddAttributeValue = true;
			});
		}
	}
}