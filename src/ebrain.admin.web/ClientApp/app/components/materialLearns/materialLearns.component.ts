// ======================================
// Author: Ebrain Team
// Email:  johnpham@ymail.com
// Copyright (c) 2017 supperbrain.visualstudio.com
// 
// ==> Contact Us: supperbrain@outlook.com
// ======================================

import { Component, OnInit, OnDestroy, TemplateRef, ViewChild } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { fadeInOut } from '../../services/animations';
import { AppTranslationService } from "../../services/app-translation.service";
import { MaterialLearnsService } from "../../services/materialLearns.service";
import { AlertService, MessageSeverity, DialogType } from '../../services/alert.service';
import { Utilities } from '../../services/utilities';
import { MaterialLearn } from '../../models/MaterialLearn.model';
import { BsModalService } from 'ngx-bootstrap/modal';
import { BsModalRef } from 'ngx-bootstrap/modal/bs-modal-ref.service';
import { Unit } from "../../models/unit.model";
import { GrpMaterialLearn } from "../../models/grpMaterialLearn.model";
import { TypeMaterialLearn } from "../../models/typeMaterialLearn.model";
import { TypeMaterialLearnsService } from "../../services/typeMaterialLearns.service";

@Component({
    selector: 'materiallearns',
    templateUrl: './materialLearns.component.html',
    styleUrls: ['./materialLearns.component.css'],
    animations: [fadeInOut]
})

export class MaterialLearnsComponent implements OnInit, OnDestroy {
    rows = [];
    columns = [];
    loadingIndicator: boolean = true;

    filterName: string;
    filterValue: string;

    allgrps: GrpMaterialLearn[] = [];
    allTypes: TypeMaterialLearn[] = [];

    private pointer: MaterialLearn;

    public changesSavedCallback: () => void;
    public changesFailedCallback: () => void;
    public changesCancelledCallback: () => void;

    modalRef: BsModalRef;

    constructor(private alertService: AlertService, private translationService: AppTranslationService,
        private localService: MaterialLearnsService, private modalService: BsModalService,
        private typeservice: TypeMaterialLearnsService) {
        this.pointer = new MaterialLearn();
    }

    ngOnInit() {
        let gT = (key: string) => this.translationService.getTranslation(key);

        this.columns = [
            { headerClass: "text-center", prop: 'code', name: gT('label.materialLearn.Code'), cellTemplate: this.grpnameTemplate },
            //{ headerClass: "text-center", prop: "code", name: gT('label.materialLearn.Code'), width: 100, headerTemplate: this.statusHeaderTemplate, cellTemplate: this.statusTemplate, resizeable: false, canAutoResize: false, sortable: false, draggable: false },
            { headerClass: "text-center", prop: 'name', name: gT('label.materialLearn.Name'), cellTemplate: this.nameTemplate },
            { headerClass: "text-center", prop: 'typeName', name: gT('label.materialLearn.Learning'), cellTemplate: this.typenameTemplate },
            //{ headerClass: "text-center", prop: 'grpName', name: gT('label.materialLearn.LevelClass'), cellTemplate: this.grpnameTemplate },
            { headerClass: "text-center", prop: 'note', name: gT('label.materialLearn.Note'), cellTemplate: this.descriptionTemplate },
            { headerClass: "text-center", prop: 'id', name: '', width: 150, cellTemplate: this.actionsTemplate, resizeable: false, canAutoResize: false, sortable: false, draggable: false }
        ];

        //
        this.getFromServer();

        //
    }

    ngOnDestroy() {
        //this.saveToDisk();
    }

    addMaterialLearn(template: TemplateRef<any>) {
        this.pointer.id = "";
        this.addMaterialLearnMain(template);
    }

    addMaterialLearnMain(template: TemplateRef<any>) {
        this.getTypeMaterial();
        this.modalRef = this.modalService.show(template, { class: 'modal-lg' });
    }

    editMaterialLearn(template: TemplateRef<any>, index: string) {
        var disp = this.localService.get(index).subscribe(
            item => {
                //
                this.pointer.id = item.id;
                this.pointer.code = item.code;
                this.pointer.name = item.name;
                this.pointer.typeName = item.typeName;
                this.pointer.grpName = item.grpName;
                this.pointer.grpMaterialId = item.grpMaterialId;
                this.pointer.typeMaterialId = item.typeMaterialId;
                this.pointer.note = item.note;
                this.pointer.unitId = item.unitId;
                this.pointer.sellPrice = item.sellPrice;
                this.pointer.calBeCourse = item.calBeCourse;
                this.pointer.calEnCourse = item.calEnCourse;
                this.pointer.spBeCourse = item.spBeCourse;
                this.pointer.spEnCourse = item.spEnCourse;
                this.pointer.numberHourse = item.numberHourse;
                this.pointer.maskPassCourse = item.maskPassCourse;
                
                //show popup
                this.addMaterialLearnMain(template);
            },
            error => {
            },
            () => { disp.unsubscribe(); });
    }

    deleteMaterialLearn(index: string) {
        this.alertService.showDialog('Are you sure you want to delete this material lean?', DialogType.confirm, () => this.deleteHelper(index));
    }

    imageFinishedUploading(file: any) {
        console.log(JSON.stringify(file.serverResponse));
    }

    onUploadStateChanged(state: boolean) {
        console.log(JSON.stringify(state));
    }

    onSearchChanged(value: string) {
        //this.rows = this.rowsCache.filter(r => Utilities.searchArray(value, false, r.name, r.description) || value == 'important' && r.important || value == 'not important' && !r.important);
    }

    private getTypeMaterial() {
        return this.localService.getalltype().subscribe(results => this.onDataLoadSuccessfulTypeMaterial(results), error => this.onDataLoadFailed(error));
    }

    private onDataLoadSuccessfulTypeMaterial(types: TypeMaterialLearn[]) {
        if (types.length > 0) {
            var typeId = types[0].id;
            this.pointer.typeMaterialId = typeId;
            this.onChangeMaterialLearn(typeId);
        }
        this.allTypes = types;
        this.alertService.stopLoadingMessage();
    }


    private onChangeMaterialLearn(typeId: string) {
        this.localService.findGrpByTypeId(typeId).subscribe(results => this.onDataLoadSuccessfulChange(results), error => this.onDataLoadFailed(error));
    }

    private onDataLoadSuccessfulChange(grps: GrpMaterialLearn[]) {
        if (grps.length > 0) {
            this.pointer.grpMaterialId = grps[0].id;
        }
        this.allgrps = grps;
        this.allgrps = [...this.allgrps];
        this.alertService.stopLoadingMessage();
    }


    private deleteHelper(id: string) {
        this.localService.delete(id).subscribe(value => this.deleteSuccessHelper(value), error => this.deleteFailedHelper(error));
    }

    private deleteSuccessHelper(value: Boolean) {
        this.getFromServer();
        this.alertService.showMessage("Success", `MaterialLearn was deleted successfully`, MessageSeverity.success);
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
        var disp = this.localService.search(this.filterName, this.filterValue).subscribe(
            results => this.onDataLoadSuccessful(results),
            error => this.onDataLoadFailed(error),
            () => {
                disp.unsubscribe();
                setTimeout(() => { this.loadingIndicator = false; }, 1500);
            });
    }

    private onDataLoadSuccessful(materialLearns: MaterialLearn[]) {
        this.rows = materialLearns;
        this.alertService.stopLoadingMessage();
        //this.allTypes = types;
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

    private saveSuccessHelper(user?: MaterialLearn) {
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
}
