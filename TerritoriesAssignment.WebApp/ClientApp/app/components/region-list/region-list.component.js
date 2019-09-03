var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
import { Component, Input } from '@angular/core';
import { RegionService } from "../../services/region.service";
import { Area } from "../../models/area";
var RegionListComponent = /** @class */ (function () {
    function RegionListComponent(regionService) {
        this.regionService = regionService;
        this._isShow = false;
    }
    Object.defineProperty(RegionListComponent.prototype, "isShow", {
        get: function () {
            return this._isShow;
        },
        set: function (value) {
            this._isShow = value;
            if (value) {
                this.initRegions();
            }
        },
        enumerable: true,
        configurable: true
    });
    RegionListComponent.prototype.initRegions = function () {
        var _this = this;
        if (!this.area) {
            return;
        }
        this.regionService.getRegions(this.area).subscribe(function (data) {
            _this.regions = data;
        });
    };
    __decorate([
        Input(),
        __metadata("design:type", Area)
    ], RegionListComponent.prototype, "area", void 0);
    __decorate([
        Input(),
        __metadata("design:type", Boolean),
        __metadata("design:paramtypes", [Boolean])
    ], RegionListComponent.prototype, "isShow", null);
    RegionListComponent = __decorate([
        Component({
            selector: 'ks-region-list',
            templateUrl: 'region-list.component.html',
            styleUrls: ['region-list.component.css'],
            providers: [RegionService]
        }),
        __metadata("design:paramtypes", [RegionService])
    ], RegionListComponent);
    return RegionListComponent;
}());
export { RegionListComponent };
//# sourceMappingURL=region-list.component.js.map