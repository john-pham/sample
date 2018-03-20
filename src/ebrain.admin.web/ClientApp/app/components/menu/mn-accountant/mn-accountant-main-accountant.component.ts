import { Component, OnInit, OnDestroy, TemplateRef, ViewChild } from '@angular/core';
import { fadeInOut } from '../../../services/animations';

@Component({
    selector: 'mn_accountant_main_accountant',
    templateUrl: './mn-accountant-main-accountant.component.html',
    styleUrls: ['./mn-accountant-main-accountant.component.css'],
    animations: [fadeInOut]
})

export class mn_accountant_main_accountantComponent implements OnInit, OnDestroy {
    ngOnDestroy() { }
    ngOnInit() { }
}
