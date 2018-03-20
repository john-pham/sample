import { Component, OnInit, OnDestroy, TemplateRef, ViewChild } from '@angular/core';
import { fadeInOut } from '../../../services/animations';

@Component({
    selector: 'mn_stock_reports_stock',
    templateUrl: './mn-stock-reports-stock.component.html',
    styleUrls: ['./mn-stock-reports-stock.component.css'],
    animations: [fadeInOut]
})

export class mn_stock_reports_stockComponent implements OnInit, OnDestroy {
    ngOnDestroy() { }
    ngOnInit() { }
}
