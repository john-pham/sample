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
import { Page } from "../../models/page.model";
import { Results } from "../../models/results.model";
@Component({
    selector: 'studentdates',
    templateUrl: './studentdates.component.html',
    styleUrls: ['./studentdates.component.css'],
    animations: [fadeInOut]
})

export class StudentDatesComponent implements OnInit, OnDestroy {

    fromDate: Date;
    toDate: Date;

    rows = [];
    columns = [];
    loadingIndicator: boolean = true;
    private page: Page;


    @Input() isAllStudent: boolean = false;

    constructor(private alertService: AlertService, private translationService: AppTranslationService,
        private localService: StudentsService, private modalService: BsModalService) {
        this.page = new Page();
        this.page.pageNumber = 0;
        this.page.size = 20;
    }

    setPage(pageInfo) {
        this.page.pageNumber = pageInfo.offset;
        this.search();
    }

    ngOnInit() {
        var date = new Date(), y = date.getFullYear(), m = date.getMonth();

        if (this.isAllStudent == false) {
            this.fromDate = date;
        }
        else {
            this.fromDate = new Date(1900, 1, 1);
        }
        this.toDate = new Date(y, m + 1, 0);

        let gT = (key: string) => this.translationService.getTranslation(key);

        this.columns = [
            { headerClass: "text-center", prop: "code", name: gT('label.student.Code'), width: 100, headerTemplate: this.statusHeaderTemplate, cellTemplate: this.statusTemplate, resizeable: false, canAutoResize: false, sortable: false, draggable: false },
            { headerClass: "text-center", prop: 'name', name: gT('label.student.Name'), cellTemplate: this.nameTemplate },
            { headerClass: "text-center", prop: 'phone', name: gT('label.student.Phone'), cellTemplate: this.typenameTemplate },
            { headerClass: "text-center", prop: 'birthday', name: gT('label.student.Birthday'), cellTemplate: this.typenameTemplate },
            { headerClass: "text-center", prop: 'email', name: gT('label.student.Email'), cellTemplate: this.grpnameTemplate },
            { headerClass: "text-center", prop: 'genderName', name: gT('label.student.Sex'), cellTemplate: this.grpnameTemplate }
        ];

        //
        this.search();
    }

    ngOnDestroy() {
        //this.saveToDisk();
    }

    private search() {
        this.loadingIndicator = true;
        var disp = this.localService.getstudentbycreatedate("", "", this.fromDate, this.toDate, this.page.pageNumber, this.page.size).subscribe(
            list => this.onDataLoadSuccessful(list),
            error => this.onDataLoadFailed(error),
            () => {
                disp.unsubscribe();
                setTimeout(() => { this.loadingIndicator = false; }, 1500);
            });


    }

    private onDataLoadSuccessful(resulted: Results<Student>) {
        this.page.totalElements = resulted.total;
        this.rows = resulted.list;
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
