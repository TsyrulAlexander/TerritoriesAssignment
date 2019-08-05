var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
import { Component } from '@angular/core';
import { CountryService } from "../../services/country.service";
var CountryListComponent = /** @class */ (function () {
    function CountryListComponent(countryService) {
        this.countryService = countryService;
    }
    CountryListComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.countryService.getCountries().subscribe(function (data) {
            _this.Countries = data;
        });
    };
    CountryListComponent = __decorate([
        Component({
            selector: "ks-country-list",
            templateUrl: "country-list.component.html",
            styleUrls: ["country-list.component.css"],
            providers: [CountryService]
        }),
        __metadata("design:paramtypes", [CountryService])
    ], CountryListComponent);
    return CountryListComponent;
}());
export { CountryListComponent };
//# sourceMappingURL=country-list.component.js.map