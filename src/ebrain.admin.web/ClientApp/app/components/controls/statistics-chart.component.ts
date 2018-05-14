// ======================================
// Author: Ebrain Team
// Email:  johnpham@ymail.com
// Copyright (c) 2017 supperbrain.visualstudio.com
// 
// ==> Contact Us: supperbrain@outlook.com
// ======================================

import { Component, OnInit, OnDestroy, Input } from '@angular/core';
import { AlertService, DialogType, AlertMessage, MessageSeverity } from '../../services/alert.service';
import { BsModalRef } from "ngx-bootstrap/modal";
import { Chart } from "../../models/chart.model";
require('chart.js');



@Component({
    selector: 'statistics-chart',
    templateUrl: './statistics-chart.component.html',
    styleUrls: ['./statistics-chart.component.css']
})
export class StatisticsChartComponent implements OnInit, OnDestroy {
    @Input() private modalRef: BsModalRef;
    @Input() private headerRef: string;
    @Input() private chart: Chart;

    chartData: Array<any> = [
        { data: [65, 59, 80, 81, 56, 55], label: 'Series A' },
        { data: [28, 48, 40, 19, 86, 27], label: 'Series B' },
        { data: [18, 48, 77, 9, 100, 27], label: 'Series C' }
    ];
    chartLabels: Array<any> = ['January', 'February', 'March', 'April', 'May', 'June'];

    chartOptions: any = {
        responsive: true,
        title: {
            display: false,
            fontSize: 16,
            text: 'Important Stuff'
        }
    };
    chartColors: Array<any> = [
        {

            backgroundColor: 'rgba(255, 0, 0, 0.2)',
            borderColor: 'rgba(255,153,51,1)',
            pointBackgroundColor: 'rgba(0,128,0,1)',
            pointBorderColor: '#fff',
            pointHoverBackgroundColor: '#fff',
            pointHoverBorderColor: 'rgba(148,159,177,0.8)',
            pointRadius: 8,
        },
        {

            backgroundColor: 'rgba(0, 0, 255, 0.4)',
            borderColor: 'rgba(77,83,96,1)',
            pointBackgroundColor: 'rgba(77,83,96,1)',
            pointBorderColor: '#fff',
            pointHoverBackgroundColor: '#fff',
            pointHoverBorderColor: 'rgba(77,83,96,1)',
            lineTension: 0,
            pointRadius: 10,
            pointStyle: 'rectRounded'
        },
        {
            backgroundColor: 'green',
            borderColor: 'rgba(0,128,0,1)',
            pointBackgroundColor: 'rgba(148,159,177,1)',
            pointBorderColor: '#fff',
            pointHoverBackgroundColor: '#fff',
            pointHoverBorderColor: 'rgba(148,159,177,0.8)',
            pointStyle: 'rect',
            pointRadius: 6,

        },

        {

            backgroundColor: 'rgba(255, 255, 0, 0.2)',
            borderColor: 'rgba(0,0,255,1)',
            pointBackgroundColor: 'rgba(0,0,255,1)',
            pointBorderColor: '#fff',
            pointHoverBackgroundColor: '#fff',
            pointHoverBorderColor: 'rgba(148,159,177,0.8)',
            pointStyle: 'triangle',
            pointRadius: 10,
        }  
    ];
    chartLegend: boolean = true;
    chartType: string = 'line';

    timerReference: any;



    constructor(private alertService: AlertService) {
    }


    ngOnInit() {
        //this.timerReference = setInterval(() => this.randomize(), 5000);

        this.chartData = this.chart.chartModels;
        this.chartLabels = this.chart.chartLabels;
    }

    ngOnDestroy() {
        clearInterval(this.timerReference);
    }



    randomize(): void {
        let _chartData: Array<any> = new Array(this.chartData.length);
        for (let i = 0; i < this.chartData.length; i++) {
            _chartData[i] = { data: new Array(this.chartData[i].data.length), label: this.chartData[i].label };

            for (let j = 0; j < this.chartData[i].data.length; j++) {
                _chartData[i].data[j] = Math.floor((Math.random() * 100) + 1);
            }
        }

        this.chartData = _chartData;
    }


    changeChartType(type: string) {
        this.chartType = type;
    }

    showMessage(msg: string): void {
        this.alertService.showMessage("Demo", msg, MessageSeverity.info);
    }

    showDialog(msg: string): void {
        this.alertService.showDialog(msg, DialogType.prompt, (val) => this.configure(true, val), () => this.configure(false));
    }

    configure(response: boolean, value?: string) {

        if (response) {

            this.alertService.showStickyMessage("Simulating...", "", MessageSeverity.wait);

            setTimeout(() => {

                this.alertService.resetStickyMessage();
                this.alertService.showMessage("Demo", `Your settings was successfully configured to \"${value}\"`, MessageSeverity.success);
            }, 2000);
        }
        else {
            this.alertService.showMessage("Demo", "Operation cancelled by user", MessageSeverity.default);
        }
    }



    // events
    chartClicked(e: any): void {
        console.log(e);
    }

    chartHovered(e: any): void {
        console.log(e);
    }

    closeChart() {
        this.modalRef.hide();
    }
}