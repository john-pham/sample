// ======================================
// Author: Ebrain Team
// Email:  johnpham@ymail.com
// Copyright (c) 2017 supperbrain.visualstudio.com
// 
// ==> Contact Us: supperbrain@outlook.com
// ======================================

import { Component, OnInit, OnDestroy, TemplateRef, ViewChild } from '@angular/core';

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
@Component({
    selector: 'iodetails',
    templateUrl: './iodetails.component.html',
    styleUrls: ['./iodetails.component.css'],
    animations: [fadeInOut]
})

export class IODetailsComponent implements OnInit, OnDestroy {
    rows = [];
    columns = [];
    loadingIndicator: boolean = true;

    filterName: string = "";
    filterValue: string = "";
    fromDate: Date;
    toDate: Date;

    private pointer: Grpsupplier;
    private chart: Chart;

    public changesSavedCallback: () => void;
    public changesFailedCallback: () => void;
    public changesCancelledCallback: () => void;

    modalRef: BsModalRef;
    private page: Page;
    constructor(private alertService: AlertService, private translationService: AppTranslationService, private localService: IOStudentListService, public accessRightService: AccessRightsService, private modalService: BsModalService) {
        this.pointer = new Grpsupplier();
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

    onSearchChanged(value: string) {
        this.filterValue = value;
        this.search();
    }

    ngOnInit() {
        let gT = (key: string) => this.translationService.getTranslation(key);

        this.columns = [
            { headerClass: "text-center", prop: 'code', name: gT('label.iostudentlist.Code'), cellTemplate: this.nameTemplate },
            { headerClass: "text-center", prop: 'createDate', name: gT('label.iostudentlist.CreateDate'), cellTemplate: this.nameTemplate },
            { headerClass: "text-center", prop: 'fullName', name: gT('label.iostudentlist.CreateUser'), cellTemplate: this.nameTemplate },
            { headerClass: "text-center", prop: 'studentName', name: gT('label.iostudentlist.Student'), cellTemplate: this.nameTemplate },
            { headerClass: "text-center", prop: 'materialCode', name: gT('label.iostudentlist.MaterialCode'), cellTemplate: this.nameTemplate },
            { headerClass: "text-center", prop: 'materialName', name: gT('label.iostudentlist.MaterialName'), cellTemplate: this.nameTemplate },
            { headerClass: "text-center", prop: 'quantity', name: gT('label.iostudentlist.Quantity'), cellTemplate: this.totalPriceTemplate, cellClass: 'text-right'},
            { headerClass: "text-center", prop: 'totalPrice', name: gT('label.iostudentlist.TotalPrice'), cellTemplate: this.totalPriceTemplate, cellClass: 'text-right' },
            { headerClass: "text-center", prop: 'note', name: gT('label.iostudentlist.Note'), cellTemplate: this.descriptionTemplate },
            { name: '', width: 80, cellTemplate: this.actionsTemplate, resizeable: false, canAutoResize: false, sortable: false, draggable: false }
        ];

        //
        this.getFromServer();

        //
    }

    ngOnDestroy() {
        //this.saveToDisk();
    }
    
    onRemoved(file: any) {
        // do some stuff with the removed file.
    }

    onUploadStateChanged(state: boolean) {
        console.log(JSON.stringify(state));
    }

    search() {
        this.getFromServer();
    }

    private getFromServer() {
        this.loadingIndicator = true;
        //
        var disp = this.localService.getiodetailbyiotypeid(this.filterName, this.filterValue, this.fromDate, this.toDate, this.page.pageNumber, this.page.size).subscribe(
            list => this.onDataLoadSuccessful(list),
            error => this.onDataLoadFailed(error),
            () => {
                disp.unsubscribe();
                setTimeout(() => { this.loadingIndicator = false; }, 1500);
            });
    }

    private getReport(template: TemplateRef<any>) {
        this.localService.reportiodetailbyiotypeid(this.filterName, this.filterValue, this.fromDate, this.toDate, this.page.pageNumber, this.page.size).subscribe(
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
