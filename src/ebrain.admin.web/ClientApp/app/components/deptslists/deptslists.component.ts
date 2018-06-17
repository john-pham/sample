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
import { DeptService } from "../../services/depts.service";
import { Depts } from "../../models/depts.model";
import { AccessRightsService } from "../../share/services/access-rights.service";
import { Page } from "../../models/page.model";
import { Results } from "../../models/results.model";

@Component({
    selector: 'deptslist',
    templateUrl: './deptslists.component.html',
    styleUrls: ['./deptslists.component.css'],
    animations: [fadeInOut]
})

export class DeptsListsComponent implements OnInit, OnDestroy {
    rows = [];
    columns = [];
    loadingIndicator: boolean = true;

    filterName: string;
    filterValue: string;
    fromDate: Date;
    toDate: Date;

    public changesSavedCallback: () => void;
    public changesFailedCallback: () => void;
    public changesCancelledCallback: () => void;

    modalRef: BsModalRef;
    private page: Page;
    constructor(private alertService: AlertService, private router: Router,
        private translationService: AppTranslationService, private localService: DeptService,
        public accessRightService: AccessRightsService,
        private modalService: BsModalService) {
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
            { headerClass: "text-center", prop: 'studentCode', name: gT('label.deptslist.StudentCode'), cellTemplate: this.nameTemplate },
            { headerClass: "text-center", prop: 'studentName', name: gT('label.deptslist.StudentName'), cellTemplate: this.nameTemplate },
            { headerClass: "text-center", prop: 'phone', name: gT('label.deptslist.Phone'), cellTemplate: this.nameTemplate },
            { headerClass: "text-center", prop: 'receiptFirst', name: gT('label.deptslist.ReceiptFirst'), cellTemplate: this.ReceiptFirstTemplate, cellClass: 'text-right' },
            { headerClass: "text-center", prop: 'paymentFirst', name: gT('label.deptslist.PaymentFirst'), cellTemplate: this.PaymentFirstTemplate, cellClass: 'text-right' },

            { headerClass: "text-center", prop: 'totalPriceReceipt', name: gT('label.deptslist.TotalPriceReceipt'), cellTemplate: this.TotalPriceReceiptTemplate, cellClass: 'text-right' },
            { headerClass: "text-center", prop: 'receipt', name: gT('label.deptslist.Receipt'), cellTemplate: this.ReceiptTemplate, cellClass: 'text-right' },

            { headerClass: "text-center", prop: 'totalPricePayment', name: gT('label.deptslist.TotalPricePayment'), cellTemplate: this.TotalPricePaymentTemplate, cellClass: 'text-right' },
            { headerClass: "text-center", prop: 'payment', name: gT('label.deptslist.Payment'), cellTemplate: this.PaymentTemplate, cellClass: 'text-right' },
            
            { headerClass: "text-center", prop: 'endReceipt', name: gT('label.deptslist.EndReceipt'), cellTemplate: this.EndReceiptTemplate, cellClass: 'text-right' },
            { headerClass: "text-center", prop: 'endPayment', name: gT('label.deptslist.EndPayment'), cellTemplate: this.EndPaymentTemplate, cellClass: 'text-right' },
            { name: '', width: 80, cellTemplate: this.actionsTemplate, resizeable: false, canAutoResize: false, sortable: false, draggable: false }
        ];

        //
        this.search();

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

    onSearchChanged(value: string) {
        this.filterValue = value;
        this.search();
    }

    private search() {
        this.loadingIndicator = true;
        //
        var disp = this.localService.getDepts(this.filterName, this.filterValue, this.fromDate, this.toDate, this.page.pageNumber, this.page.size).subscribe(
            list => this.onDataLoadSuccessful(list),
            error => this.onDataLoadFailed(error),
            () => {
                disp.unsubscribe();
                setTimeout(() => { this.loadingIndicator = false; }, 1500);
            });
    }

    private onDataLoadSuccessful(resulted: Results<Depts>) {
        this.page.totalElements = resulted.total;
        this.rows = resulted.list;
        this.alertService.stopLoadingMessage();
    }

    private onDataLoadFailed(error: any) {
        this.alertService.stopLoadingMessage();
        this.alertService.showStickyMessage("Load Error", `Unable to retrieve user data from the server.\r\nErrors: "${Utilities.getHttpResponseMessage(error)}"`,
            MessageSeverity.error, error);
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

    @ViewChild('ReceiptFirstTemplate')
    ReceiptFirstTemplate: TemplateRef<any>;

    @ViewChild('PaymentFirstTemplate')
    PaymentFirstTemplate: TemplateRef<any>;


    @ViewChild('TotalPricePaymentTemplate')
    TotalPricePaymentTemplate: TemplateRef<any>;


    @ViewChild('TotalPriceReceiptTemplate')
    TotalPriceReceiptTemplate: TemplateRef<any>;


    @ViewChild('PaymentTemplate')
    PaymentTemplate: TemplateRef<any>;
    
    @ViewChild('ReceiptTemplate')
    ReceiptTemplate: TemplateRef<any>;

    @ViewChild('EndPaymentTemplate')
    EndPaymentTemplate: TemplateRef<any>;
    
    @ViewChild('EndReceiptTemplate')
    EndReceiptTemplate: TemplateRef<any>;
}
