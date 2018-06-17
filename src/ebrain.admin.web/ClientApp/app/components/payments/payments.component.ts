// ======================================
// Author: Ebrain Team
// Email:  johnpham@ymail.com
// Copyright (c) 2017 supperbrain.visualstudio.com
// 
// ==> Contact Us: supperbrain@outlook.com
// ======================================

import { Component, OnInit, OnDestroy, TemplateRef, ViewChild } from '@angular/core';
import { ActivatedRoute, ParamMap } from '@angular/router';

import { Observable } from 'rxjs/Observable';
import { fadeInOut } from '../../services/animations';
import { AppTranslationService } from "../../services/app-translation.service";
import { PaymentsService } from "../../services/payments.service";
import { AlertService, MessageSeverity, DialogType } from '../../services/alert.service';
import { Utilities } from '../../services/utilities';
import { MaterialLearn } from '../../models/MaterialLearn.model';
import { BsModalService } from 'ngx-bootstrap/modal';
import { BsModalRef } from 'ngx-bootstrap/modal/bs-modal-ref.service';
import { Unit } from "../../models/unit.model";
import { GrpMaterialLearn } from "../../models/grpMaterialLearn.model";
import { TypeMaterialLearn } from "../../models/typeMaterialLearn.model";
import { TypeMaterialLearnsService } from "../../services/typeMaterialLearns.service";
import { TypeMaterialsService } from "../../services/typeMaterials.service";
import { TypeMaterial } from "../../models/typeMaterial.model";
import { Student } from "../../models/student.model";
import { GrpMaterial } from "../../models/grpMaterial.model";
import { Supplier } from "../../models/supplier.model";
import { Payment } from "../../models/payment.model";
import { User } from "../../models/user.model";
import { Material } from "../../models/material.model";
import { PaymentDetail } from "../../models/paymentdetail.model";
import { IOStudentListService } from "../../services/iostudentlists.service";
import { IOStockReport } from "../../models/iostockreport.model";
import { PaymentType } from "../../models/paymenttype.model";
import { AccessRightsService } from "../../share/services/access-rights.service";
import { Results } from "../../models/results.model";
@Component({
    selector: 'payments',
    templateUrl: './payments.component.html',
    styleUrls: ['./payments.component.css'],
    animations: [fadeInOut]
})

export class PaymentsComponent implements OnInit, OnDestroy {
    rows = [];
    columns = [];

    rowios = [];
    columnios = [];


    loadingIndicator: boolean = true;

    filterName: string;
    filterValue: string;
    fromDate: Date;
    toDate: Date;
    ioId: string;

    allPaymentTypes: PaymentType[] = [];
    allUsers: User[] = [];

    private pointer: Payment;
    private isEditMode = true;

    public changesSavedCallback: () => void;
    public changesFailedCallback: () => void;
    public changesCancelledCallback: () => void;

    modalRef: BsModalRef;

    constructor(private alertService: AlertService, private route: ActivatedRoute, private translationService: AppTranslationService,
        private localService: PaymentsService, private modalService: BsModalService,
        public accessRightService: AccessRightsService,
        private typeservice: TypeMaterialsService, private ioservice: IOStudentListService) {
        this.pointer = new Payment();

        var date = new Date(), y = date.getFullYear(), m = date.getMonth();
        this.fromDate = new Date(y, m, 1);
        this.toDate = new Date();
        this.filterValue = '';
        this.ioId = '';
    }

    ngOnInit() {
        let gT = (key: string) => this.translationService.getTranslation(key);

        this.columns = [
            { headerClass: "text-center", prop: 'code', name: gT('label.payment.IONumber'), cellTemplate: this.typenameTemplate },

            { headerClass: "text-center", prop: 'totalPrice', name: gT('label.payment.TotalPrice'), cellTemplate: this.totalPriceTemplate, cellClass: 'text-right' },
            { headerClass: "text-center", prop: 'totalPricePayment', name: gT('label.payment.TotalPricePayment'), cellTemplate: this.totalPricePaymentTemplate, cellClass: 'text-right' },
            { headerClass: "text-center", prop: 'totalPriceExist', name: gT('label.payment.TotalPriceRest'), cellTemplate: this.totalPriceResTemplate, cellClass: 'text-right' },
            { headerClass: "text-center", prop: 'note', name: gT('label.payment.Note'), cellTemplate: this.descriptionTemplate },
            { name: '', width: 150, cellTemplate: this.actionsTemplate, resizeable: false, canAutoResize: false, sortable: false, draggable: false }
        ];

        //
        this.getFromServer(false);

        //
    }

    ngOnDestroy() {
        //this.saveToDisk();
    }

    showpayment(template: TemplateRef<any>) {
        this.modalRef = this.modalService.show(template, { class: 'modal-lg' });
    }

    onActivateMaterial(event) {
        if (event instanceof IOStockReport) {
            var row = event;
            this.onMappingIOToPaymentDetail(row);
        }
    }

    onMappingIOToPaymentDetail(row) {
        var iod = new PaymentDetail();

        iod.code = row.code;
        iod.ioStockId = row.id;

        iod.totalPrice = row.totalPriceExist;
        iod.totalPricePayment = row.totalPriceExist;
        row.totalPriceExist = row.totalPrice - row.totalPricePayment;
        this.rows.push(iod);
        this.rows = [...this.rows]
    }

    updateValue(row, event, rowIndex) {
        row.totalPricePayment = event.target.value;
        if (row.totalPricePayment > row.totalPrice) {
            row.totalPricePayment = row.totalPrice;
        }
        this.refreshValueRow(row, rowIndex);
    }

    refreshValueRow(row, rowIndex) {
        row.totalPriceExist = row.totalPrice - row.totalPricePayment;
        let rows = [...this.rows];
        rows[rowIndex] = row;
        this.rows = [...this.rows]
    }

    onRemoved(file: any) {
        // do some stuff with the removed file.
    }

    onUploadStateChanged(state: boolean) {
        console.log(JSON.stringify(state));
    }

    onSearchChanged(value: string) {
        //this.rows = this.rowsCache.filter(r => Utilities.searchArray(value, false, r.name, r.description) || value == 'important' && r.important || value == 'not important' && !r.important);
    }

    delete(row) {
        this.alertService.showDialog('Are you sure you want to delete the row?', DialogType.confirm, () => this.deleteHelper(row));
    }

    private deleteHelper(row) {
        this.rows = this.rows.filter(obj => obj !== row);
        this.rows = [...this.rows];
    }

    private deleteSuccessHelper(row: Student) {
        this.getFromServer(true);
        this.alertService.showMessage("Success", `MaterialLearn \"${row.name}\" was deleted successfully`, MessageSeverity.success);
        if (this.changesSavedCallback)
            this.changesSavedCallback();
    }


    private deleteFailedHelper(error: any) {
        this.alertService.stopLoadingMessage();
        this.alertService.showStickyMessage("Delete Error", "The below errors occured whilst deleting your changes:", MessageSeverity.error, error);
        this.alertService.showStickyMessage(error, null, MessageSeverity.error);

        if (this.changesFailedCallback)
            this.changesFailedCallback();
    }

    private getdefault(isReset: boolean) {
        //
        this.route.paramMap
            .switchMap((params: ParamMap) => {
                var id = '';
                if (isReset == false) {
                    id = params.get('id');
                }
                return this.localService.getdefault(id);
            })
            .subscribe(results => this.mappingHelper(results), error => this.onDataLoadFailed(error));
        //
        this.alertService.stopLoadingMessage();

    }

    private getIOID() {
        if (this.pointer.id == null || this.pointer.id.length == 0) {
            this.route.paramMap
                .switchMap((params: ParamMap) => {
                    //get io
                    var ioid = params.get('ioid');
                    if (ioid != null && ioid.length > 0) {
                        this.ioservice.getiopayment(this.filterName, this.filterValue, 0, 0, false, ioid, this.fromDate, this.toDate, 0, 0).subscribe(resulted => {
                            var results = resulted.list;
                            results.forEach(row => {
                                this.onMappingIOToPaymentDetail(row);
                            });
                        }, error => this.onDataLoadFailed(error));
                    }
                    return ioid;
                })
                .subscribe(results => {
                }, error => this.onDataLoadFailed(error));
        }
        //
        this.alertService.stopLoadingMessage();
    }

    private getPaymentTypes() {
        //load user
        this.localService.getPaymentTypes(false).subscribe(results => this.onDataLoadSuccessfulStudents(results), error => this.onDataLoadFailed(error));
    }

    private onDataLoadSuccessfulStudents(types: PaymentType[]) {
        if (types != null && types.length > 0) {
            this.pointer.paymentTypeId = types[0].ID;
        }
        this.allPaymentTypes = types;
    }

    addnew() {
        this.pointer.id = '';
        this.getFromServer(true);
        this.rows = [];
    }

    private getFromServer(isReset: boolean) {
        //load user
        this.loadingIndicator = true;
        //
        var disp = this.localService.getUsers(-1, -1).subscribe(
            results => this.allUsers = results,
            error => this.onDataLoadFailed(error),
            () => {
                disp.unsubscribe();
                setTimeout(() => { this.loadingIndicator = false; }, 1500);
            });
        this.getPaymentTypes();
        this.getdefault(isReset);
    }

    private onDataLoadSuccessful(ios: Payment[]) {
        this.rows = ios;
        this.alertService.stopLoadingMessage();

    }

    private onDataLoadFailed(error: any) {
        this.alertService.stopLoadingMessage();
        this.alertService.showStickyMessage("Load Error", `Unable to retrieve user data from the server.\r\nErrors: "${Utilities.getHttpResponseMessage(error)}"`,
            MessageSeverity.error, error);

    }

    private save() {
        if (this.rows != null && this.rows.length > 0) {
            this.alertService.startLoadingMessage("Saving changes...");
            this.pointer.ioDetails = this.rows;

            this.localService.save(this.pointer).subscribe(value => this.saveSuccessHelper(value), error => this.saveFailedHelper(error));
        }
        else {
            this.showErrorAlert("Input details", "Please selected one item on grids.");
        }
    }

    private deletemaster() {
        this.alertService.showDialog('Are you sure you want to delete?', DialogType.confirm, () => {
            this.alertService.startLoadingMessage("Delete changes...");
            this.pointer.ioDetails = this.rows;
            this.localService.deletemaster(this.pointer.id).subscribe(value => this.deletemasterSuccessHelper(value), error => this.saveFailedHelper(error));
        });
    }

    private deletemasterSuccessHelper(io?: Payment) {
        this.pointer = io;
        this.rows = [];
        this.alertService.stopLoadingMessage();
    }

    private cancelmaster() {
        this.alertService.showDialog('Are you sure you want to cancel?', DialogType.confirm, () => {
            this.alertService.startLoadingMessage("Cancel changes...");
            this.pointer.ioDetails = this.rows;
            this.localService.cancelmaster(this.pointer.id).subscribe(value => this.cancelmasterSuccessHelper(value), error => this.saveFailedHelper(error));
        });

    }

    private cancelmasterSuccessHelper(io?: Payment) {
        this.pointer = io;
        this.rows = [];
        this.alertService.stopLoadingMessage();
    }

    edit(template: TemplateRef<any>, index: string) {

        var disp = this.localService.get(index).subscribe(
            item => {
                //
                this.pointer = item;

                this.modalRef = this.modalService.show(template, { class: 'modal-lg' });
            },
            error => {
            },
            () => { disp.unsubscribe(); });


    }

    private mappingHelper(io?: Payment) {
        if (io != null) {
            this.pointer = io;
            this.pointer.paymentTypeId = io.paymentTypeId;

            this.rows = [];

            io.ioDetails.forEach(row => {
                var iod = new PaymentDetail();

                iod.code = row.code;
                iod.totalPrice = row.totalPrice;
                iod.totalPriceExist = row.totalPriceExist;
                iod.totalPricePayment = row.totalPricePayment;
                iod.Note = row.Note;
                iod.id = row.id;

                this.rows.push(iod);
                this.rows = [...this.rows];
            });

            this.alertService.stopLoadingMessage();

            this.getIOID();
        }
    }
    private saveSuccessHelper(io?: Payment) {

        this.mappingHelper(io);
        //if (this.isNewUser)
        this.alertService.showMessage("Success", `User \"${this.pointer.name}\" was created successfully`, MessageSeverity.success);
        //else if (!this.isEditingSelf)
        //    this.alertService.showMessage("Success", `Changes to user \"${this.pointer.Name}\" was saved successfully`, MessageSeverity.success);
        this.alertService.stopLoadingMessage();
        if (this.changesSavedCallback)
            this.changesSavedCallback();
    }


    private saveFailedHelper(error: any) {
        this.alertService.stopLoadingMessage();
        this.alertService.showStickyMessage("Save Error", "The below errors occured whilst saving your changes:", MessageSeverity.error, error);
        this.alertService.showStickyMessage(error, null, MessageSeverity.error);

        if (this.changesFailedCallback)
            this.changesFailedCallback();
    }

    close() {
        this.modalRef.hide();
    }

    @ViewChild('f')
    private form;

    private uniqueId: string = Utilities.uniqueId();

    private showErrorAlert(caption: string, message: string) {
        this.alertService.showMessage(caption, message, MessageSeverity.error);
    }

    @ViewChild('statusHeaderTemplate')
    statusHeaderTemplate: TemplateRef<any>;

    @ViewChild('nameTemplate')
    nameTemplate: TemplateRef<any>;

    @ViewChild('typenameTemplate')
    typenameTemplate: TemplateRef<any>;

    @ViewChild('grpnameTemplate')
    grpnameTemplate: TemplateRef<any>;

    @ViewChild('descriptionTemplate')
    descriptionTemplate: TemplateRef<any>;

    @ViewChild('actionsTemplate')
    actionsTemplate: TemplateRef<any>;

    @ViewChild('statusTemplate')
    statusTemplate: TemplateRef<any>;

    @ViewChild('priceTemplate')
    priceTemplate: TemplateRef<any>;

    @ViewChild('quantityTemplate')
    quantityTemplate: TemplateRef<any>;

    @ViewChild('totalPriceTemplate')
    totalPriceTemplate: TemplateRef<any>;

    @ViewChild('totalPricePaymentTemplate')
    totalPricePaymentTemplate: TemplateRef<any>;

    @ViewChild('totalPriceResTemplate')
    totalPriceResTemplate: TemplateRef<any>;

    @ViewChild('priceMaterialTemplate')
    priceMaterialTemplate: TemplateRef<any>;

}
