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
import { ClassesService } from "../../services/classes.service";

@Component({
    selector: 'studentendclass',
    templateUrl: './studentendclass.component.html',
    styleUrls: ['./studentendclass.component.css'],
    animations: [fadeInOut]
})

export class StudentEndClassComponent implements OnInit, OnDestroy {

    classId: any;
    toDate: Date;

    rows = [];
    columns = [];
    classes = [];
    loadingIndicator: boolean = true;

    constructor(private alertService: AlertService, private translationService: AppTranslationService,
        private localService: StudentsService, private modalService: BsModalService, private classService: ClassesService) {

    }

    ngOnInit() {
        var date = new Date(), y = date.getFullYear(), m = date.getMonth();
        this.toDate = new Date(y, m + 1, 0);

        let gT = (key: string) => this.translationService.getTranslation(key);

        this.columns = [
            { headerClass: "text-center", prop: "code", name: gT('label.student.Code'), width: 100, headerTemplate: this.statusHeaderTemplate, cellTemplate: this.statusTemplate, resizeable: false, canAutoResize: false, sortable: false, draggable: false },
            { headerClass: "text-center", prop: 'name', name: gT('label.student.Name'), cellTemplate: this.nameTemplate },
            { headerClass: "text-center", prop: 'phone', name: gT('label.student.Phone'), cellTemplate: this.typenameTemplate },
            { headerClass: "text-center", prop: 'birthday', name: gT('label.student.Birthday'), cellTemplate: this.typenameTemplate },
            { headerClass: "text-center", prop: 'email', name: gT('label.student.Email'), cellTemplate: this.grpnameTemplate },
            { headerClass: "text-center", prop: 'genderName', name: gT('label.student.Sex'), cellTemplate: this.grpnameTemplate },
            { headerClass: "text-center", prop: 'totalDay', name: gT('label.student.TotalDay'), cellTemplate: this.descriptionTemplate, cellClass: 'text-right' }
        ];
        
        this.search();
    }

    ngOnDestroy() {
        //this.saveToDisk();
    }

    private search() {
        this.loadingIndicator = true;
        var disp = this.localService.getStudentEndClass(this.classId, this.toDate).subscribe(
            list => this.onDataLoadSuccessful(list),
            error => this.onDataLoadFailed(error),
            () => {
                disp.unsubscribe();
                setTimeout(() => { this.loadingIndicator = false; }, 1500);
            });

        this.classService.search("","").subscribe(
            list => this.onClassLoadSuccessful(list),
            error => this.onDataLoadFailed(error),
            () => {
                disp.unsubscribe();
                setTimeout(() => { this.loadingIndicator = false; }, 1500);
            });

    }

    private onClassLoadSuccessful(list: Class[]) {
        this.classes = list;
        this.alertService.stopLoadingMessage();
    }

    private onDataLoadSuccessful(list: Student[]) {
        this.rows = list;
        this.alertService.stopLoadingMessage();
    }

    private onDataLoadFailed(error: any) {
        this.alertService.stopLoadingMessage();
        this.alertService.showStickyMessage("Load Error", `Unable to retrieve user data from the server.\r\nErrors: "${Utilities.getHttpResponseMessage(error)}"`,
            MessageSeverity.error, error);

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
