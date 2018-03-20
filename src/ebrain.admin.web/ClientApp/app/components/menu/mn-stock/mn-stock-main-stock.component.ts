import { Component, OnInit, OnDestroy, TemplateRef, ViewChild } from '@angular/core';
import { fadeInOut } from '../../../services/animations';

@Component({
    selector: 'mn_stock_main_stock',
    templateUrl: './mn-stock-main-stock.component.html',
    styleUrls: ['./mn-stock-main-stock.component.css'],
    animations: [fadeInOut]
})

export class mn_stock_main_stockComponent implements OnInit, OnDestroy {
    ngOnDestroy() { }
    ngOnInit() { }
}
