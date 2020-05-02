import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
    selector: 'ks-log-in',
    templateUrl: './log-in.component.html',
    styleUrls: ['./log-in.component.css']
})
/** log-in component*/
export class LogInComponent {
    /** log-in ctor */
    private rememberMe: boolean;
    private login: string;
    private password: string
    constructor(private router: Router) {
                
    }
    userLogin(): void {
        this.router.navigate(['/territory']);
    }
}