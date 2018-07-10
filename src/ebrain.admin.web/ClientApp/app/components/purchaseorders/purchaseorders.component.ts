// ======================================
// Author: Ebrain Team
// Email:  johnpham@ymail.com
// Copyright (c) 2017 supperbrain.visualstudio.com
// 
// ==> Contact Us: supperbrain@outlook.com
// ======================================

import { Component, OnInit, OnDestroy, TemplateRef, ViewChild, Input } from '@angular/core';
import { ActivatedRoute, ParamMap } from '@angular/router';
import { Router } from '@angular/router';

import { Observable } from 'rxjs/Observable';
import { fadeInOut } from '../../services/animations';
import { AppTranslationService } from "../../services/app-translation.service";
import { IOStudentsService } from "../../services/iostudents.service";
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
import { Student } from "../../models/student.model";
import { GrpMaterial } from "../../models/grpMaterial.model";
import { Supplier } from "../../models/supplier.model";
import { PurchaseOrder } from "../../models/PurchaseOrder.model";
import { User } from "../../models/user.model";
import { Material } from "../../models/material.model";
import { PurchaseOrderDetail } from "../../models/PurchaseOrderdetail.model";
import { AccessRightsService } from "../../services/access-rights.service";
import { Results } from "../../models/results.model";
import { PurchaseOrderService } from "../../services/purchaseorders.service";
@Component({
    selector: 'purchaseorders',
    templateUrl: './purchaseorders.component.html',
    styleUrls: ['./purchaseorders.component.css'],
    animations: [fadeInOut]
})

export class PurchaseOrdersComponent implements OnInit, OnDestroy {
    rows = [];
    columns = [];

    rowmaterials = [];
    columnmaterials = [];

    @Input() purchaseOrderId: any = "";
    @Input() isShowHeader: any = true;
    
    loadingIndicator: boolean = true;

    filterName: string;
    filterValue: string;

    allGrps: GrpMaterial[] = [];
    allTypes: TypeMaterial[] = [];
    allUnits: Unit[] = [];
    allSups: Supplier[] = [];
    allUsers: User[] = [];
    allstudents: Student[] = [];

    private pointer: PurchaseOrder;
    private isEditMode = true;

    public changesSavedCallback: () => void;
    public changesFailedCallback: () => void;
    public changesCancelledCallback: () => void;

    modalRef: BsModalRef;

    constructor(private alertService: AlertService, private route: ActivatedRoute, private translationService: AppTranslationService,
        private localService: PurchaseOrderService, private modalService: BsModalService,
        public accessRightService: AccessRightsService,
        private typeservice: TypeMaterialsService, private router: Router) {
        this.pointer = new PurchaseOrder();
    }

    ngOnInit() {
        let gT = (key: string) => this.translationService.getTranslation(key);

        this.columnmaterials = [
            { headerClass: "text-center", prop: "code", name: gT('label.material.Code'), width: 100, headerTemplate: this.statusHeaderTemplate, cellTemplate: this.statusTemplate, resizeable: false, canAutoResize: false, sortable: false, draggable: false },
            { headerClass: "text-center", prop: 'name', name: gT('label.material.Name'), cellTemplate: this.nameTemplate },
            { headerClass: "text-center", prop: 'typeName', name: gT('label.material.TypeMaterial'), cellTemplate: this.typenameTemplate },
            { headerClass: "text-center", prop: 'grpName', name: gT('label.material.GrpMaterial'), cellTemplate: this.grpnameTemplate },
            { headerClass: "text-center", prop: 'sellPrice', name: gT('label.material.SellPrice'), cellTemplate: this.priceMaterialTemplate, cellClass: 'text-right' },
            { headerClass: "text-center", prop: 'note', name: gT('label.material.Note'), cellTemplate: this.descriptionTemplate }
        ];

        this.columns = [
            { headerClass: "text-center", prop: 'typeMaterial', name: gT('label.iostudent.TypeMaterial'), cellTemplate: this.typenameTemplate },
            { headerClass: "text-center", prop: 'grpMaterial', name: gT('label.iostudent.GrpMaterial'), cellTemplate: this.grpnameTemplate },

            { headerClass: "text-center", prop: "materialCode", name: gT('label.iostudent.MaterialCode'), width: 100, headerTemplate: this.statusHeaderTemplate, cellTemplate: this.statusTemplate, resizeable: false, canAutoResize: false, sortable: false, draggable: false },
            { headerClass: "text-center", prop: 'materialName', name: gT('label.iostudent.MaterialName'), cellTemplate: this.nameTemplate },

            { headerClass: "text-center", prop: 'quantity', name: gT('label.iostudent.Quantity'), cellTemplate: this.quantityTemplate, cellClass: 'text-right' },
            { headerClass: "text-center", prop: 'sellPrice', name: gT('label.iostudent.Price'), cellTemplate: this.priceTemplate, cellClass: 'text-right' },
            { headerClass: "text-center", prop: 'totalPrice', name: gT('label.iostudent.TotalPrice'), cellTemplate: this.totalPriceTemplate, cellClass: 'text-right' },
            { headerClass: "text-center", prop: 'note', name: gT('label.iostudent.Note'), cellTemplate: this.descriptionTemplate },
            { name: '', width: 150, cellTemplate: this.actionsTemplate, resizeable: false, canAutoResize: false, sortable: false, draggable: false }
        ];

        //
        this.getFromServer(false);

        //
    }

    ngOnDestroy() {
        //this.saveToDisk();
    }

    addnew() {
        this.pointer = new PurchaseOrder();
        this.getFromServer(true)
        this.rows = [];
    }

    showmaterial(template: TemplateRef<any>) {
        this.getMaterial();
        this.modalRef = this.modalService.show(template, { class: 'modal-lg' });
    }

    onActivateMaterial(event) {
        if (event.type == 'dblclick') {
            var row = event.row;
            var iod = new PurchaseOrderDetail();

            iod.grpMaterial = row.grpName;
            iod.typeMaterial = row.typeName;
            iod.materialCode = row.code;
            iod.materialName = row.name;
            iod.quantity = 1;
            iod.sellPrice = row.sellPrice;
            iod.materialGrpId = row.grpMaterialId;
            iod.materialTypeId = row.typeMaterialId;
            iod.materialid = row.id;
            iod.totalPrice = 1 * row.sellPrice;
            this.rows.push(iod);
            this.rows = [...this.rows]
        }
    }

    goPayment(ioid: string) {
        this.router.navigate(['/paymentio', ioid]);
    }

    updateValue(row, event, rowIndex) {
        row.quantity = event.target.value;
        this.refreshValueRow(row, rowIndex);
    }

    refreshValueRow(row, rowIndex) {
        row.totalPrice = row.quantity * row.sellPrice;
        let rows = [...this.rows];
        rows[rowIndex] = row;

        this.rows = [...this.rows]
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
        this.alertService.showDialog('Are you sure you want to delete the row?', DialogType.confirm, () => this.deleteHelper(row));
    }

    private deleteHelper(row) {
        this.rows = this.rows.filter(obj => obj !== row);
        this.rows = [...this.rows];
    }

    private deleteSuccessHelper(row: Student) {
        this.getFromServer(false);
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

    private getMaterial() {
        //load user
        this.localService.getMaterial(this.filterName, this.filterValue).subscribe(results => this.onDataLoadSuccessfulMaterial(results), error => this.onDataLoadFailed(error));

    }

    private getdefault(isReset: boolean) {
        //load user
        //this.localService.getdefault("", false).subscribe(results => this.onDataLoadSuccessfulDefault(results), error => this.onDataLoadFailed(error));
        this.route.paramMap
            .switchMap((params: ParamMap) => {
                var id = '';
                if (isReset == false) {
                    if (this.purchaseOrderId != null && this.purchaseOrderId.length > 0) id = this.purchaseOrderId
                    else id = params.get('id');
                }
                return this.localService.getdefault(id, false);
            })
            .subscribe(results => this.mappingHelper(results), error => this.onDataLoadFailed(error));

        this.alertService.stopLoadingMessage();
    }

    private onDataLoadSuccessfulDefault(io: PurchaseOrder) {
        this.pointer = io;
        this.loadingIndicator = false;
    }

    private getStudent() {
        //load user
        this.localService.getStudent().subscribe(results => this.onDataLoadSuccessfulStudents(results), error => this.onDataLoadFailed(error));
    }

    private onDataLoadSuccessfulStudents(stu: Student[]) {
        if (stu.length > 0) {
            this.pointer.studentId = stu[0].id;
        }
        this.allstudents = stu;
    }

    private onDataLoadSuccessfulMaterial(resulted: Results<Material>) {
        this.rowmaterials = resulted.list;

    }

    private getFromServer(isReset: boolean) {
        //load user
        this.loadingIndicator = true;
        //
        var disp = this.localService.getUsers(-1, -1).subscribe(
            results => this.allUsers = results,
            error => this.onDataLoadFailed(error));
        //return this.localService.getAll().subscribe(
        //    results => this.onDataLoadSuccessful(results[0]),
        //    error => this.onDataLoadFailed(error));
        this.getStudent();
        this.getdefault(isReset);
    }

    private onDataLoadSuccessful(ios: PurchaseOrder[]) {
        this.rows = ios;
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
        this.allGrps = grps;
        this.alertService.stopLoadingMessage();
    }

    private save() {
        if (this.rows != null && this.rows.length > 0) {
            this.alertService.startLoadingMessage("Saving changes...");
            this.pointer.ioDetails = this.rows;
            this.localService.save(this.pointer).subscribe(value => this.saveSuccessHelper(value), error => this.saveFailedHelper(error));
        }
        else {
            this.showErrorAlert("Input details", "Please selected one item on grids.");
        }
    }

    private deletemaster() {
        this.alertService.showDialog('Are you sure you want to delete?', DialogType.confirm, () => {
            this.alertService.startLoadingMessage("Delete changes...");
            this.pointer.ioDetails = this.rows;
            this.localService.deletemaster(this.pointer.id).subscribe(value => this.deletemasterSuccessHelper(value), error => this.saveFailedHelper(error));
        });
    }

    private deletemasterSuccessHelper(io?: PurchaseOrder) {
        this.pointer = io;
        this.rows = [];
        this.alertService.stopLoadingMessage();
    }

    private cancelmaster() {
        this.alertService.showDialog('Are you sure you want to cancel?', DialogType.confirm, () => {
            this.alertService.startLoadingMessage("Cancel changes...");
            this.pointer.ioDetails = this.rows;
            this.localService.cancelmaster(this.pointer.id).subscribe(value => this.cancelmasterSuccessHelper(value), error => this.saveFailedHelper(error));
        });

    }

    private cancelmasterSuccessHelper(io?: PurchaseOrder) {
        this.pointer = io;
        this.rows = [];
        this.alertService.stopLoadingMessage();
    }

    edit(template: TemplateRef<any>, index: string) {

        var disp = this.localService.get(index).subscribe(
            item => {
                //
                this.pointer = item;

                this.modalRef = this.modalService.show(template, { class: 'modal-lg' });
            },
            error => {
            },
            () => { disp.unsubscribe(); });


    }

    private mappingHelper(io?: PurchaseOrder) {
        this.pointer.id = io.id;
        this.pointer = io;

        this.rows = [];

        io.ioDetails.forEach(row => {
            var iod = new PurchaseOrderDetail();

            iod.grpMaterial = row.grpMaterial;
            iod.typeMaterial = row.typeMaterial;
            iod.materialCode = row.materialCode;
            iod.materialName = row.materialName;
            iod.quantity = row.quantity;
            iod.sellPrice = row.sellPrice;
            iod.materialGrpId = row.materialGrpId;
            iod.materialTypeId = row.materialTypeId;
            iod.materialid = row.id;
            iod.totalPrice = row.quantity * row.sellPrice;
            iod.id = row.id;

            this.rows.push(iod);
            this.rows = [...this.rows];
        });
        this.loadingIndicator = false;
    }

    private saveSuccessHelper(io?: PurchaseOrder) {
        this.mappingHelper(io);

        //if (this.isNewUser)
        this.alertService.showMessage("Success", `User \"${this.pointer.name}\" was created successfully`, MessageSeverity.success);
        //else if (!this.isEditingSelf)
        //    this.alertService.showMessage("Success", `Changes to user \"${this.pointer.Name}\" was saved successfully`, MessageSeverity.success);

        if (this.changesSavedCallback)
            this.changesSavedCallback();
        this.alertService.stopLoadingMessage();
    }


    private saveFailedHelper(error: any) {
        this.alertService.stopLoadingMessage();
        this.alertService.showStickyMessage("Superbrain thông báo", "The below errors occured whilst saving your changes:", MessageSeverity.error, error);
        this.alertService.showStickyMessage(error, null, MessageSeverity.error);

        if (this.changesFailedCallback)
            this.changesFailedCallback();
    }

    private close() {
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

    @ViewChild('quantityTemplate')
    quantityTemplate: TemplateRef<any>;

    @ViewChild('totalPriceTemplate')
    totalPriceTemplate: TemplateRef<any>;

    @ViewChild('priceMaterialTemplate')
    priceMaterialTemplate: TemplateRef<any>;

}
