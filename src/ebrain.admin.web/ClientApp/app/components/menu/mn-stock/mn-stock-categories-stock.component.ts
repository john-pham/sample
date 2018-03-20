import { Component, OnInit, OnDestroy, TemplateRef, ViewChild } from '@angular/core';
import { fadeInOut } from '../../../services/animations';

@Component({
    selector: 'mn_stock_categories_stock',
    templateUrl: './mn-stock-categories-stock.component.html',
    styleUrls: ['./mn-stock-categories-stock.component.css'],
    animations: [fadeInOut]
})

export class mn_stock_categories_stockComponent implements OnInit, OnDestroy {
    ngOnDestroy() { }
    ngOnInit() { }
}
