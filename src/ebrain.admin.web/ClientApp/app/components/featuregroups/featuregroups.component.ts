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
import { SMSService } from "../../services/sms.service";
import { AlertService, MessageSeverity, DialogType } from '../../services/alert.service';
import { Utilities } from '../../services/utilities';
import { File } from '../../models/file.model';
import { SMS } from '../../models/sms.model';
import { Results } from '../../models/results.model';
import { Page } from '../../models/page.model';
import { FeatureGroups } from "../../models/featuregroups.model";
import { FeatureGroupsService } from "../../services/featuregroup.service";

@Component({
    selector: 'featuregroups',
    templateUrl: './featuregroups.component.html',
    styleUrls: ['./featuregroups.component.css'],
    animations: [fadeInOut]
})

export class FeatureGroupsComponent implements OnInit, OnDestroy {
    rows = [];
    columns = [];

    rowHeads = [];
    columnHeads = [];

    loadingIndicator: boolean = true;

    filterName: string;
    filterValue: string;
    phone: string;

    private pointer: FeatureGroups;
    private page: Page;

    public changesSavedCallback: () => void;
    public changesFailedCallback: () => void;
    public changesCancelledCallback: () => void;

    modalRef: BsModalRef;
    modalHeadRef: BsModalRef;

    constructor(private alertService: AlertService, private translationService: AppTranslationService, private localService: FeatureGroupsService, private modalService: BsModalService) {
        this.pointer = new FeatureGroups();
        this.page = new Page();
        this.filterName = "";
        this.filterValue = "";
        //
        this.page.pageNumber = 0;
        this.page.size = 20;
    }

    ngOnInit() {

        let gT = (key: string) => this.translationService.getTranslation(key);

        this.columns = [
         
            { headerClass: "text-center", prop: 'name', name: gT('label.featuregroup.Name'), width: 100, cellTemplate: this.nameTemplate },
            { headerClass: "text-center", prop: 'description', name: gT('label.featuregroup.Note'), cellTemplate: this.nameTemplate },
            { name: '', width: 150, cellTemplate: this.actionsTemplate, resizeable: false, canAutoResize: false, sortable: false, draggable: false }
        ];

        this.getFromServer();
    }

    ngOnDestroy() {
        //this.saveToDisk();
    }

    //
    add(template: TemplateRef<any>) {
        this.modalRef = this.modalService.show(template, { class: 'modal-lg' });
    }

    edit(template: TemplateRef<any>, index: string) {

        var disp = this.localService.get(index).subscribe(
            item => {
                //
                this.pointer.id = item.id;
                this.pointer.code = item.code;
                this.pointer.name = item.name;
                this.pointer.description = item.description;

                //
                this.modalRef = this.modalService.show(template);
            },
            error => {
            },
            () => { disp.unsubscribe(); });


    }

    delete(row) {
        this.alertService.showDialog('Are you sure you want to delete the task?', DialogType.confirm, () => this.deleteHelper(row));
    }

    private deleteHelper(row) {
        this.localService.delete(row.id).subscribe(value => this.deleteSuccessHelper(row), error => this.deleteFailedHelper(error));
    }

    private deleteSuccessHelper(row: FeatureGroups) {
        this.getFromServer();
        this.alertService.showMessage("Success", `Class \"${row.name}\" was deleted successfully`, MessageSeverity.success);
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
   
    onSearchChanged(value: string) {
        this.getFromServer();
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

    private onDataLoadSuccessful(resulted: Results<FeatureGroups>) {
        this.page.totalElements = resulted.total;
        this.rows = resulted.list;
        this.alertService.stopLoadingMessage();
    }

    private onDataLoadFailed(error: any) {
        this.alertService.stopLoadingMessage();
        this.alertService.showStickyMessage("Load Error", `Unable to retrieve user data from the server.\r\nErrors: "${Utilities.getHttpResponseMessage(error)}"`,
            MessageSeverity.error, error);

    }

    private saveSuccessHelper(user?: FeatureGroups) {
        this.alertService.stopLoadingMessage();
        //this.resetForm();
        this.modalRef.hide();
        //
        this.getFromServer();
        //
        //if (this.isNewUser)
        this.alertService.showMessage("Success", `User \"${this.pointer.name}\" was created successfully`, MessageSeverity.success);
        //else if (!this.isEditingSelf)
        //    this.alertService.showMessage("Success", `Changes to user \"${this.pointer.name}\" was saved successfully`, MessageSeverity.success);

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
