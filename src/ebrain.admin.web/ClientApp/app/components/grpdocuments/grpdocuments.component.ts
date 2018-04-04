﻿// ======================================
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
import { GrpDocumentsService } from "../../services/grpdocuments.service";
import { AlertService, MessageSeverity, DialogType } from '../../services/alert.service';
import { Utilities } from '../../services/utilities';
import { File } from '../../models/file.model';
import { GrpDocument } from '../../models/grpdocument.model';
import { Results } from '../../models/results.model';
import { Page } from '../../models/page.model';

@Component({
    selector: 'grpdocuments',
    templateUrl: './grpdocuments.component.html',
    styleUrls: ['./grpdocuments.component.css'],
    animations: [fadeInOut]
})

export class GrpDocumentsComponent implements OnInit, OnDestroy {
    rows = [];
    columns = [];

    rowHeads = [];
    columnHeads = [];

    loadingIndicator: boolean = true;

    filterName: string;
    filterValue: string;

    private pointer: GrpDocument;
    private page: Page;

    public changesSavedCallback: () => void;
    public changesFailedCallback: () => void;
    public changesCancelledCallback: () => void;

    modalRef: BsModalRef;
    modalHeadRef: BsModalRef;


    constructor(private alertService: AlertService, private translationService: AppTranslationService, private localService: GrpDocumentsService, private modalService: BsModalService) {
        this.pointer = new GrpDocument();
        this.page = new Page();
        
        //
        this.page.pageNumber = 0;
        this.page.size = 20;
    }

    ngOnInit() {

        let gT = (key: string) => this.translationService.getTranslation(key);

        this.columns = [
            { headerClass: "text-center", prop: "code", name: gT('label.grpdocument.Code'), width: 100, headerTemplate: this.statusHeaderTemplate, cellTemplate: this.statusTemplate, resizeable: false, canAutoResize: false, sortable: false, draggable: false },
        
            { headerClass: "text-center", prop: 'name', name: gT('label.grpdocument.Name'), cellTemplate: this.nameTemplate },

            { headerClass: "text-center", prop: 'note', name: gT('label.grpdocument.Note'), cellTemplate: this.descriptionTemplate },
            { headerClass: "text-center", prop: 'id', name: '', width: 200, cellTemplate: this.actionsTemplate, resizeable: false, canAutoResize: false, sortable: false, draggable: false }
        ];
        
        this.getFromServer();
    }

    ngOnDestroy() {
        //this.saveToDisk();
    }

    //
    addGrpDocument(template: TemplateRef<any>) {
        this.modalRef = this.modalService.show(template, { class: 'modal-lg' });
    }

    editgrpdocument(template: TemplateRef<any>, index: string) {

        var disp = this.localService.get(index).subscribe(
            item => {
                //
                this.pointer.id = item.id;
                this.pointer.code = item.code;
                this.pointer.name = item.name;
                this.pointer.note = item.note;
                this.modalRef = this.modalService.show(template, { class: 'modal-lg' });
            },
            error => {
            },
            () => { disp.unsubscribe(); });


    }
    
    onSearchChanged(value: string) {
        this.getFromServer();
    }

    deletegrpdocument(index: string) {
        this.alertService.showDialog('Bạn có muốn xóa nhóm tài liệu này không?', DialogType.confirm, () => this.deleteHelper(index));
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

    private onDataLoadSuccessful(resulted: Results<GrpDocument>) {
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
    
    private saveSuccessHelper(grpdocument?: GrpDocument) {
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
        this.alertService.showMessage("Success", `GrpDocument was deleted successfully`, MessageSeverity.success);
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