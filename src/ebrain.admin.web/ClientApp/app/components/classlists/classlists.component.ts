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

import { IOStockReport } from "../../models/iostockreport.model";
import { PaymentsService } from "../../services/payments.service";
import { Payment } from "../../models/payment.model";
import { ClassList } from "../../models/classlists.model";
import { ClassesService } from "../../services/classes.service";
import { SuppliersService } from "../../services/suppliers.service";
import { ClassStatusService } from "../../services/classstatus.service";
import { Supplier } from "../../models/supplier.model";
import { ClassStatus } from "../../models/classstatus.model";
import { AccessRightsService } from "../../services/access-rights.service";
import { Page } from "../../models/page.model";
import { Results } from "../../models/results.model";

@Component({
    selector: 'classlists',
    templateUrl: './classlists.component.html',
    styleUrls: ['./classlists.component.css'],
    animations: [fadeInOut]
})

export class ClassListsComponent implements OnInit, OnDestroy {
    @Input() isUsageTeacher: any = false;
    rows = [];
    columns = [];
    loadingIndicator: boolean = true;

    filterName: string;
    filterValue: string;
    statusId: string;
    supplierId: string;

    status = [];
    suppliers = [];
    ioStockId: any = "";
    private pointer: Grpsupplier;

    public changesSavedCallback: () => void;
    public changesFailedCallback: () => void;
    public changesCancelledCallback: () => void;

    modalRef: BsModalRef;
    private page: Page;
    constructor(private alertService: AlertService, private router: Router, private translationService: AppTranslationService,
        private localService: ClassesService, private supplierService: SuppliersService,
        private classStatusService: ClassStatusService, public accessRightService: AccessRightsService,
        private modalService: BsModalService) {
        this.filterValue = '';
        this.page = new Page();
        this.page.pageNumber = 0;
        this.page.size = 20;
        this.ioStockId = "";
    }

    setPage(pageInfo) {
        this.page.pageNumber = pageInfo.offset;
        this.getFromServer();
    }

    ngOnInit() {
        let gT = (key: string) => this.translationService.getTranslation(key);

        this.columns = [
            { headerClass: "text-center", prop: 'code', name: gT('label.classlist.Code'), cellTemplate: this.nameTemplate },
            { headerClass: "text-center", prop: 'name', name: gT('label.classlist.Name'), cellTemplate: this.nameTemplate },
            { headerClass: "text-center", prop: 'materialName', name: gT('label.classlist.MaterialName'), cellTemplate: this.nameTemplate },
            { headerClass: "text-center", prop: 'createdDate', name: gT('label.classlist.CreatedDate'), cellTemplate: this.nameTemplate },
            { headerClass: "text-center", prop: 'fullName', name: gT('label.classlist.CreatedBy'), cellTemplate: this.nameTemplate },

            { headerClass: "text-center", prop: 'supplierName', name: gT('label.classlist.SupplierName'), cellTemplate: this.nameTemplate },
            { headerClass: "text-center", prop: 'materialName', name: gT('label.classlist.MaterialName'), cellTemplate: this.nameTemplate },
            { headerClass: "text-center", prop: 'maxStudent', name: gT('label.classlist.MaxStudent'), cellTemplate: this.nameTemplate, cellClass: 'text-right', width: 80 },
            { headerClass: "text-center", prop: 'countStudent', name: gT('label.classlist.LearnStudent'), cellTemplate: this.nameTemplate, cellClass: 'text-right', width: 80 },
            { name: '', cellTemplate: this.actionsTemplate, resizeable: false, canAutoResize: false, sortable: false, draggable: false, width: 80 }
        ];

        var disp = this.classStatusService.getAll().subscribe(
            list => this.onDataLoadClassStatusSuccessful(list),
            error => this.onDataLoadFailed(error),
            () => {
                disp.unsubscribe();
                setTimeout(() => { this.loadingIndicator = false; }, 1500);
            });

        this.supplierService.search("", "", 4, 0, 0).subscribe(
            list => this.onDataLoadSupplierSuccessful(list),
            error => this.onDataLoadFailed(error),
            () => {
                disp.unsubscribe();
                setTimeout(() => { this.loadingIndicator = false; }, 1500);
            });



        this.getFromServer();
    }

    ngOnDestroy() {
        //this.saveToDisk();
    }

    goDetails(template: TemplateRef<any>, value: ClassList) {
        this.ioStockId = value.id;
        this.modalRef = this.modalService.show(template, { class: 'modal-large' });
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
        var disp = this.localService.getsummaries(
            this.filterName, this.filterValue, this.statusId, this.supplierId, "",
            (this.isUsageTeacher ? 1 : 0), this.page.pageNumber, this.page.size).subscribe(
            list => this.onDataLoadSuccessful(list),
            error => this.onDataLoadFailed(error),
            () => {
                disp.unsubscribe();
                setTimeout(() => { this.loadingIndicator = false; }, 1500);
            });


    }

    private onDataLoadSupplierSuccessful(resulted: Results<Supplier>) {
        this.suppliers = resulted.list;
        this.suppliers = [...this.suppliers];
        this.alertService.stopLoadingMessage();
    }

    private onDataLoadClassStatusSuccessful(list: ClassStatus[]) {
        this.status = list;
        this.status = [...this.status];
        this.alertService.stopLoadingMessage();

    }


    private onDataLoadSuccessful(resulted: Results<ClassList>) {
        this.page.totalElements = resulted.total;
        this.rows = resulted.list;
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

    showAddNew(template: TemplateRef<any>) {
        this.ioStockId = "";
        this.modalRef = this.modalService.show(template, { class: 'modal-large' });
    }

    closeAddNew() {
        this.modalRef.hide();
    }

    onActivateMaterial(event) {
        if (event.type == 'dblclick') {
            if (this.accessRightService.isEdit("8AA6E971-1C3D-4835-B154-D662CE12AE96")) {
                this.ioStockId = event.row.id;
                this.modalRef = this.modalService.show(this.template, { class: 'modal-large' });
            }
        }
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

    @ViewChild('template')
    template: TemplateRef<any>;
}
