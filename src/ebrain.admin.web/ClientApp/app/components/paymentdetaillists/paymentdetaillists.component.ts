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
import { AccessRightsService } from "../../services/access-rights.service";
import { Page } from "../../models/page.model";
@Component({
    selector: 'paymentdetaillist',
    templateUrl: './paymentdetaillists.component.html',
    styleUrls: ['./paymentdetaillists.component.css'],
    animations: [fadeInOut]
})

export class PaymentDetailListsComponent implements OnInit, OnDestroy {
    rows = [];
    columns = [];
    loadingIndicator: boolean = true;

    filterName: string;
    filterValue: string;
    fromDate: Date;
    toDate: Date;

    private pointer: Grpsupplier;

    public changesSavedCallback: () => void;
    public changesFailedCallback: () => void;
    public changesCancelledCallback: () => void;

    modalRef: BsModalRef;
    private page: Page;
    constructor(private alertService: AlertService, private router: Router, private translationService: AppTranslationService, private localService: PaymentsService, public accessRightService: AccessRightsService,private modalService: BsModalService) {
        var date = new Date(), y = date.getFullYear(), m = date.getMonth();
        this.fromDate = new Date(y, m, 1);
        this.toDate = new Date(y, m + 1, 0);
        this.filterValue = '';
        this.page = new Page();
        this.page.pageNumber = 0;
        this.page.size = 20;
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
            { headerClass: "text-center", prop: 'ioNumber', name: gT('label.paymentlist.IONumber'), cellTemplate: this.nameTemplate },
            { headerClass: "text-center", prop: 'createDate', name: gT('label.paymentlist.CreateDate'), cellTemplate: this.nameTemplate },
            { headerClass: "text-center", prop: 'fullName', name: gT('label.paymentlist.CreateUser'), cellTemplate: this.nameTemplate },
            { headerClass: "text-center", prop: 'totalPrice', name: gT('label.paymentlist.TotalPrice'), cellTemplate: this.totalPriceTemplate, cellClass: 'text-right'},
            { headerClass: "text-center", prop: 'note', name: gT('label.paymentlist.Note'), cellTemplate: this.descriptionTemplate },
            { headerClass: "text-center", prop: '', width: 80, cellTemplate: this.actionsTemplate, resizeable: false, canAutoResize: false, sortable: false, draggable: false }
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
        if (value != null && value.paymentTypeId == 1) { url = '/payment'; } else { url='/paymentvouchers'; }
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
    }

    private getFromServer() {
        this.loadingIndicator = true;
        //
        var disp = this.localService.searchDetail(this.filterName, this.filterValue, this.fromDate, this.toDate, this.page.pageNumber, this.page.size).subscribe(
            list => this.onDataLoadSuccessful(list),
            error => this.onDataLoadFailed(error));
    }

    private onDataLoadSuccessful(list: Payment[]) {
        this.rows = list;
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
