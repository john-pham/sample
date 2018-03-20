import { Component, OnInit, OnDestroy, TemplateRef, ViewChild } from '@angular/core';
import { fadeInOut } from '../../../services/animations';

@Component({
    selector: 'mn_stock_manages_stock',
    templateUrl: './mn-stock-manages-stock.component.html',
    styleUrls: ['./mn-stock-manages-stock.component.css'],
    animations: [fadeInOut]
})

export class mn_stock_manages_stockComponent implements OnInit, OnDestroy {
    ngOnDestroy() { }
    ngOnInit() { }
}
