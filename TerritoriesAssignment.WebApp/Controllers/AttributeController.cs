using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TerritoriesAssignment.Core;
using TerritoriesAssignment.WebApp.Models;

namespace TerritoriesAssignment.WebApp.Controllers {
	[Route("api/attribute")]
	[ApiController]
	public class AttributeController : Controller {
		public IDataStorage Storage { get; }
		public AttributeController(IDataStorage storage) {
			Storage = storage;
		}
		[HttpGet("getAttributes")]
		public BaseLookupViewItem[] GetAttributes() {
			return Storage.GetAttributes().Select(attribute => new BaseLookupViewItem(attribute)).ToArray();
		}
		[HttpPost("addAttribute")]
		public void AddAttribute([FromBody]AttributeView attribute) {
			Storage.AddAttribute(attribute.Cast());
		}
		[HttpPost("addAttributeValue")]
		public void AddAttributeValue([FromBody]AttributeValueView attributeValue) {
			Storage.AddAttributeValue(attributeValue.Cast());
		}
		[HttpPost("updateAttributeValue")]
		public void UpdateAttributeValue([FromBody]AttributeValueView attributeValue) {
			Storage.UpdateAttributeValue(attributeValue.Cast());
		}
		[HttpGet("getAttributeValues/{regionId?}")]
		public AttributeValueView[] GetAttributeValuesFromRegion(Guid regionId) {
			return Storage.GetAttributeValues(regionId).Select(attribute => new AttributeValueView(attribute)).ToArray();
		}
		[HttpGet("getAttributeValuesFromArea/{areaId?}")]
		public AttributeValueView[] GetAttributeValuesFromArea(Guid areaId) {
			return Storage.GetAttributeValuesFromArea(areaId).Select(attribute => new AttributeValueView(attribute)).ToArray();
		}
		[HttpGet("getAttributeValuesFromCountry/{countryId?}")]
		public AttributeValueView[] GetAttributeValuesFromCountry(Guid countryId) {
			return Storage.GetAttributeValuesFromCountry(countryId).Select(attribute => new AttributeValueView(attribute)).ToArray();
		}
	}
}
