// ======================================
// Author: Ebrain Team
// Email:  johnpham@ymail.com
// Copyright (c) 2017 supperbrain.visualstudio.com
// 
// ==> Contact Us: supperbrain@outlook.com
// ======================================

import { Component, OnInit, OnDestroy, TemplateRef, ViewChild } from '@angular/core';

import { BsModalService } from 'ngx-bootstrap/modal';
import { BsModalRef } from 'ngx-bootstrap/modal/bs-modal-ref.service';

import { fadeInOut } from '../../services/animations';
import { AppTranslationService } from "../../services/app-translation.service";
import { BranchesService } from "../../services/branches.service";
import { AlertService, MessageSeverity, DialogType } from '../../services/alert.service';
import { Utilities } from '../../services/utilities';
import { File } from '../../models/file.model';
import { Branch } from '../../models/branch.model';
import { Results } from '../../models/results.model';
import { Page } from '../../models/page.model';
import { AccessRightsService } from "../../services/access-rights.service";
import { saveAs } from "file-saver";
import { BranchZalo } from '../../models/branchzalo.model';
import * as jsPDF from 'jspdf';
import 'jspdf-autotable';

@Component({
    selector: 'branches',
    templateUrl: './branches.component.html',
    styleUrls: ['./branches.component.css'],
    animations: [fadeInOut]
})

export class BranchesComponent implements OnInit, OnDestroy {
    rows = [];
    columns = [];

    rowHeads = [];
    columnHeads = [];

    loadingIndicator: boolean = true;

    filterName: string;
    filterValue: string;

    private pointer: Branch;
    private page: Page;

    readonly smsTab = "sms";
    readonly zaloTab = "zalo";

    activeTab: string = this.smsTab;
    isSMSActive = true;
    isZaloActive = false;

    public changesSavedCallback: () => void;
    public changesFailedCallback: () => void;
    public changesCancelledCallback: () => void;

    modalRef: BsModalRef;
    modalHeadRef: BsModalRef;


    constructor(private alertService: AlertService, private translationService: AppTranslationService,
        private localService: BranchesService, public accessRightService: AccessRightsService, private modalService: BsModalService) {
        this.pointer = new Branch();
        this.pointer.branchZalo = new BranchZalo();
        this.page = new Page();

        //
        this.pointer.logo = new File();
        //
        this.page.pageNumber = 0;
        this.page.size = 20;
        this.filterName = "";
        this.filterValue = "";
    }

    ngOnInit() {

        let gT = (key: string) => this.translationService.getTranslation(key);

        this.columns = [
            { headerClass: "text-center", prop: "code", name: gT('label.branch.Code'), width: 100, headerTemplate: this.statusHeaderTemplate, cellTemplate: this.statusTemplate, resizeable: false, canAutoResize: false, sortable: false, draggable: false },
            { headerClass: "text-center", prop: "logo.name", name: '', cellTemplate: this.logoTemplate },
            { headerClass: "text-center", prop: 'name', name: gT('label.branch.Name'), cellTemplate: this.nameTemplate },

            { headerClass: "text-center", prop: 'address', name: gT('label.branch.Address'), cellTemplate: this.descriptionTemplate },
            { headerClass: "text-center", prop: 'id', name: '', width: 200, cellTemplate: this.actionsTemplate, resizeable: false, canAutoResize: false, sortable: false, draggable: false }
        ];

        this.columnHeads = [
            { headerClass: "text-center", prop: "isExist", name: gT('label.branch.Manage'), width: 30, cellTemplate: this.checkboxTemplate, cellClass: 'text-center' },
            { headerClass: "text-center", prop: 'name', name: gT('label.branch.Name'), cellTemplate: this.nameTemplate }
        ];

        this.getFromServer();
    }

    ngOnDestroy() {
        //this.saveToDisk();
    }

    //
    src: string = "";
    file_name: string = "";

    fileInputClick() {
        document.getElementById('avatar').click();
    }

    clearFile() {
        this.pointer.logo.name = null;
        this.pointer.logo.type = null;
        this.pointer.logo.value = null;
        this.src = "";
        this.file_name = "";
    }

    onFileChange(event) {
        let reader = new FileReader();
        if (event.target.files && event.target.files.length > 0) {
            let file = event.target.files[0];
            //
            reader.onload = () => {
                this.file_name = file.name;
                this.src = reader.result;
                this.pointer.logo.name = file.name;
                this.pointer.logo.type = file.type;
                this.pointer.logo.value = reader.result.split(',')[1];
            };
            //
            reader.onloadend = (loadEvent: any) => {
                this.src = loadEvent.target.result;
            };
            //
            reader.readAsDataURL(file);
        }
    }

    //
    addBranch(template: TemplateRef<any>) {
        this.modalRef = this.modalService.show(template, { class: 'modal-lg' });
    }

    editBranch(template: TemplateRef<any>, index: string) {

        var disp = this.localService.get(index).subscribe(
            item => {
                this.clearFile();
                this.pointer.id = item.id;
                this.pointer.code = item.code;
                this.pointer.name = item.name;
                this.pointer.email = item.email;
                this.pointer.address = item.address;
                this.pointer.branchZalo = item.branchZalo;
                this.src = item.logo.name; 
                //
                this.modalRef = this.modalService.show(template, { class: 'modal-lg' });
            },
            error => {
            },
            () => { disp.unsubscribe(); });


    }

    onOutputCSV() {
        //
        //
        this.localService.outputCSV(this.filterName, this.filterValue, this.page.pageNumber, this.page.size).subscribe(result => {

            var blob = new Blob([result], { type: "text/plain;charset=utf-8" });
            saveAs(blob, "output.branches.csv");

        }, error => {
        });

    }

    onOutputPdf() {
        //
        let gT = (key: string) => this.translationService.getTranslation(key);
        let doc = new jsPDF();

        let columns = [
            { title: gT('label.branch.Code'), dataKey: "code" },
            { title: gT('label.branch.Name'), dataKey: "name" },
            { title: gT('label.branch.Address'), dataKey: "address" }];

        doc.autoTable(columns, this.rows, {
            styles: { fillColor: [100, 255, 255] },
            columnStyles: {
                id: { fillColor: 255 }
            },
            margin: { top: 60 },
            addPageContent: function (data) {
                doc.text(gT("pageMain.branch.header"), 40, 30);
            }
        });
        doc.save('export.branches.pdf');
    }

    //head
    editHead(template: TemplateRef<any>, index: string) {
        var disp = this.localService.getBranchHead(index).subscribe(
            items => {
                this.rowHeads = items;
                //
                this.modalHeadRef = this.modalService.show(template, { class: 'modal-lg' });
            },
            error => {
            },
            () => { disp.unsubscribe(); });
    }

    onSearchChanged(value: string) {
        this.filterValue = value;
        this.getFromServer();
    }

    deleteBranch(index: string) {
        this.alertService.showDialog('Bạn có muốn xóa chi nhánh này không?', DialogType.confirm, () => this.deleteHelper(index));
    }

    setPage(pageInfo) {
        this.page.pageNumber = pageInfo.offset;
        this.getFromServer();
    }

    private getFromServer() {
        this.loadingIndicator = true;
        //
        var disp = this.localService.search(this.filterName, this.filterValue, this.page.pageNumber, this.page.size).subscribe(
            resulted => this.onDataLoadSuccessful(resulted),
            error => this.onDataLoadFailed(error),
            () => {
                disp.unsubscribe();
                setTimeout(() => { this.loadingIndicator = false; }, 1500);
            });
    }

    private onDataLoadSuccessful(resulted: Results<Branch>) {
        this.page.totalElements = resulted.total;
        this.rows = resulted.list;
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

    saveHead() {
        this.alertService.startLoadingMessage("Saving changes...");

        this.localService.saveHead(this.rowHeads).subscribe(values => {
            this.rowHeads = values;
            this.alertService.stopLoadingMessage();
            this.alertService.showMessage("Success", `Head branch was saved successfully`, MessageSeverity.success);
        }, error => this.saveFailedHelper(error));
    }

    private saveSuccessHelper(branch?: Branch) {
        this.alertService.stopLoadingMessage();
        //this.resetForm();
        this.modalRef.hide();
        //
        this.getFromServer();
        //
        //if (this.isNewUser)
        this.alertService.showMessage("Success", `User \"${this.pointer.name}\" was created successfully`, MessageSeverity.success);
        //else if (!this.isEditingSelf)
        //    this.alertService.showMessage("Success", `Changes to user \"${this.pointer.Name}\" was saved successfully`, MessageSeverity.success);

        if (this.changesSavedCallback)
            this.changesSavedCallback();
    }

    private saveFailedHelper(error: any) {
        this.alertService.stopLoadingMessage();
        this.alertService.showStickyMessage("Superbrain thông báo", "The below errors occured whilst saving your changes:", MessageSeverity.error, error);
        this.alertService.showStickyMessage(error, null, MessageSeverity.error);

        if (this.changesFailedCallback)
            this.changesFailedCallback();
    }

    private deleteHelper(index: string) {
        this.localService.delete(index).subscribe(result => this.deleteSuccessHelper(result), error => this.deleteFailedHelper(error));
    }

    private deleteSuccessHelper(value: Boolean) {
        this.getFromServer();
        this.alertService.showMessage("Success", `Branch was deleted successfully`, MessageSeverity.success);
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

    updateValue(row, event, rowIndex) {
        row.isExist = event.target.checked;
    }

    close() {
        this.modalRef.hide();
    }
    closeHead() {
        this.modalHeadRef.hide();
    }

    search() {
        this.getFromServer();
    }

    onShowTab(event) {
        this.setActiveTab(event.target.hash);

        switch (this.activeTab) {
            case this.smsTab:
                this.isSMSActive = true;
                break;
            case this.zaloTab:
                this.isZaloActive = true;
                break;
            default:
                throw new Error("Selected bootstrap tab is unknown. Selected Tab: " + this.activeTab);
        }
    }

    setActiveTab(tab: string) {
        this.activeTab = tab.split("#", 2).pop();
    }


    @ViewChild('f')
    private form;

    private uniqueId: string = Utilities.uniqueId();

    private showErrorAlert(caption: string, message: string) {
        this.alertService.showMessage(caption, message, MessageSeverity.error);
    }



    @ViewChild('statusHeaderTemplate')
    statusHeaderTemplate: TemplateRef<any>;

    @ViewChild('logoTemplate')
    logoTemplate: TemplateRef<any>;

    @ViewChild('nameTemplate')
    nameTemplate: TemplateRef<any>;

    @ViewChild('descriptionTemplate')
    descriptionTemplate: TemplateRef<any>;

    @ViewChild('actionsTemplate')
    actionsTemplate: TemplateRef<any>;

    @ViewChild('statusTemplate')
    statusTemplate: TemplateRef<any>;

    @ViewChild('checkboxTemplate')
    checkboxTemplate: TemplateRef<any>;


}
