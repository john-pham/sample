// ======================================
// Author: Ebrain Team
// Email:  johnpham@ymail.com
// Copyright (c) 2017 supperbrain.visualstudio.com
// 
// ==> Contact Us: supperbrain@outlook.com
// ======================================

import { Component, OnInit, OnDestroy, TemplateRef, ViewChild, Input, Output, EventEmitter } from '@angular/core';
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
import { AccessRightsService } from "../../services/access-rights.service";
import { Page } from "../../models/page.model";
import { Results } from "../../models/results.model";
import { Chart } from "../../models/chart.model";
import { IOStockDetail } from "../../models/iostockdetail.model";
@Component({
    selector: 'iostudentlistspaydetail',
    templateUrl: './iostudentlistspaydetail.component.html',
    styleUrls: ['./iostudentlistspaydetail.component.css'],
    animations: [fadeInOut]
})

export class IOStudenListPayDetailComponent implements OnInit, OnDestroy {

    @Input() isNotShowPrice: any = true;
    @Input() isNotShowGetAll: any = false;
    @Input() isWaitingClass: any = false;
    @Input() isLearning: any = true;
    @Input() isShowButtonOnGrid: any = false;
    @Input() isShowButtonPaymentOnGrid: any = true;
    @Output() private activeDoubleClick = new EventEmitter<any>();

    rows = [];
    columns = [];
    loadingIndicator: boolean = true;

    filterName: string;
    filterValue: string;
    fromDate: Date;
    toDate: Date;
    ioStockId: string;
    ioStockDetailId: string;
    studentId: string;
    materialId: string;

    private pointer: Grpsupplier;
    private chart: Chart;

    public changesSavedCallback: () => void;
    public changesFailedCallback: () => void;
    public changesCancelledCallback: () => void;

    modalRef: BsModalRef;
    orderClassRef: BsModalRef;

    private page: Page;
    constructor(private alertService: AlertService, private translationService: AppTranslationService,
        public accessRightService: AccessRightsService,
        private localService: IOStudentListService, private modalService: BsModalService, private router: Router) {
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

        this.getFromServer();

        //
    }

    ngOnDestroy() {
        //this.saveToDisk();
    }

    addGrpsupplier(template: TemplateRef<any>) {
        this.modalRef = this.modalService.show(template);
    }
    
    onSearchChanged(value: string) {
        this.filterValue = value;
        this.getFromServer();
    }

    private getFromServer() {
        this.loadingIndicator = true;
        const isShow = this.isNotShowPrice;

        var disp = this.localService.getiopaymentDetail(this.filterName, this.filterValue,
            (this.isNotShowGetAll ? 0 : 1), (this.isWaitingClass ? 1 : 0), false,
            (this.isLearning == false? 0 : 1), "", this.fromDate, this.toDate, this.page.pageNumber, this.page.size).subscribe(
                list => this.onDataLoadSuccessful(list),
                error => this.onDataLoadFailed(error),
                () => {
                    disp.unsubscribe();
                    setTimeout(() => { this.loadingIndicator = false; }, 1500);
                });
    }

    private getReport(template: TemplateRef<any>) {
        this.localService.reportsearch(this.filterName, this.filterValue, this.fromDate, this.toDate, this.page.pageNumber, this.page.size).subscribe(
            resulted => {
                this.chart = resulted;
                this.alertService.stopLoadingMessage();
                this.loadingIndicator = false;
                this.modalRef = this.modalService.show(template, { class: 'modal-lg' });
            },
            error => this.onDataLoadFailed(error));
    }

    private onDataLoadSuccessful(resulted: Results<IOStockReport>) {
        this.page.totalElements = resulted.total;
        this.rows = resulted.list;
        this.alertService.stopLoadingMessage();

    }

    private onDataLoadFailed(error: any) {
        this.alertService.stopLoadingMessage();
        this.alertService.showStickyMessage("Load Error", `Unable to retrieve user data from the server.\r\nErrors: "${Utilities.getHttpResponseMessage(error)}"`,
            MessageSeverity.error, error);

    }

    goPayment(ioid: string) {
        this.router.navigate(['/paymentio', ioid]);
    }

    goDetails(value: IOStockReport) {
        var url = '';
        if (value != null && value.ioTypeId == 1) { url = '/iooutput'; } else { url = '/ioinput'; }
        this.router.navigate([url, value.id]);
    }

    onActivateMaterial(event) {
        if (event.type == 'dblclick') {
            var row = event.row;
            this.activeDoubleClick.emit(row);
        }
    }

    showChart(template: TemplateRef<any>) {
        this.loadingIndicator = true;
        this.getReport(template);
    }

    private editClass(value: IOStockReport, template: TemplateRef<any>) {
        this.ioStockId = value.id;
        this.studentId = value.studentId;
        this.materialId = value.materialId;
        this.ioStockDetailId = value.ioStockDetailId;
        this.orderClassRef = this.modalService.show(template, { class: 'modal-lg' });
    }

    closeClass() {
        this.orderClassRef.hide();
    }

    close() {
        this.modalRef.hide();
    }

    search() {
        this.getFromServer();
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
