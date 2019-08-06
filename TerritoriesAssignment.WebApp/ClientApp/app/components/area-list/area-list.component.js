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
import { Country } from "../../models/country";
import { AreaService } from "../../services/area.service";
var AreaListComponent = /** @class */ (function () {
    function AreaListComponent(areaService) {
        this.areaService = areaService;
        this._isShow = false;
    }
    Object.defineProperty(AreaListComponent.prototype, "isShow", {
        get: function () {
            return this._isShow;
        },
        set: function (value) {
            this._isShow = value;
            if (value) {
                this.initAreas();
            }
        },
        enumerable: true,
        configurable: true
    });
    AreaListComponent.prototype.initAreas = function () {
        var _this = this;
        if (this.areas) {
            return this.areas;
        }
        this.areaService.getAreas(this.country).subscribe(function (data) {
            _this.areas = data;
        });
    };
    __decorate([
        Input(),
        __metadata("design:type", Country)
    ], AreaListComponent.prototype, "country", void 0);
    __decorate([
        Input(),
        __metadata("design:type", Boolean),
        __metadata("design:paramtypes", [Boolean])
    ], AreaListComponent.prototype, "isShow", null);
    AreaListComponent = __decorate([
        Component({
            selector: 'ks-area-list',
            templateUrl: 'area-list.component.html',
            styleUrls: ['area-list.component.css'],
            providers: [AreaService]
        }),
        __metadata("design:paramtypes", [AreaService])
    ], AreaListComponent);
    return AreaListComponent;
}());
export { AreaListComponent };
//# sourceMappingURL=area-list.component.js.map