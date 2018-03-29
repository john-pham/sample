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
import { SMSService } from "../../services/sms.service";
import { AlertService, MessageSeverity, DialogType } from '../../services/alert.service';
import { Utilities } from '../../services/utilities';
import { File } from '../../models/file.model';
import { SMS } from '../../models/sms.model';
import { Results } from '../../models/results.model';
import { Page } from '../../models/page.model';

@Component({
    selector: 'sms',
    templateUrl: './sms.component.html',
    styleUrls: ['./sms.component.css'],
    animations: [fadeInOut]
})

export class SMSComponent implements OnInit, OnDestroy {
    rows = [];
    columns = [];

    rowHeads = [];
    columnHeads = [];

    loadingIndicator: boolean = true;

    filterName: string;
    filterValue: string;
    phone: string;

    private pointer: SMS;
    private page: Page;

    public changesSavedCallback: () => void;
    public changesFailedCallback: () => void;
    public changesCancelledCallback: () => void;

    modalRef: BsModalRef;
    modalHeadRef: BsModalRef;

    constructor(private alertService: AlertService, private translationService: AppTranslationService, private localService: SMSService, private modalService: BsModalService) {
        this.pointer = new SMS();
        this.page = new Page();

        //
        this.page.pageNumber = 0;
        this.page.size = 20;
    }

    ngOnInit() {

        let gT = (key: string) => this.translationService.getTranslation(key);

        this.columns = [
            { headerClass: "text-center", prop: "code", name: gT('label.sms.BranchName'),width:150, cellTemplate: this.statusTemplate },
            { headerClass: "text-center", prop: 'phone', name: gT('label.sms.Phone'), width: 100, cellTemplate: this.nameTemplate },
            { headerClass: "text-center", prop: 'body', name: gT('label.sms.Body'), cellTemplate: this.nameTemplate },
            { headerClass: "text-center", prop: 'result', name: gT('label.sms.Result'), width: 100, cellTemplate: this.nameTemplate },
        ];

        this.getFromServer();
    }

    ngOnDestroy() {
        //this.saveToDisk();
    }

    //
    addSMS(template: TemplateRef<any>) {
        this.modalRef = this.modalService.show(template, { class: 'modal-lg' });
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

    private onDataLoadSuccessful(resulted: Results<SMS>) {
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
