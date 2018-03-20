import { Component, OnInit, OnDestroy, TemplateRef, ViewChild } from '@angular/core';
import { fadeInOut } from '../../../services/animations';

@Component({
    selector: 'mn_stock_inputs_stock',
    templateUrl: './mn-stock-inputs-stock.component.html',
    styleUrls: ['./mn-stock-inputs-stock.component.css'],
    animations: [fadeInOut]
})

export class mn_stock_inputs_stockComponent implements OnInit, OnDestroy {
    ngOnDestroy() { }
    ngOnInit() { }
}
