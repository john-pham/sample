import { Component, OnInit, OnDestroy, TemplateRef, ViewChild } from '@angular/core';
import { fadeInOut } from '../../../services/animations';

@Component({
    selector: 'mn_accountant_payment_accountant',
    templateUrl: './mn-accountant-payment-accountant.component.html',
    styleUrls: ['./mn-accountant-payment-accountant.component.css'],
    animations: [fadeInOut]
})

export class mn_accountant_payment_accountantComponent implements OnInit, OnDestroy {
    ngOnDestroy() { }
    ngOnInit() { }
}
