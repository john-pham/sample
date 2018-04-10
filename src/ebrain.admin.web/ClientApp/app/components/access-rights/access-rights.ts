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
import { AccessRightsService } from "../../services/access-rights.service";
import { AlertService, MessageSeverity, DialogType } from '../../services/alert.service';
import { Utilities } from '../../services/utilities';
import { File } from '../../models/file.model';
import { Branch } from '../../models/branch.model';
import { Results } from '../../models/results.model';
import { Page } from '../../models/page.model';
import { AccessRight } from "../../models/accessright.model";
import { FeatureGroupsService } from "../../services/featuregroup.service";
import { UserGroupsService } from "../../services/usergroup.service";
import { UserGroups } from "../../models/usergroups.model";
import { FeatureGroups } from "../../models/featuregroups.model";

@Component({
    selector: 'accessrights',
    templateUrl: './access-rights.html',
    styleUrls: ['./access-rights.css'],
    animations: [fadeInOut]
})

export class AccessRightsComponent implements OnInit, OnDestroy {
    rows = [];
    columns = [];

    userGroups = [];
    featureGroups = [];

    loadingIndicator: boolean = true;

    groupId: string;
    featureGroupId: string;

    private pointer: AccessRight;
    private page: Page;

    public changesSavedCallback: () => void;
    public changesFailedCallback: () => void;
    public changesCancelledCallback: () => void;

    modalRef: BsModalRef;
    modalHeadRef: BsModalRef;


    constructor(private alertService: AlertService, private translationService: AppTranslationService,
        private localService: AccessRightsService, private featureGroupService: FeatureGroupsService, private userGroupService: UserGroupsService,
        private modalService: BsModalService) {
        this.pointer = new AccessRight();
        this.page = new Page();
        this.page.pageNumber = 0;
        this.page.size = 20;
    }

    ngOnInit() {

        let gT = (key: string) => this.translationService.getTranslation(key);

        this.columns = [

            { headerClass: "text-center", prop: 'featureName', name: gT('label.accessright.Feature'), cellTemplate: this.nameTemplate },
            { headerClass: "text-center", prop: 'view', name: gT('label.accessright.View'), cellTemplate: this.viewTemplate },
            { headerClass: "text-center", prop: 'edit', name: gT('label.accessright.Edit'), cellTemplate: this.editTemplate },
            { headerClass: "text-center", prop: 'create', name: gT('label.accessright.Create'), cellTemplate: this.createTemplate },
            { headerClass: "text-center", prop: 'delete', name: gT('label.accessright.Delete'), cellTemplate: this.deleteTemplate }
        ];

        this.getFromServer();
    }

    ngOnDestroy() {
        //this.saveToDisk();
    }

    //
    src: string = "";
    file_name: string = "";

    //
    search() {
        this.loadingIndicator = true;
        //
        var disp = this.localService.search(this.groupId, this.featureGroupId, 0, 0).subscribe(
            resulted => this.onDataLoadSuccessful(resulted),
            error => this.onDataLoadFailed(error),
            () => {
                disp.unsubscribe();
                setTimeout(() => { this.loadingIndicator = false; }, 1500);
            });
    }

    setPage(pageInfo) {
        this.page.pageNumber = pageInfo.offset;
        this.getFromServer();
    }

    private getFromServer() {
        this.loadingIndicator = true;
        //
        var disp = this.userGroupService.getAll().subscribe(
            resulted => this.onDataLoadUserGroupSuccessful(resulted),
            error => this.onDataLoadFailed(error),
            () => {
                disp.unsubscribe();
                setTimeout(() => { this.loadingIndicator = false; }, 1500);
            });

        this.featureGroupService.getAll().subscribe(
            resulted => this.onDataLoadFeatureGroupSuccessful(resulted),
            error => this.onDataLoadFailed(error),
            () => {
                disp.unsubscribe();
                setTimeout(() => { this.loadingIndicator = false; }, 1500);
            });
    }

    private onDataLoadFeatureGroupSuccessful(resulted: FeatureGroups[]) {
        this.featureGroups = resulted;
        this.alertService.stopLoadingMessage();
    }

    private onDataLoadUserGroupSuccessful(resulted: UserGroups[]) {
        if (resulted != null && resulted.length > 0) {
            this.groupId = resulted[0].id;
            this.search();
        }
        this.userGroups = resulted;
        this.alertService.stopLoadingMessage();
    }

    private onDataLoadSuccessful(resulted: Results<AccessRight>) {
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

        this.localService.save(this.rows).subscribe(value => this.saveSuccessHelper(value), error => this.saveFailedHelper(error));
    }

    private saveSuccessHelper(result: Boolean) {
        this.alertService.stopLoadingMessage();

        this.alertService.showMessage("Success", `User \"${this.pointer.name}\" was created successfully`, MessageSeverity.success);

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

    updateViewValue(row, event, rowIndex) {
        row.view = event.target.checked;
    }

    updateEditValue(row, event, rowIndex) {
        row.edit = event.target.checked;
    }

    updateCreateValue(row, event, rowIndex) {
        row.create = event.target.checked;
    }

    updateDeleteValue(row, event, rowIndex) {
        row.delete = event.target.checked;
    }

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

    @ViewChild('viewTemplate')
    viewTemplate: TemplateRef<any>;

    @ViewChild('editTemplate')
    editTemplate: TemplateRef<any>;

    @ViewChild('createTemplate')
    createTemplate: TemplateRef<any>;

    @ViewChild('deleteTemplate')
    deleteTemplate: TemplateRef<any>;
}
