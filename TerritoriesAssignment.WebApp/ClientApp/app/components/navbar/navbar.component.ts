import { Component } from '@angular/core';
import { Navbar } from "../../models/navbar"

@Component({
    selector: 'ks-navbar',
    templateUrl: './navbar.component.html',
    styleUrls: ['./navbar.component.scss']
})
/** navbar component*/
export class NavbarComponent {
    /** navbar ctor */
    public navbarItems: Navbar[] = [
            new Navbar("Профіль", "/profile"),
            new Navbar("Території", "/territory"),
            new Navbar("Налаштування", "/settings"),
            new Navbar("Вийти", "/logout")];
    
}