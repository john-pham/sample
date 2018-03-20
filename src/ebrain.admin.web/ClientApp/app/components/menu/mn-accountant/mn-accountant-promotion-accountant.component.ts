import { Component, OnInit, OnDestroy, TemplateRef, ViewChild } from '@angular/core';
import { fadeInOut } from '../../../services/animations';

@Component({
    selector: 'mn_accountant_promotion_accountant',
    templateUrl: './mn-accountant-promotion-accountant.component.html',
    styleUrls: ['./mn-accountant-promotion-accountant.component.css'],
    animations: [fadeInOut]
})

export class mn_accountant_promotion_accountantComponent implements OnInit, OnDestroy {
    ngOnDestroy() { }
    ngOnInit() { }
}
