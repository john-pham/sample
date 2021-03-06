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
import { SuppliersService } from "../../services/suppliers.service";
import { AlertService, MessageSeverity, DialogType } from '../../services/alert.service';
import { Utilities } from '../../services/utilities';
import { Supplier } from '../../models/Supplier.model';
import { BsModalService } from 'ngx-bootstrap/modal';
import { BsModalRef } from 'ngx-bootstrap/modal/bs-modal-ref.service';
import { Grpsupplier } from "../../models/grpsupplier.model";
import { AccessRightsService } from "../../services/access-rights.service";
import { Page } from "../../models/page.model";
import { Results } from "../../models/results.model";
import { AccountService } from "../../services/account.service";
import { User } from "../../models/user.model";
@Component({
    selector: 'supplierteachs',
    templateUrl: './supplierteachs.component.html',
    styleUrls: ['./supplierteachs.component.css'],
    animations: [fadeInOut]
})

export class SupplierTeacherComponent implements OnInit, OnDestroy {
    rows = [];
    users = [];
    columns = [];
    loadingIndicator: boolean = true;
    isEditMode: boolean = true;

    filterName: string;
    filterValue: string;

    allGrpSuppliers: Grpsupplier[] = [];

    private pointer: Supplier;

    public changesSavedCallback: () => void;
    public changesFailedCallback: () => void;
    public changesCancelledCallback: () => void;

    modalRef: BsModalRef;
    private page: Page;

    constructor(private alertService: AlertService, private translationService: AppTranslationService,
        private localService: SuppliersService, private accountService: AccountService,
        public accessRightService: AccessRightsService, private modalService: BsModalService) {
        this.pointer = new Supplier();
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
            { headerClass: "text-center", prop: "code", name: gT('label.supplier.Code'), width: 100, headerTemplate: this.statusHeaderTemplate, cellTemplate: this.statusTemplate, resizeable: false, canAutoResize: false, sortable: false, draggable: false },
            { headerClass: "text-center", prop: 'name', name: gT('label.supplier.Name'), cellTemplate: this.nameTemplate },
            { headerClass: "text-center", prop: 'phone', name: gT('label.supplier.Phone'), cellTemplate: this.nameTemplate },
            { headerClass: "text-center", prop: 'address', name: gT('label.supplier.Address'), cellTemplate: this.nameTemplate },
            { headerClass: "text-center", prop: 'note', name: gT('label.supplier.Note'), cellTemplate: this.descriptionTemplate },
            { name: '', width: 150, cellTemplate: this.actionsTemplate, resizeable: false, canAutoResize: false, sortable: false, draggable: false }
        ];

        //
        this.getFromServer();

        //
    }

    ngOnDestroy() {
        //this.saveToDisk();
    }

    addSupplier(template: TemplateRef<any>) {
        this.pointer.id = "";
        this.getGrpSupplier();
        this.getAllUser();
        this.modalRef = this.modalService.show(template, { class: 'modal-lg' });
    }

    onSearchChanged(value: string) {
        this.filterValue = value;
        this.getFromServer();
    }

    delete(row) {
        this.alertService.showDialog('Are you sure you want to delete the task?', DialogType.confirm, () => this.deleteHelper(row));
    }

    private deleteHelper(row) {
        this.localService.delete(row.id).subscribe(value => this.deleteSuccessHelper(row), error => this.deleteFailedHelper(error));
    }

    private deleteSuccessHelper(row: Supplier) {
        this.getFromServer();
        this.alertService.showMessage("Success", `Supplier \"${row.name}\" was deleted successfully`, MessageSeverity.success);
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

    private getFromServer() {
        this.loadingIndicator = true;
        //
        var disp = this.localService.search(this.filterName, this.filterValue, 4, this.page.pageNumber, this.page.size).subscribe(
            results => this.onDataLoadSuccessful(results),
            error => this.onDataLoadFailed(error),
            () => {
                disp.unsubscribe();
                setTimeout(() => { this.loadingIndicator = false; }, 1500);
            });
    }

    private getAllUser() {
        this.loadingIndicator = true;
        this.accountService.getAllUsers().subscribe(
            results => this.onDataLoadUserSuccessful(results),
            error => this.onDataLoadFailed(error));
    }

    private onDataLoadUserSuccessful(resulted: User[]) {
        this.users = resulted;
        this.alertService.stopLoadingMessage();
        this.loadingIndicator = false;
    }

    private onDataLoadSuccessful(resulted: Results<Supplier>) {
        this.page.totalElements = resulted.total;
        this.rows = resulted.list;
        this.alertService.stopLoadingMessage();
        this.loadingIndicator = false;
    }

    private getGrpSupplier() {
        this.loadingIndicator = true;
        this.localService.getGrpSupplier(4).subscribe(
            results => this.onDataLoadSuccessfulGrp(results),
            error => this.onDataLoadFailed(error));
    }

    private onDataLoadSuccessfulGrp(resulted: Results<Grpsupplier>) {
        var grpSuppliers = resulted.list;
        if (grpSuppliers != null && grpSuppliers.length > 0) {
            this.pointer.grpSupplierId = grpSuppliers[0].id;
        }
        this.allGrpSuppliers = grpSuppliers;
        this.loadingIndicator = false
        this.alertService.stopLoadingMessage();
    }

    private onDataLoadFailed(error: any) {
        this.alertService.stopLoadingMessage();
        this.alertService.showStickyMessage("Load Error", `Unable to retrieve user data from the server.\r\nErrors: "${Utilities.getHttpResponseMessage(error)}"`,
            MessageSeverity.error, error);

    }

    private save() {
        this.alertService.startLoadingMessage("Saving changes...");
        this.localService.save(this.pointer).subscribe(value => this.saveSuccessHelper(value), error => this.saveFailedHelper(error));
    }

    private saveSuccessHelper(user?: Supplier) {
        this.alertService.stopLoadingMessage();
        this.modalRef.hide();
        this.getFromServer();
        this.alertService.showMessage("Success", `Supplier \"${this.pointer.name}\" was created successfully`, MessageSeverity.success);

        if (this.changesSavedCallback)
            this.changesSavedCallback();
    }

    edit(template: TemplateRef<any>, index: string) {
        this.getGrpSupplier();
        this.getAllUser();
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

    private saveFailedHelper(error: any) {
        this.alertService.stopLoadingMessage();
        this.alertService.showStickyMessage("Superbrain thông báo", "The below errors occured whilst saving your changes:", MessageSeverity.error, error);
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

    @ViewChild('descriptionTemplate')
    descriptionTemplate: TemplateRef<any>;

    @ViewChild('actionsTemplate')
    actionsTemplate: TemplateRef<any>;

    @ViewChild('statusTemplate')
    statusTemplate: TemplateRef<any>;
}
