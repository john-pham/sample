// ======================================
// Author: Ebrain Team
// Email:  johnpham@ymail.com
// Copyright (c) 2017 supperbrain.visualstudio.com
// 
// ==> Contact Us: supperbrain@outlook.com
// ======================================

import { Component, OnInit, OnDestroy, TemplateRef, ViewChild } from '@angular/core';
import { Router } from '@angular/router';

import { fadeInOut } from '../../services/animations';
import { AppTranslationService } from "../../services/app-translation.service";
import { GrpsuppliersService } from "../../services/grpsuppliers.service";
import { AlertService, MessageSeverity, DialogType } from '../../services/alert.service';
import { Utilities } from '../../services/utilities';
import { Grpsupplier } from '../../models/Grpsupplier.model';
import { BsModalService } from 'ngx-bootstrap/modal';
import { BsModalRef } from 'ngx-bootstrap/modal/bs-modal-ref.service';

import { IOStockReport } from "../../models/iostockreport.model";
import { PaymentsService } from "../../services/payments.service";
import { Payment } from "../../models/payment.model";
import { AccessRightsService } from "../../share/services/access-rights.service";
import { Page } from "../../models/page.model";
import { Results } from "../../models/results.model";
import { Chart } from "../../models/chart.model";

@Component({
    selector: 'paymentlist',
    templateUrl: './paymentlists.component.html',
    styleUrls: ['./paymentlists.component.css'],
    animations: [fadeInOut]
})

export class PaymentListsComponent implements OnInit, OnDestroy {
    rows = [];
    columns = [];
    loadingIndicator: boolean = true;

    filterName: string;
    filterValue: string;
    fromDate: Date;
    toDate: Date;

    private pointer: Grpsupplier;
    private chart: Chart;

    public changesSavedCallback: () => void;
    public changesFailedCallback: () => void;
    public changesCancelledCallback: () => void;

    modalRef: BsModalRef;
    private page: Page;
    constructor(private alertService: AlertService, private router: Router, private translationService: AppTranslationService, private localService: PaymentsService, public accessRightService: AccessRightsService, private modalService: BsModalService) {
        var date = new Date(), y = date.getFullYear(), m = date.getMonth();
        this.fromDate = new Date(y, m, 1);
        this.toDate = new Date(y, m + 1, 0);
        this.filterValue = '';
        this.page = new Page();
        this.page.pageNumber = 0;
        this.page.size = 20;
        this.chart = new Chart();
    }

    setPage(pageInfo) {
        this.page.pageNumber = pageInfo.offset;
        this.getFromServer();
    }

    ngOnInit() {
        let gT = (key: string) => this.translationService.getTranslation(key);

        this.columns = [
            { headerClass: "text-center", prop: 'paymentTypeName', name: gT('label.paymentlist.PaymentTypeName'), cellTemplate: this.nameTemplate },
            { headerClass: "text-center", prop: 'code', name: gT('label.paymentlist.Code'), cellTemplate: this.nameTemplate },
            { headerClass: "text-center", prop: 'createDate', name: gT('label.paymentlist.CreateDate'), cellTemplate: this.nameTemplate },
            { headerClass: "text-center", prop: 'fullName', name: gT('label.paymentlist.CreateUser'), cellTemplate: this.nameTemplate },
            { headerClass: "text-center", prop: 'totalPrice', name: gT('label.paymentlist.TotalPrice'), cellTemplate: this.totalPriceTemplate, cellClass: 'text-right' },
            { headerClass: "text-center", prop: 'note', name: gT('label.paymentlist.Note'), cellTemplate: this.descriptionTemplate },
            { name: '', width: 80, cellTemplate: this.actionsTemplate, resizeable: false, canAutoResize: false, sortable: false, draggable: false }
        ];

        //
        this.getFromServer();

        //
    }

    ngOnDestroy() {
        //this.saveToDisk();
    }

    goDetails(template: TemplateRef<any>, value: Payment) {
        var url = '';
        if (value != null && value.paymentTypeId == 1) url = '/payment'; else '/paymentvouchers';
        this.router.navigate([url, value.id]);
    }

    onRemoved(file: any) {
        // do some stuff with the removed file.
    }

    onUploadStateChanged(state: boolean) {
        console.log(JSON.stringify(state));
    }

    onSearchChanged(value: string) {
        this.filterValue = value;
        this.getFromServer();
    }

    private getFromServer() {
        this.loadingIndicator = true;
        //
        var disp = this.localService.searchSummarize(this.filterName, this.filterValue, this.fromDate, this.toDate, this.page.pageNumber, this.page.size).subscribe(
            list => this.onDataLoadSuccessful(list),
            error => this.onDataLoadFailed(error));
    }

    private getReport(template: TemplateRef<any>) {
        this.localService.repotSummarize(this.filterName, this.filterValue, this.fromDate, this.toDate, this.page.pageNumber, this.page.size).subscribe(
            resulted => {
                this.chart = resulted;
                this.alertService.stopLoadingMessage();
                this.loadingIndicator = false;
                this.modalRef = this.modalService.show(template, { class: 'modal-lg' });
            },
            error => this.onDataLoadFailed(error));
    }
    

    private onDataLoadSuccessful(resulted: Results<Payment>) {
        this.page.totalElements = resulted.total;
        this.rows = resulted.list;
        this.alertService.stopLoadingMessage();
        this.loadingIndicator = false;
    }

    private onDataLoadFailed(error: any) {
        this.alertService.stopLoadingMessage();
        this.alertService.showStickyMessage("Load Error", `Unable to retrieve user data from the server.\r\nErrors: "${Utilities.getHttpResponseMessage(error)}"`,
            MessageSeverity.error, error);
        this.loadingIndicator = false;
    }

    search() {
        this.getFromServer();
    }

    showChart(template: TemplateRef<any>) {
        this.loadingIndicator = true;
        this.getReport(template);
    }

    close() {
        this.modalRef.hide();
    }

    @ViewChild('statusHeaderTemplate')
    statusHeaderTemplate: TemplateRef<any>;

    @ViewChild('nameTemplate')
    nameTemplate: TemplateRef<any>;

    @ViewChild('descriptionTemplate')
    descriptionTemplate: TemplateRef<any>;

    @ViewChild('actionsTemplate')
    actionsTemplate: TemplateRef<any>;

    @ViewChild('statusTemplate')
    statusTemplate: TemplateRef<any>;

    @ViewChild('totalPriceTemplate')
    totalPriceTemplate: TemplateRef<any>;
}
