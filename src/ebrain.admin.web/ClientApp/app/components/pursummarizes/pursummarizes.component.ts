// ======================================
// Author: Ebrain Team
// Email:  johnpham@ymail.com
// Copyright (c) 2017 supperbrain.visualstudio.com
// 
// ==> Contact Us: supperbrain@outlook.com
// ======================================

import { Component, OnInit, OnDestroy, TemplateRef, ViewChild, Input } from '@angular/core';
import { Router } from '@angular/router';

import { fadeInOut } from '../../services/animations';
import { AppTranslationService } from "../../services/app-translation.service";
import { GrpsuppliersService } from "../../services/grpsuppliers.service";
import { AlertService, MessageSeverity, DialogType } from '../../services/alert.service';
import { Utilities } from '../../services/utilities';
import { Grpsupplier } from '../../models/Grpsupplier.model';
import { BsModalService } from 'ngx-bootstrap/modal';
import { BsModalRef } from 'ngx-bootstrap/modal/bs-modal-ref.service';
import { IOStudentListService } from "../../services/iostudentlists.service";
import { IOStockReport } from "../../models/iostockreport.model";
import { Page } from '../../models/page.model';
import { AccessRightsService } from "../../share/services/access-rights.service";
import { Results } from "../../models/results.model";
import { PurchaseOrderService } from "../../services/purchaseorders.service";
import { PurchaseOrderReport } from "../../models/purchaseorderreport.model";
import { Chart } from "../../models/chart.model";
@Component({
    selector: 'pursummarizes',
    templateUrl: './pursummarizes.component.html',
    styleUrls: ['./pursummarizes.component.css'],
    animations: [fadeInOut]
})

export class PurSummarizesComponent implements OnInit, OnDestroy {
    rows = [];
    columns = [];
    loadingIndicator: boolean = true;

    filterName: string;
    filterValue: string;
    fromDate: Date;
    toDate: Date;
    private page: Page;

    public changesSavedCallback: () => void;
    public changesFailedCallback: () => void;
    public changesCancelledCallback: () => void;
    private chart: Chart;
    private pending: any;
    private processing: any;
    private done: any;

    @Input() isShowSummarized: any = false;

    modalRef: BsModalRef;

    isAllIO: boolean = false;
    @Input()
    set IsAllIO(isMes: boolean) {
        this.isAllIO = (isMes) || null;
    }

    get IsAllIO() {
        return this.isAllIO;
    }

    constructor(private alertService: AlertService, private router: Router, private translationService: AppTranslationService,
        private localService: PurchaseOrderService, public accessRightService: AccessRightsService, private modalService: BsModalService) {
        var date = new Date(), y = date.getFullYear(), m = date.getMonth();
        this.fromDate = new Date(y, m, 1);
        this.toDate = new Date(y, m + 1, 0);
        this.filterValue = "";
        this.page = new Page();
        this.page.pageNumber = 0;
        this.page.size = 20;
    }

    setPage(pageInfo) {
        this.page.pageNumber = pageInfo.offset;
        this.search();
    }

    ngOnInit() {
        let gT = (key: string) => this.translationService.getTranslation(key);

        this.columns = [
            { headerClass: "text-center", prop: 'code', name: gT('label.purchaselist.Code'), cellTemplate: this.nameTemplate },
            { headerClass: "text-center", prop: 'createDate', name: gT('label.purchaselist.CreateDate'), cellTemplate: this.nameTemplate },
            { headerClass: "text-center", prop: 'fullName', name: gT('label.purchaselist.CreateUser'), cellTemplate: this.nameTemplate },
            { headerClass: "text-center", prop: 'branchNameIO', name: gT('label.purchaselist.BranchNameIO'), cellTemplate: this.nameTemplate },
            { headerClass: "text-center", prop: 'branchName', name: gT('label.purchaselist.BranchName'), cellTemplate: this.nameTemplate },
            { headerClass: "text-center", prop: 'purchaseQuantity', name: gT('label.purchaselist.PurchaseQuantity'), cellTemplate: this.totalPriceTemplate, cellClass: 'text-right' },
            { headerClass: "text-center", prop: 'ioQuantity', name: gT('label.purchaselist.IOQuantity'), cellTemplate: this.totalPriceTemplate, cellClass: 'text-right' },
            { headerClass: "text-center", prop: 'remainQuantity', name: gT('label.purchaselist.RemainQuantity'), cellTemplate: this.totalPriceTemplate, cellClass: 'text-right' },
            { name: '', width: 80, cellTemplate: this.actionsTemplate, resizeable: false, canAutoResize: false, sortable: false, draggable: false }
        ];

        //
        this.getFromServer();

        //
    }

    ngOnDestroy() {
        //this.saveToDisk();
    }

    goDetails(template: TemplateRef<any>, value: PurchaseOrderReport) {
        var url = '/purchaseorders';
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
        var disp = this.localService.getpurchaseorders(this.filterName, this.filterValue, this.fromDate, this.toDate, 0, this.page.pageNumber, this.page.size).subscribe(
            list => this.onDataLoadSuccessful(list),
            error => this.onDataLoadFailed(error),
            () => {
                disp.unsubscribe();
                setTimeout(() => { this.loadingIndicator = false; }, 1500);
            });
    }

    private getReport(template: TemplateRef<any>) {
        this.localService.reportpurchaseorders(this.filterName, this.filterValue, this.fromDate, this.toDate, 0, this.page.pageNumber, this.page.size).subscribe(
            resulted => {
                this.chart = resulted;
                this.alertService.stopLoadingMessage();
                this.loadingIndicator = false;
                this.modalRef = this.modalService.show(template, { class: 'modal-lg' });
            },
            error => this.onDataLoadFailed(error));
    }

    private onDataLoadSuccessful(resulted: Results<PurchaseOrderReport>) {
        this.page.totalElements = resulted.total;
        this.rows = resulted.list;
        if (this.rows != null && this.rows.length > 0) {
            this.done =  this.rows
                .map(c => c.remainQuantity)
                .reduce((sum, current) => sum + current);

            this.processing = this.rows
                .map(c => c.ioQuantity)
                .reduce((sum, current) => sum + current);

            this.pending = this.rows
                .map(c => c.purchaseQuantity)
                .reduce((sum, current) => sum + current);
        }
        this.alertService.stopLoadingMessage();

    }

    private onDataLoadFailed(error: any) {
        this.alertService.stopLoadingMessage();
        this.alertService.showStickyMessage("Load Error", `Unable to retrieve user data from the server.\r\nErrors: "${Utilities.getHttpResponseMessage(error)}"`,
            MessageSeverity.error, error);

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
