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
import { StudentsService } from "../../services/students.service";
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
import { Studentstatus } from "../../models/studentstatus.model";
import { GenderStudent } from "../../models/genderstudent.model";
import { Class } from "../../models/class.model";
import { AccessRightsService } from "../../services/access-rights.service";
import { Page } from '../../models/page.model';
import { Results } from '../../models/results.model';
import { ClassesService } from '../../services/classes.service';
import { ClassList } from '../../models/classlists.model';
@Component({
    selector: 'studentschedulelearning',
    templateUrl: './studentschedulelearning.component.html',
    styleUrls: ['./studentschedulelearning.component.css'],
    animations: [fadeInOut]
})

export class StudentScheduleLearningComponent implements OnInit, OnDestroy {

    rows = [];
    columns = [];
    loadingIndicator: boolean = true;
    isShowHeader: boolean = false;

    filterName: string;
    filterValue: string;

    public changesSavedCallback: () => void;
    public changesFailedCallback: () => void;
    public changesCancelledCallback: () => void;


    modalRef: BsModalRef;

    modalStudentRef: BsModalRef;
    private page: Page;
    constructor(private alertService: AlertService, private translationService: AppTranslationService,
        private localService: ClassesService, private modalService: BsModalService, public accessRightService: AccessRightsService,
        private typeservice: TypeMaterialsService) {
        this.filterValue = "";
        this.page = new Page();
        this.page.pageNumber = 0;
        this.page.size = 20;
    }

    setPage(pageInfo) {
        this.page.pageNumber = pageInfo.offset;
        this.getFromServer();
    }

    search() {
        this.getFromServer();
    }
    ngOnInit() {
        let gT = (key: string) => this.translationService.getTranslation(key);

        this.columns = [
            { headerClass: "text-center", prop: "ioNumber", name: gT('label.student.IONumber'), cellTemplate: this.statusTemplate },
            { headerClass: "text-center", prop: 'studentName', name: gT('label.student.StudentName'), cellTemplate: this.nameTemplate },
            { headerClass: "text-center", prop: 'materialName', name: gT('label.student.MaterialName'), cellTemplate: this.nameTemplate },
            { headerClass: "text-center", prop: 'startDate', name: gT('label.student.StartDate'), cellTemplate: this.typenameTemplate },
            { headerClass: "text-center", prop: 'endDate', name: gT('label.student.EndDate'), cellTemplate: this.descriptionTemplate },
            { headerClass: "text-center", prop: 'numberHourse', name: gT('label.student.NumberHourse'), cellTemplate: this.priceTemplate },
            { headerClass: "text-center", prop: 'totalPrice', name: gT('label.student.TotalPrice'), cellTemplate: this.priceTemplate },
            { headerClass: "text-center", prop: 'numberLearning', name: gT('label.student.NumberLearning'), cellTemplate: this.priceTemplate },
            { headerClass: "text-center", prop: 'totalPriceLearning', name: gT('label.student.TotalPriceLearning'), cellTemplate: this.priceTemplate },
            { headerClass: "text-center", prop: 'numberRemain', name: gT('label.student.NumberRemain'), cellTemplate: this.priceTemplate },
            { headerClass: "text-center", prop: 'totalRemain', name: gT('label.student.TotalPriceRemain'), cellTemplate: this.priceTemplate }
        ];

        //
        this.getFromServer();
    }

    ngOnDestroy() {
        //this.saveToDisk();
    }

    onSearchChanged(value: string) {
        this.filterValue = value;
        this.getFromServer();
    }

    private getFromServer() {
        this.loadingIndicator = true;
        //
        var disp = this.localService.getStudentMaterialDept(this.filterValue, this.page.pageNumber, this.page.size).subscribe(
            results => this.onDataLoadSuccessful(results),
            error => this.onDataLoadFailed(error),
            () => {
                disp.unsubscribe();
                setTimeout(() => { this.loadingIndicator = false; }, 1500);
            });

    }

    private onDataLoadSuccessful(resulted: Results<ClassList>) {
        this.page.totalElements = resulted.total;
        this.rows = resulted.list;
        this.alertService.stopLoadingMessage();
        this.loadingIndicator = false;
    }

    private onDataLoadFailed(error: any) {
        this.alertService.stopLoadingMessage();
        this.alertService.showStickyMessage("Load Error", `Unable to retrieve user data from the server.\r\nErrors: "${Utilities.getHttpResponseMessage(error)}"`,
            MessageSeverity.error, error);

    }

    close() {
        this.modalRef.hide();
    }


    @ViewChild('priceTemplate')
    priceTemplate: TemplateRef<any>;

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
