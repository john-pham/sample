// ======================================
// Author: Ebrain Team
// Email:  johnpham@ymail.com
// Copyright (c) 2017 supperbrain.visualstudio.com
// 
// ==> Contact Us: supperbrain@outlook.com
// ======================================

import { Component, OnInit, OnDestroy, TemplateRef, ViewChild, Input } from '@angular/core';

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
import { DateOnlyPipe } from "../../directives/dateonlypipe.directive";
@Component({
    selector: 'studentlearning',
    templateUrl: './studentlearning.component.html',
    styleUrls: ['./studentlearning.component.css'],
    animations: [fadeInOut]
})

export class StudentLearningComponent implements OnInit, OnDestroy {

    rows = [];
    columns = [];
    classes = [];

    loadingIndicator: boolean = true;
    isShowHeader: boolean = false;

    filterName: string;
    filterValue: string = "";
    classId: string = "";
    studentId: string = "";

    public changesSavedCallback: () => void;
    public changesFailedCallback: () => void;
    public changesCancelledCallback: () => void;

    // 0: get all; 1: get learning; 2: get end class
    @Input() learning: any = "";

    modalRef: BsModalRef;

    modalStudentRef: BsModalRef;
    private page: Page;
    constructor(private alertService: AlertService, private translationService: AppTranslationService,
        private localService: StudentsService, private modalService: BsModalService, public accessRightService: AccessRightsService,
        private classService: ClassesService) {
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
        this.classService.search("", "", 0).subscribe(
            list => this.onDataLoadClassesSuccessful(list),
            error => this.onDataLoadFailed(error),);
    }

    ngOnInit() {
        let gT = (key: string) => this.translationService.getTranslation(key);

        this.columns = [
            { headerClass: "text-center", prop: "code", name: gT('label.student.Code'), cellTemplate: this.statusTemplate },
            { headerClass: "text-center", prop: 'name', name: gT('label.student.Name'), cellTemplate: this.nameTemplate },
            { headerClass: "text-center", prop: 'className', name: gT('label.student.ClassName'), cellTemplate: this.typenameTemplate },
            { headerClass: "text-center", prop: 'materialName', name: gT('label.student.MaterialName'), cellTemplate: this.typenameTemplate },
            { headerClass: "text-center", prop: 'startDate', name: gT('label.student.StartDate'), cellTemplate: this.descriptionTemplate, pipe: new DateOnlyPipe('en-US'), cellClass: 'text-right' },
            { headerClass: "text-center", prop: 'endDate', name: gT('label.student.EndClass'), cellTemplate: this.descriptionTemplate, pipe: new DateOnlyPipe('en-US'), cellClass: 'text-right' },
            { name: '', width: 200, cellTemplate: this.actionsTemplate, resizeable: false, canAutoResize: false, sortable: false, draggable: false }
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

    private onChangeClass(classId: string) {
        this.classId = classId;
        this.search();
    }

    private getFromServer() {
        this.loadingIndicator = true;
        //
        var disp = this.localService.getStudentLearning(
            this.filterValue, this.studentId, this.classId, this.learning, this.page.pageNumber, this.page.size).subscribe(
                results => this.onDataLoadSuccessful(results),
                error => this.onDataLoadFailed(error),
                () => {
                    disp.unsubscribe();
                    setTimeout(() => { this.loadingIndicator = false; }, 1500);
                });

    }

    private onDataLoadClassesSuccessful(list: Class[]) {
        this.classId = "";
        this.classes = list;
        this.alertService.stopLoadingMessage();
        this.loadingIndicator = false;
        this.getFromServer();
    }

    private onDataLoadSuccessful(resulted: Results<Student>) {
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

    onActivateClass(event, template: any) {
        if (event != null && event.type == 'dblclick') {
            this.studentId = event.row.id;
            this.classId = event.row.classId;
            this.modalRef = this.modalService.show(template, { class: 'modal-lg' });
        }
    }

    close() {
        this.modalRef.hide();
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
