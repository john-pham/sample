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
import { DocumentsService } from "../../services/documents.service";
import { AlertService, MessageSeverity, DialogType } from '../../services/alert.service';
import { Utilities } from '../../services/utilities';
import { File } from '../../models/file.model';
import { Document } from '../../models/document.model';
import { Results } from '../../models/results.model';
import { Page } from '../../models/page.model';
import { GrpDocumentsService } from "../../services/grpdocuments.service";
import { GrpDocument } from "../../models/grpdocument.model";
import { AccessRightsService } from "../../services/access-rights.service";

@Component({
    selector: 'documents',
    templateUrl: './documents.component.html',
    styleUrls: ['./documents.component.css'],
    animations: [fadeInOut]
})

export class DocumentsComponent implements OnInit, OnDestroy {
    rows = [];
    columns = [];

    rowHeads = [];
    columnHeads = [];
    grps = [];
    loadingIndicator: boolean = true;

    filterName: string;
    filterValue: string;
    grpId: string;

    private pointer: Document;
    private page: Page;

    public changesSavedCallback: () => void;
    public changesFailedCallback: () => void;
    public changesCancelledCallback: () => void;

    modalRef: BsModalRef;
    modalHeadRef: BsModalRef;


    constructor(private alertService: AlertService, private translationService: AppTranslationService,
        private localService: DocumentsService, private modalService: BsModalService,
        private grpService: GrpDocumentsService, public accessRightService: AccessRightsService) {
        this.pointer = new Document();
        this.page = new Page();
        this.pointer.logo = new File();
        //
        this.page.pageNumber = 0;
        this.page.size = 20;
        this.filterValue = "";
        this.grpId = "";
    }

    ngOnInit() {

        let gT = (key: string) => this.translationService.getTranslation(key);

        this.columns = [
            { headerClass: "text-center", prop: 'grDocumentName', name: gT('label.document.GrpDocument'), cellTemplate: this.statusTemplate },
            { headerClass: "text-center", prop: "code", name: gT('label.document.Code'), width: 100, headerTemplate: this.statusHeaderTemplate, cellTemplate: this.statusTemplate, resizeable: false, canAutoResize: false, sortable: false, draggable: false },
            { headerClass: "text-center", prop: 'name', name: gT('label.document.Name'), cellTemplate: this.hyperlinkTemplate },
            { headerClass: "text-center", prop: 'linkWebSite', name: gT('label.document.LinkWebSite'), cellTemplate: this.hyperlinkSiteTemplate },
            { headerClass: "text-center", prop: 'note', name: gT('label.document.Note'), cellTemplate: this.descriptionTemplate },
            { headerClass: "text-center", prop: 'id', name: '', width: 140, cellTemplate: this.actionsTemplate, resizeable: false, canAutoResize: false, sortable: false, draggable: false }
        ];

        this.getFromServer();
        this.grpId = '';
    }


    ngOnDestroy() {
        //this.saveToDisk();
    }

    //
    addDocument(template: TemplateRef<any>) {
        this.pointer.id = '';
        this.grpDocument();
        this.modalRef = this.modalService.show(template, { class: 'modal-lg' });
    }

    editDocument(template: TemplateRef<any>, index: string) {
        this.grpDocument();
        var disp = this.localService.get(index).subscribe(
            item => {
                //
                this.pointer.id = item.id;
                this.pointer.code = item.code;
                this.pointer.name = item.name;
                this.pointer.note = item.note;
                this.pointer.path = item.path;
                this.pointer.grpId = item.grpId;
                this.pointer.linkWebSite = item.linkWebSite;
                this.file_name = item.path;
                this.modalRef = this.modalService.show(template, { class: 'modal-lg' });
            },
            error => {
            },
            () => { disp.unsubscribe(); });


    }

    onSearchChanged(value: string) {
        this.getFromServer();
    }

    deleteDocument(index: string) {
        this.alertService.showDialog('Bạn có muốn xóa tài liệu này không?', DialogType.confirm, () => this.deleteHelper(index));
    }

    setPage(pageInfo) {
        this.page.pageNumber = pageInfo.offset;
        this.getFromServer();
    }

    private getFromServer() {
        this.loadingIndicator = true;
        //
        var disp = this.localService.search(this.filterName, this.filterValue, this.grpId, this.page.pageNumber, this.page.size).subscribe(
            resulted => this.onDataLoadSuccessful(resulted),
            error => this.onDataLoadFailed(error),
            () => {
                disp.unsubscribe();
                setTimeout(() => { this.loadingIndicator = false; }, 1500);
            });
        this.grpDocument();
    }

    grpDocument() {
        var disp = this.grpService.getAll().subscribe(
            results => this.onDataLoadGrpSuccessful(results),
            error => this.onDataLoadFailed(error),
            () => {
                disp.unsubscribe();
                setTimeout(() => { this.loadingIndicator = false; }, 1500);
            });
    }

    private onDataLoadGrpSuccessful(grps: GrpDocument[]) {
        if (grps != null && grps.length > 0) {
            this.pointer.grpId = grps[0].id;
        }
        this.grps = grps;
        this.alertService.stopLoadingMessage();
    }

    private onDataLoadSuccessful(resulted: Results<Document>) {
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

    private saveSuccessHelper(document?: Document) {
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
        this.alertService.showStickyMessage("Save Error", "The below errors occured whilst saving your changes:", MessageSeverity.error, error);
        this.alertService.showStickyMessage(error, null, MessageSeverity.error);

        if (this.changesFailedCallback)
            this.changesFailedCallback();
    }

    private deleteHelper(index: string) {
        this.localService.delete(index).subscribe(result => this.deleteSuccessHelper(result), error => this.deleteFailedHelper(error));
    }

    private deleteSuccessHelper(value: Boolean) {
        this.getFromServer();
        this.alertService.showMessage("Success", `document was deleted successfully`, MessageSeverity.success);
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

    src: string = "";
    file_name: string = "";

    fileInputClick() {
        document.getElementById('avatar').click();
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

    @ViewChild('hyperlinkTemplate')
    hyperlinkTemplate: TemplateRef<any>;

    @ViewChild('hyperlinkSiteTemplate')
    hyperlinkSiteTemplate: TemplateRef<any>;
    
}
