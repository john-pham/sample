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
import { IOStock } from "../../models/iostock.model";
import { User } from "../../models/user.model";
import { Material } from "../../models/material.model";
import { IOStockDetail } from "../../models/iostockdetail.model";
import { AccessRightsService } from "../../services/access-rights.service";
import { Results } from "../../models/results.model";
import { PurchaseOrderService } from "../../services/purchaseorders.service";
import { PurchaseOrderReport } from "../../models/purchaseorderreport.model";
import { Page } from "../../models/page.model";
@Component({
    selector: 'ioinputs',
    templateUrl: './ioinputs.component.html',
    styleUrls: ['./ioinputs.component.css'],
    animations: [fadeInOut]
})

export class IOInputsComponent implements OnInit, OnDestroy {
    rows = [];
    columns = [];

    columnPurchases = [];
    rowPurchases = [];

    fromDate: Date;
    toDate: Date;

    @Input() ioStockId: any = false;
    @Input() isShowHeader: any = true;
    
    rowmaterials = [];
    columnmaterials = [];


    loadingIndicator: boolean = true;

    filterName: string;
    filterValue: string;

    filterPurchaseValue: string;

    allUsers: User[] = [];
    allstudents: Student[] = [];

    private pointer: IOStock;
    private isEditMode = true;

    public changesSavedCallback: () => void;
    public changesFailedCallback: () => void;
    public changesCancelledCallback: () => void;

    modalRef: BsModalRef;
    modalPurchaseRef: BsModalRef;
    private page: Page;

    constructor(private alertService: AlertService, private route: ActivatedRoute, private translationService: AppTranslationService,
        private localService: IOStudentsService, private modalService: BsModalService,
        public accessRightService: AccessRightsService,
        private purchaseService: PurchaseOrderService,
        private typeservice: TypeMaterialsService, private router: Router) {
        this.pointer = new IOStock();

        var date = new Date(), y = date.getFullYear(), m = date.getMonth();
        this.fromDate = new Date(y, m, 1);
        this.toDate = date;//new Date(y, m + 1, 0);
        this.filterPurchaseValue = "";
        this.page = new Page();
        this.page.pageNumber = 0;
        this.page.size = 20;
    }

    setPage(pageInfo) {
        this.page.pageNumber = pageInfo.offset;
        this.getPurchase();
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

        this.columnPurchases = [
            { headerClass: "text-center", prop: 'code', name: gT('label.purchaselist.Code'), cellTemplate: this.nameTemplate },
            { headerClass: "text-center", prop: 'createDate', name: gT('label.purchaselist.CreateDate'), cellTemplate: this.nameTemplate },
            { headerClass: "text-center", prop: 'fullName', name: gT('label.purchaselist.CreateUser'), cellTemplate: this.nameTemplate },
            { headerClass: "text-center", prop: 'branchName', name: gT('label.purchaselist.BranchName'), cellTemplate: this.nameTemplate },
            { headerClass: "text-center", prop: 'purchaseQuantity', width: 70, name: gT('label.purchaselist.PurchaseQuantity'), cellTemplate: this.totalPriceTemplate, cellClass: 'text-right' },
            { headerClass: "text-center", prop: 'ioQuantity', width: 70, name: gT('label.purchaselist.IOQuantity'), cellTemplate: this.totalPriceTemplate, cellClass: 'text-right' },
            { headerClass: "text-center", prop: 'remainQuantity', width: 70, name: gT('label.purchaselist.RemainQuantity'), cellTemplate: this.totalPriceTemplate, cellClass: 'text-right' }

        ];

        //
        this.getFromServer(false);

        //
    }

    ngOnDestroy() {
        //this.saveToDisk();
    }

    goPayment(ioid: string, template: TemplateRef<any>) {
        this.ioStockId = ioid;
        this.modalRef = this.modalService.show(template, { class: 'modal-large' });
    }

    addnew() {
        this.pointer = new IOStock();
        this.ioStockId = "";
        this.getFromServer(true);
        this.rows = [];
    }

    showmaterial(template: TemplateRef<any>) {
        if (this.pointer.purchaseOrderCode != null && this.pointer.purchaseOrderCode.length > 0) {
            this.alertService.showDialog('Đã tồn tại phiếu đặt hàng, bạn cần khởi tạo mới dữ liệu?', DialogType.confirm, () => {
                this.showmaterialMain(template);
            });
        } else {
            this.showmaterialMain(template);
        }
    }

    showmaterialMain(template: TemplateRef<any>) {
        this.getMaterial();
        this.modalRef = this.modalService.show(template, { class: 'modal-lg' });
    }

    showPurchase(template: TemplateRef<any>) {
        let id = this.pointer.id;

        if (id != null && id.length > 0) {
            this.alertService.showDialog('Phiếu đã tồn tại, bạn cần khởi tạo mới dữ liệu?', DialogType.confirm, () => {
                this.showPurchaseMain(template);
            });
        } else {
            this.showPurchaseMain(template);
        }
    }

    showPurchaseMain(template: TemplateRef<any>) {
        this.getPurchase();
        this.modalPurchaseRef = this.modalService.show(template, { class: 'modal-lg' });
    }

    onActivateMaterial(event) {
        if (event.type == 'dblclick') {
            var row = event.row;
            var iod = new IOStockDetail();

            iod.grpMaterial = row.grpName;
            iod.typeMaterial = row.typeName;
            iod.materialCode = row.code;
            iod.materialName = row.name;
            iod.quantity = 1;
            iod.sellPrice = row.sellPrice;
            iod.materialGrpId = row.grpMaterialId;
            iod.materialTypeId = row.typeMaterialId;
            iod.materialId = row.id;
            iod.totalPrice = 1 * row.sellPrice;
            this.rows.push(iod);
            this.rows = [...this.rows]
        }
    }

    onActivatePurchase(event) {
        if (event.type == 'dblclick') {
            var row = event.row;
            this.pointer.purchaseOrderCode = row.code;
            this.pointer.purchaseOrderId = row.id;
            this.getPurchasedetails(row.id);

        }
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


    onSearchChanged(value: string) {
        this.filterValue = value;
        this.getMaterial();
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

    onSearchPurchaseChanged(value: string) {
        this.filterPurchaseValue = value;
        this.getPurchase();
    }

    private getPurchase() {
        this.purchaseService.getpurchaseorders(this.filterName, this.filterPurchaseValue, this.fromDate, this.toDate, 1, this.page.pageNumber, this.page.size).subscribe(
            list => this.onDataLoadPurchaseSuccessful(list),
            error => this.onDataLoadFailed(error));
    }

    private getPurchasedetails(index) {
        this.purchaseService.getpurchasedetailsbyid(index).subscribe(
            list => this.onDataLoadPurchaseDetailsSuccessful(list),
            error => this.onDataLoadFailed(error));
    }

    private onDataLoadPurchaseDetailsSuccessful(resulted: IOStockDetail[]) {
        this.pointer.ioDetails = resulted;
        this.mappingHelper(this.pointer);
    }

    private onDataLoadPurchaseSuccessful(resulted: Results<PurchaseOrderReport>) {
        this.page.totalElements = resulted.total;
        this.rowPurchases = resulted.list;
        this.alertService.stopLoadingMessage();

    }

    private getdefault(isReset: boolean) {
        this.route.paramMap
            .switchMap((params: ParamMap) => {
                var id = '';
                if (isReset == false) {
                    if (this.ioStockId != null && this.ioStockId.length > 0) id = this.ioStockId
                    else id = params.get('id');
                }
                return this.localService.getdefault(id, true);
            })
            .subscribe(results => this.mappingHelper(results), error => this.onDataLoadFailed(error));
        this.alertService.stopLoadingMessage();
    }

    private onDataLoadSuccessfulDefault(io: IOStock) {
        this.pointer = io;
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
            error => this.onDataLoadFailed(error),
            () => {
                disp.unsubscribe();
                setTimeout(() => { this.loadingIndicator = false; }, 1500);
            });
        //return this.localService.getAll().subscribe(
        //    results => this.onDataLoadSuccessful(results[0]),
        //    error => this.onDataLoadFailed(error));
        this.getStudent();
        this.getdefault(isReset);
    }

    private onDataLoadSuccessful(ios: IOStock[]) {
        this.rows = ios;
        this.alertService.stopLoadingMessage();

    }

    private onDataLoadFailed(error: any) {
        this.alertService.stopLoadingMessage();
        this.alertService.showStickyMessage("Load Error", `Unable to retrieve user data from the server.\r\nErrors: "${Utilities.getHttpResponseMessage(error)}"`,
            MessageSeverity.error, error);

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

    private deletemasterSuccessHelper(io?: IOStock) {
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

    private cancelmasterSuccessHelper(io?: IOStock) {
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

    private mappingHelper(io?: IOStock) {
        this.pointer.id = io.id;
        this.pointer = io;

        this.rows = [];

        io.ioDetails.forEach(row => {
            var iod = new IOStockDetail();

            iod.grpMaterial = row.grpMaterial;
            iod.typeMaterial = row.typeMaterial;
            iod.materialCode = row.materialCode;
            iod.materialName = row.materialName;
            iod.quantity = row.quantity;
            iod.sellPrice = row.sellPrice;
            iod.materialGrpId = row.materialGrpId;
            iod.materialTypeId = row.materialTypeId;
            iod.materialId = row.materialId;
            iod.totalPrice = row.quantity * row.sellPrice;
            iod.id = row.id;
            iod.purchaseOrderDetailId = row.purchaseOrderDetailId;
            iod.purchaseOrderId = row.purchaseOrderId;

            this.rows.push(iod);
            this.rows = [...this.rows];
        });

        this.loadingIndicator = false;
    }

    private saveSuccessHelper(io?: IOStock) {
        this.mappingHelper(io);
        this.alertService.stopLoadingMessage();

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

    goDetails(id: string) {
        this.router.navigate(['/ioinput', id]);
    }
    private close() {
        this.modalRef.hide();
    }

    private closePurchaseOrder() {
        this.modalPurchaseRef.hide();
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
