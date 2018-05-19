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
    @Input() isShowSearch: any ;
    @Input() isShowAddNew: any;
    @Input() isShowExport: any = true;

    @Output() private searchTemplate = new EventEmitter<any>();
    @Output() private showTemplate = new EventEmitter<any>();
    @Output() private addNewTemplate = new EventEmitter<any>();
    @Output() private exportPdfTemplate = new EventEmitter<any>();
    @Output() private exportExcelTemplate = new EventEmitter<any>();

    private search() {
        setTimeout(() => this.searchTemplate.emit(""));
    }

    private showChart() {
        setTimeout(() => this.showTemplate.emit(""));
    }

    private showNew() {
        setTimeout(() => this.addNewTemplate.emit(""));
    }

    private exportPdf() {
        setTimeout(() => this.exportPdfTemplate.emit(""));
    }

    private exportExcel() {
        setTimeout(() => this.exportExcelTemplate.emit(""));
    }
}