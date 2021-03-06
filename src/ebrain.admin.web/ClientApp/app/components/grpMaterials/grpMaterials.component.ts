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
import { GrpMaterialsService } from "../../services/grpMaterials.service";
import { AlertService, MessageSeverity, DialogType } from '../../services/alert.service';
import { Utilities } from '../../services/utilities';
import { GrpMaterial } from '../../models/GrpMaterial.model';
import { BsModalService } from 'ngx-bootstrap/modal';
import { BsModalRef } from 'ngx-bootstrap/modal/bs-modal-ref.service';
import { TypeMaterial } from "../../models/typeMaterial.model";
import { AccessRightsService } from "../../services/access-rights.service";
import { Page } from "../../models/page.model";
import { TypeMaterialsService } from "../../services/typeMaterials.service";
import { Results } from "../../models/results.model";

@Component({
    selector: 'grpmaterials',
    templateUrl: './grpMaterials.component.html',
    styleUrls: ['./grpMaterials.component.css'],
    animations: [fadeInOut]
})

export class GrpMaterialsComponent implements OnInit, OnDestroy {
    rows = [];
    columns = [];
    loadingIndicator: boolean = true;

    allTypeMaterials: TypeMaterial[] = [];

    filterName: string;
    filterValue: string;

    private pointer: GrpMaterial;

    public changesSavedCallback: () => void;
    public changesFailedCallback: () => void;
    public changesCancelledCallback: () => void;

    modalRef: BsModalRef;
    private page: Page;
    constructor(private alertService: AlertService, private translationService: AppTranslationService,
        private localService: GrpMaterialsService,
        public accessRightService: AccessRightsService,
        private typeMaterialService: TypeMaterialsService,
        private modalService: BsModalService) {
        this.pointer = new GrpMaterial();
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
            { headerClass: "text-center", prop: "code", name: gT('label.grpMaterial.Code'), width: 100, headerTemplate: this.statusHeaderTemplate, cellTemplate: this.statusTemplate, resizeable: false, canAutoResize: false, sortable: false, draggable: false },
            { headerClass: "text-center", prop: 'name', name: gT('label.grpMaterial.Name'), cellTemplate: this.nameTemplate },
            { headerClass: "text-center", prop: 'typeMaterialName', name: gT('label.grpMaterial.TypeMaterialName'), cellTemplate: this.descriptionTemplate },
            { headerClass: "text-center", prop: 'note', name: gT('label.grpMaterial.Note'), cellTemplate: this.descriptionTemplate },
            { name: '', width: 150, cellTemplate: this.actionsTemplate, resizeable: false, canAutoResize: false, sortable: false, draggable: false }
        ];

        //
        this.getFromServer();

        //
    }

    ngOnDestroy() {
        //this.saveToDisk();
    }

    addGrpMaterial(template: TemplateRef<any>) {
        this.getTypeMaterial();
        this.pointer.id = "";
        this.modalRef = this.modalService.show(template);
    }

    imageFinishedUploading(file: any) {
        console.log(JSON.stringify(file.serverResponse));
    }

    onRemoved(file: any) {
        // do some stuff with the removed file.
    }

    onUploadStateChanged(state: boolean) {
        console.log(JSON.stringify(state));
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

    private deleteSuccessHelper(row: GrpMaterial) {
        this.getFromServer();
        this.alertService.showMessage("Success", `GrpMaterial \"${row.name}\" was deleted successfully`, MessageSeverity.success);
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
        var disp = this.localService.search(this.filterName, this.filterValue, this.page.pageNumber, this.page.size).subscribe(
            results => this.onDataLoadGrpSuccessful(results),
            error => this.onDataLoadFailed(error));
    }

    private getTypeMaterial() {
        var disp = this.typeMaterialService.search("", "", 0, 0).subscribe(
            results => this.onDataLoadTypeSuccessful(results),
            error => this.onDataLoadFailed(error));
    }

    private onDataLoadGrpSuccessful(resulted: Results<GrpMaterial>) {
        this.page.totalElements = resulted.total;
        this.rows = resulted.list;
        this.alertService.stopLoadingMessage();
        this.loadingIndicator = false;
    }

    private onDataLoadTypeSuccessful(resulted: Results<TypeMaterial>) {
        this.allTypeMaterials = resulted.list;
        this.alertService.stopLoadingMessage();
        this.loadingIndicator = false;
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

    edit(template: TemplateRef<any>, index: string) {
        this.getTypeMaterial();
        var disp = this.localService.get(index).subscribe(
            item => {
                //
                this.pointer = item;
                this.modalRef = this.modalService.show(template);
            },
            error => {
            },
            () => { disp.unsubscribe(); });


    }

    private saveSuccessHelper(user?: GrpMaterial) {
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
