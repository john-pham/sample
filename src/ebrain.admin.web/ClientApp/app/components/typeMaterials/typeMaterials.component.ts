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
import { TypeMaterialsService } from "../../services/typeMaterials.service";
import { AlertService, MessageSeverity, DialogType } from '../../services/alert.service';
import { Utilities } from '../../services/utilities';
import { TypeMaterial } from '../../models/TypeMaterial.model';
import { BsModalService } from 'ngx-bootstrap/modal';
import { BsModalRef } from 'ngx-bootstrap/modal/bs-modal-ref.service';
import { AccessRightsService } from "../../services/access-rights.service";
import { Page } from "../../models/page.model";
import { Results } from "../../models/results.model";
@Component({
    selector: 'typematerials',
    templateUrl: './typeMaterials.component.html',
    styleUrls: ['./typeMaterials.component.css'],
    animations: [fadeInOut]
})

export class TypeMaterialsComponent implements OnInit, OnDestroy {
    rows = [];
    columns = [];
    loadingIndicator: boolean = true;

    filterName: string;
    filterValue: string;

    private pointer: TypeMaterial;

    public changesSavedCallback: () => void;
    public changesFailedCallback: () => void;
    public changesCancelledCallback: () => void;

    modalRef: BsModalRef;
    private page: Page;
    constructor(private alertService: AlertService, private translationService: AppTranslationService, private localService: TypeMaterialsService, public accessRightService: AccessRightsService, private modalService: BsModalService) {
        this.pointer = new TypeMaterial();
        this.page = new Page();
this.filterValue = '';
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
            { headerClass: "text-center", prop: "code", name: gT('label.typeMaterial.Code'), width: 100, headerTemplate: this.statusHeaderTemplate, cellTemplate: this.statusTemplate, resizeable: false, canAutoResize: false, sortable: false, draggable: false },
            { headerClass: "text-center", prop: 'name', name: gT('label.typeMaterial.Name'), cellTemplate: this.nameTemplate },
            { headerClass: "text-center", prop: 'note', name: gT('label.typeMaterial.Note'), cellTemplate: this.descriptionTemplate },
            { name: '', width: 140, cellTemplate: this.actionsTemplate, resizeable: false, canAutoResize: false, sortable: false, draggable: false }
        ];

        //
        this.getFromServer();

        //
    }

    ngOnDestroy() {
        //this.saveToDisk();
    }

    addTypeMaterials(template: TemplateRef<any>) {
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

    delete(index: string) {
        this.alertService.showDialog('Are you sure you want to delete the task?', DialogType.confirm, () => this.deleteHelper(index));
    }

    private deleteHelper(index) {
        this.localService.delete(index).subscribe(result => this.deleteSuccessHelper(result), error => this.deleteFailedHelper(error));
    }

    private deleteSuccessHelper(value: Boolean) {
        this.getFromServer();
        this.alertService.showMessage("Success", `TypeMaterials was deleted successfully`, MessageSeverity.success);
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
            list => this.onDataLoadSuccessful(list),
            error => this.onDataLoadFailed(error),
            () => {
                disp.unsubscribe();
                setTimeout(() => { this.loadingIndicator = false; }, 1500);
            });
    }

    private onDataLoadSuccessful(resulted: Results<TypeMaterial>) {
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

    edit(template: TemplateRef<any>, index: string) {

        var disp = this.localService.get(index).subscribe(
            item => {
                //
                this.pointer.id = item.id;
                this.pointer.code = item.code;
                this.pointer.name = item.name;
                this.pointer.note = item.note;
                this.pointer.isDocument = item.isDocument;
                this.pointer.isLearning = item.isLearning;

                //
                this.modalRef = this.modalService.show(template);
            },
            error => {
            },
            () => { disp.unsubscribe(); });


    }

    close() {
        this.modalRef.hide();
    }
    private saveSuccessHelper(user?: TypeMaterial) {
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
