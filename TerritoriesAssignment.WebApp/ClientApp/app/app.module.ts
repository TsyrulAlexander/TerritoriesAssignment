import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { Routes, RouterModule } from '@angular/router';
import { AppComponent } from "./components/app/app.component";
import { CountryListComponent } from "./components/country-list/country-list.component";
import { AreaListComponent } from "./components/area-list/area-list.component";
import { RegionListComponent } from "./components/region-list/region-list.component";
import { MapComponent } from "./components/map/map.component";
import { CountryComponent } from "./components/country/country.component";
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { AreaComponent } from "./components/area/area.component";
import { RegionComponent } from "./components/region/region.component";


const appRoutes: Routes = [
 	{ path: '', component: MapComponent }
];

@NgModule({
	imports: [BrowserModule, FormsModule, RouterModule.forRoot(appRoutes), HttpClientModule, NgbModule],
	declarations: [AppComponent, MapComponent, CountryListComponent, AreaListComponent, RegionListComponent,
		CountryComponent, AreaComponent, RegionComponent],
	bootstrap: [AppComponent]
})
export class AppModule { }