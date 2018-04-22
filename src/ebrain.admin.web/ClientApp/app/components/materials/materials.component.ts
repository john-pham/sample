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
import { MaterialsService } from "../../services/materials.service";
import { AlertService, MessageSeverity, DialogType } from '../../services/alert.service';
import { Utilities } from '../../services/utilities';
import { MaterialLearn } from '../../models/MaterialLearn.model';
import { BsModalService } from 'ngx-bootstrap/modal';
import { BsModalRef } from 'ngx-bootstrap/modal/bs-modal-ref.service';
import { Unit } from "../../models/unit.model";
import { GrpMaterialLearn } from "../../models/grpMaterialLearn.model";
import { TypeMaterialLearn } from "../../models/typeMaterialLearn.model";
import { TypeMaterialLearnsService } from "../../services/typeMaterialLearns.service";
import { TypeMaterialsService } from "../../services/typeMaterials.service";
import { TypeMaterial } from "../../models/typeMaterial.model";
import { Material } from "../../models/material.model";
import { GrpMaterial } from "../../models/grpMaterial.model";
import { Supplier } from "../../models/supplier.model";
import { AccessRightsService } from "../../services/access-rights.service";
@Component({
    selector: 'materials',
    templateUrl: './materials.component.html',
    styleUrls: ['./materials.component.css'],
    animations: [fadeInOut]
})

export class MaterialsComponent implements OnInit, OnDestroy {
    rows = [];
    columns = [];
    loadingIndicator: boolean = true;

    filterName: string;
    filterValue: string;

    allGrps: GrpMaterial[] = [];
    allTypes: TypeMaterial[] = [];
    allUnits: Unit[] = [];
    allSups: Supplier[] = [];

    private pointer: Material;

    public changesSavedCallback: () => void;
    public changesFailedCallback: () => void;
    public changesCancelledCallback: () => void;

    modalRef: BsModalRef;

    constructor(private alertService: AlertService, private translationService: AppTranslationService,
        private localService: MaterialsService, private modalService: BsModalService,
        public accessRightService: AccessRightsService,
        private typeservice: TypeMaterialsService) {
        this.pointer = new Material();
    }

    ngOnInit() {
        let gT = (key: string) => this.translationService.getTranslation(key);

        this.columns = [
            { headerClass: "text-center", prop: "code", name: gT('label.material.Code'), width: 100, headerTemplate: this.statusHeaderTemplate, cellTemplate: this.statusTemplate, resizeable: false, canAutoResize: false, sortable: false, draggable: false },
            { headerClass: "text-center", prop: 'name', name: gT('label.material.Name'), cellTemplate: this.nameTemplate },
            { headerClass: "text-center", prop: 'typeName', name: gT('label.material.TypeMaterial'), cellTemplate: this.typenameTemplate },
            { headerClass: "text-center", prop: 'grpName', name: gT('label.material.GrpMaterial'), cellTemplate: this.grpnameTemplate },
            { headerClass: "text-center", prop: 'sellPrice', name: gT('label.material.SellPrice'), cellTemplate: this.priceTemplate, cellClass: 'text-right' },
            { headerClass: "text-center", prop: 'note', name: gT('label.material.Note'), cellTemplate: this.descriptionTemplate },
            { name: '', width: 150, cellTemplate: this.actionsTemplate, resizeable: false, canAutoResize: false, sortable: false, draggable: false }
        ];

        //
        this.getFromServer();

        //
    }

    ngOnDestroy() {
        //this.saveToDisk();
    }

    addMaterial(template: TemplateRef<any>) {
        this.pointer.id = "";
        this.addMaterialMain(template);
    }

    addMaterialMain(template: TemplateRef<any>) {
        this.getType();
        this.getUnit();
        this.getSupplier();
        this.modalRef = this.modalService.show(template, { class: 'modal-lg' });
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
        //this.rows = this.rowsCache.filter(r => Utilities.searchArray(value, false, r.name, r.description) || value == 'important' && r.important || value == 'not important' && !r.important);
    }

    delete(row) {
        this.alertService.showDialog('Are you sure you want to delete the task?', DialogType.confirm, () => this.deleteHelper(row));
    }

    private deleteHelper(row) {
        this.localService.delete(row.id).subscribe(value => this.deleteSuccessHelper(row), error => this.deleteFailedHelper(error));
    }

    private deleteSuccessHelper(row: MaterialLearn) {
        this.getFromServer();
        this.alertService.showMessage("Success", `MaterialLearn \"${row.name}\" was deleted successfully`, MessageSeverity.success);
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

    private getUnit() {
        return this.localService.getUnit().subscribe(results => this.onDataLoadSuccessfulUnit(results), error => this.onDataLoadFailed(error));
    }

    private onDataLoadSuccessfulUnit(units: Unit[]) {
        if (units.length > 0) {
            this.pointer.unitId = units[0].id;
        }
        this.allUnits = units;
        this.allUnits = [...this.allUnits];
        this.alertService.stopLoadingMessage();
        //this.allTypes = types;
    }

    private getType() {
        return this.localService.getTypeMaterial().subscribe(results => this.onDataLoadSuccessfulTypeMaterial(results), error => this.onDataLoadFailed(error));
    }

    private onDataLoadSuccessfulTypeMaterial(types: TypeMaterial[]) {
        if (types.length > 0) {
            var typeId = types[0].id;
            this.pointer.typeMaterialId = typeId;
            this.onChangeMaterial(typeId);
        }
        this.allTypes = types;
        this.allTypes = [...this.allTypes];
        this.alertService.stopLoadingMessage();

    }

    private getSupplier() {
        return this.localService.getSupplier().subscribe(results => this.onDataLoadSuccessfulSupplier(results), error => this.onDataLoadFailed(error));
    }

    private onDataLoadSuccessfulSupplier(suppliers: Supplier[]) {
        if (suppliers.length > 0) {
            this.pointer.supplierId = suppliers[0].id;
        }
        this.allSups = suppliers;
        this.alertService.stopLoadingMessage();

    }

    private onDataLoadSuccessful(materialLearns: Material[]) {
        this.rows = materialLearns;
        this.alertService.stopLoadingMessage();

    }

    private onDataLoadFailed(error: any) {
        this.alertService.stopLoadingMessage();
        this.alertService.showStickyMessage("Load Error", `Unable to retrieve user data from the server.\r\nErrors: "${Utilities.getHttpResponseMessage(error)}"`,
            MessageSeverity.error, error);

    }

    private onChangeMaterial(typeId: string) {
        this.localService.findGrpByTypeId(typeId).subscribe(results => this.onDataLoadSuccessfulChange(results), error => this.onDataLoadFailed(error));
    }

    private onDataLoadSuccessfulChange(grps: GrpMaterial[]) {
        if (grps.length > 0) {
            this.pointer.grpMaterialId = grps[0].id;
        }
        this.allGrps = grps;
        this.allGrps = [...this.allGrps];
        this.alertService.stopLoadingMessage();
    }

    private save() {
        this.alertService.startLoadingMessage("Saving changes...");

        this.localService.save(this.pointer).subscribe(value => this.saveSuccessHelper(value), error => this.saveFailedHelper(error));
    }

    private saveSuccessHelper(user?: Material) {
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

    s

    editMaterial(template: TemplateRef<any>, index: string) {

        var disp = this.localService.get(index).subscribe(
            item => {
                //
                this.pointer.id = item.id;
                this.pointer = item;

                //show popup
                this.addMaterialMain(template);
            },
            error => {
            },
            () => { disp.unsubscribe(); });
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

    @ViewChild('priceTemplate')
    priceTemplate: TemplateRef<any>;

}
