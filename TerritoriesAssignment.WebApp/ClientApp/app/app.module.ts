import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { HomeComponent } from "./components/home/home.component";
import { HttpClientModule } from '@angular/common/http';
import { Routes, RouterModule } from '@angular/router';
import { AppComponent } from "./components/app/app.component";
import { CountryComponent } from "./components/country/country.component";

const appRoutes: Routes = [
    { path: '', component: HomeComponent }
];

@NgModule({
    imports: [BrowserModule, FormsModule, RouterModule.forRoot(appRoutes), HttpClientModule],
    declarations: [AppComponent, HomeComponent, CountryComponent],
    bootstrap: [AppComponent]
})
export class AppModule { }