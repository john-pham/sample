// ======================================
// Author: Ebrain Team
// Email:  johnpham@ymail.com
// Copyright (c) 2017 supperbrain.visualstudio.com
// 
// ==> Contact Us: supperbrain@outlook.com
// ======================================

import { Component, ViewChild, ElementRef, Input, Output, EventEmitter } from '@angular/core';

@Component({
    selector: 'search-chart',
    templateUrl: './search-chart.component.html',
    styleUrls: ['./search-chart.component.css']
})
export class SearChartComponent {

    @Input() isShowTemplate: any;

    @Output() private searchTemplate = new EventEmitter<any>();
    @Output() private showTemplate = new EventEmitter<any>();

    private search() {
        setTimeout(() => this.searchTemplate.emit(""));
    }

    private showChart() {
        setTimeout(() => this.showTemplate.emit(""));
    }

}