import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { Routes, RouterModule } from '@angular/router';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { HomeComponent } from "../components/home/home.component";
import { NavbarComponent } from "../components/navbar/navbar.component";
import { SettingsComponent } from "../components/settings/settings.component";
import { LogInComponent } from "../components/log-in/log-in.component";
import { AppComponent } from "../components/app/app.component";
import { CountryListComponent } from "../components/country-list/country-list.component";
import { AreaListComponent } from "../components/area-list/area-list.component";
import { RegionListComponent } from "../components/region-list/region-list.component";
import { CountryComponent } from "../components/country/country.component";
import { AreaComponent } from "../components/area/area.component";
import { RegionComponent } from "../components/region/region.component";
import { MapComponent } from "../components/map/map.component";
import {ModalComponent} from "../components/modal/modal.component";
import {MessageService} from "../services/message.service";
import {AddCountryComponent} from "../components/add-country/add-country.component";
import {VarDirective} from "../directives/VarDirective";
import {OverlayModule} from '@angular/cdk/overlay';
import {MatDialogModule} from '@angular/material/dialog';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { JwBootstrapSwitchNg2Module } from 'jw-bootstrap-switch-ng2';
import {AddAreaComponent} from "../components/add-area/add-area.component";
import {ListItemViewComponent} from "../controls/list-item-view/list-item-view.component";
import {ItemInfoComponent} from "../components/item-info/item-info.component";
import {AddRegionComponent} from "../components/add-region/add-region.component";
import {ManagerDistributionComponent} from "../components/manager-distribution/manager-distribution.component";
import { ColorPickerModule } from 'ngx-color-picker';
import {ManagerDistributionResultComponent} from "../components/manager-distribution-result/manager-distribution-result.component";

const appRoutes: Routes = [
    { path: '', component: HomeComponent },
    { path: 'territory', component: HomeComponent },
    { path: 'settings', component: SettingsComponent },
    { path: 'logout', component: LogInComponent },
    { path: '**', redirectTo: '/' }
];

@NgModule({
	imports: [BrowserModule, FormsModule, RouterModule.forRoot(appRoutes), HttpClientModule, OverlayModule, NgbModule,
		MatDialogModule, BrowserAnimationsModule, JwBootstrapSwitchNg2Module, ColorPickerModule],
    declarations: [AppComponent, HomeComponent, NavbarComponent, LogInComponent, SettingsComponent, CountryListComponent, AreaListComponent, RegionListComponent,
        CountryComponent, AreaComponent, RegionComponent, MapComponent, ModalComponent, AddCountryComponent,
        VarDirective, AddAreaComponent, ListItemViewComponent, ItemInfoComponent, AddRegionComponent,
		ManagerDistributionComponent, ManagerDistributionResultComponent],
	entryComponents: [AddCountryComponent, AddAreaComponent, AddRegionComponent, ManagerDistributionComponent,
		ManagerDistributionResultComponent],
    providers:[MessageService],
    bootstrap: [AppComponent]
})
export class AppModule { }