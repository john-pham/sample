// ======================================
// Author: Ebrain Team
// Email:  johnpham@ymail.com
// Copyright (c) 2017 supperbrain.visualstudio.com
// 
// ==> Contact Us: supperbrain@outlook.com
// ======================================

import { Component, OnInit, OnDestroy, TemplateRef, ViewChild, Input } from '@angular/core';
import { Router } from '@angular/router';
import { ActivatedRoute, ParamMap } from '@angular/router';

import { fadeInOut } from '../../services/animations';
import { AppTranslationService } from "../../services/app-translation.service";
import { ClassesService } from "../../services/classes.service";
import { AlertService, MessageSeverity, DialogType } from '../../services/alert.service';
import { Utilities } from '../../services/utilities';
import { Class } from '../../models/Class.model';
import { BsModalService } from 'ngx-bootstrap/modal';
import { BsModalRef } from 'ngx-bootstrap/modal/bs-modal-ref.service';
import { MaterialLearnsService } from "../../services/materialLearns.service";
import { MaterialLearn } from "../../models/MaterialLearn.model";
import { ClassStatusService } from "../../services/classstatus.service";
import { ClassStatus } from "../../models/classstatus.model";
import { Student } from "../../models/student.model";
import { IOStudentsService } from "../../services/iostudents.service";
import { SuppliersService } from "../../services/suppliers.service";
import { Supplier } from "../../models/supplier.model";
import { Today } from "../../models/today.model";
import { TodayService } from "../../services/today.service";
import { RoomsService } from "../../services/rooms.service";
import { Room } from "../../models/room.model";
import { StudentsService } from "../../services/students.service";
import { ClassTime } from "../../models/classtime.model";
import { ClassStudent } from "../../models/classstudent.model";
import { ClassList } from "../../models/classlists.model";
import { ClassExamine } from "../../models/classexamine.model";

@Component({
    selector: 'classexamines',
    templateUrl: './classexamines.component.html',
    styleUrls: ['./classexamines.component.css'],
    animations: [fadeInOut]
})

export class ClassExamineComponent implements OnInit, OnDestroy {

    loadingIndicator: boolean = true;
    isEditMode: boolean = true;

    public pointer: Class;
    private columns = [];
    private rows = [];
    private classes = [];

    studentId: string;
    classId: string;

    @Input()
    set ClassId(id: string) {
        this.classId = (id && id.trim()) || null;
    }

    get ClassId() {
        return this.classId;
    }

    @Input()
    set StudentId(id: string) {
        this.studentId = (id && id.trim()) || null;
    }

    get StudentId() {
        return this.studentId;
    }

    modalRef: BsModalRef;

    constructor(private alertService: AlertService, private translationService: AppTranslationService,
        private localService: ClassesService, private modalService: BsModalService,
        private materialService: MaterialLearnsService, private classStatusService: ClassStatusService,
        private studentService: StudentsService, private supplierService: SuppliersService,
        private todayService: TodayService, private roomService: RoomsService, private route: ActivatedRoute, private router: Router) {

        this.pointer = new Class();

    }

    ngOnInit() {
        let gT = (key: string) => this.translationService.getTranslation(key);

        this.columns = [
            { prop: "examineCode", name: gT('label.examine.Code'), cellTemplate: this.nameTemplate },
            { prop: 'examineName', name: gT('label.examine.Name'), cellTemplate: this.nameTemplate },
            { prop: 'mark', name: gT('label.examine.Mark'), cellTemplate: this.markTemplate },
            { name: '', width: 150, cellTemplate: this.actionsTemplate, resizeable: false, canAutoResize: false, sortable: false, draggable: false }
        ];

        //
        this.getFromServer();


    }

    private getFromServer() {
        //get classes
        var disp = this.localService.getFirstClass(this.classId).subscribe(
            item => this.onDataLoadClassSuccessful(item),
            error => this.onDataLoadFailed(error),
            () => {
                disp.unsubscribe();
                setTimeout(() => { this.loadingIndicator = false; }, 1500);
            });

        //get students
        this.studentService.get(this.studentId).subscribe(
            item => {
                if (item != null) {
                    this.pointer.studentName = item.name;
                    this.pointer.studentBirthday = item.birthday;
                    this.pointer.studentAddress = item.address;
                }
            },
            error => {
            },
            () => {
                disp.unsubscribe();
                setTimeout(() => { this.loadingIndicator = false; }, 1500);
            });


        //get class Examine
        this.getClassExamine();
    }

    private getClassExamine() {
        //get classExamines
        var disp = this.localService.getClassExamines(this.classId, this.studentId).subscribe(
            list => this.onDataLoadSuccessful(list),
            error => this.onDataLoadFailed(error),
            () => {
                disp.unsubscribe();
                setTimeout(() => { this.loadingIndicator = false; }, 1500);
            });
    }


    private deleteClasses(row) {
        this.alertService.showDialog('Are you sure you want to delete the row?', DialogType.confirm, () => this.deleteTimeHelper(row));
    }

    private deleteTimeHelper(row) {
        this.rows = this.rows.filter(obj => obj !== row);
        this.rows = [...this.rows];
    }

    private onDataLoadAllClassStudentSuccessful(items: ClassList[]) {
        this.rows = items;
        this.rows = [...this.rows];
        this.alertService.stopLoadingMessage();
    }

    private onDataLoadClassSuccessful(item: Class) {
        this.pointer.code = item.code;
        this.pointer.name = item.name;
        this.pointer.id = item.id;
        this.pointer.materialName = item.materialName;
        this.pointer.maxStudent = item.maxStudent;
        this.pointer.countStudent = item.countStudent;
        this.alertService.stopLoadingMessage();
    }

    private onDataLoadSuccessful(list: ClassExamine[]) {
        this.rows = list;
        this.alertService.stopLoadingMessage();
    }

    private onDataLoadFailed(error: any) {
        this.alertService.stopLoadingMessage();
        this.alertService.showStickyMessage("Load Error", `Unable to retrieve user data from the server.\r\nErrors: "${Utilities.getHttpResponseMessage(error)}"`,
            MessageSeverity.error, error);

    }

    private save() {
        var cls = [];
        this.rows.forEach(row => {
            var itemNew = new ClassExamine();
            itemNew.classId = this.classId;
            itemNew.studentId = this.studentId;
            itemNew.examineCode = row.examineCode;
            itemNew.examineName = row.examineName;
            itemNew.examineId = row.examineId;
            itemNew.mark = row.mark;
            cls.push(itemNew);
        });

        var disp = this.localService.saveExamine(cls).subscribe(
            items => {
                this.alertService.showMessage("Success", `Class \"${this.pointer.name}\" was saved successfully`, MessageSeverity.success);
                this.getClassExamine();
            },
            error => this.onDataLoadFailed(error),
            () => {
                disp.unsubscribe();
                setTimeout(() => { this.loadingIndicator = false; }, 1500);
            });
    }

    updateValue(row, event, rowIndex) {
        row.mark = event.target.value;
    }

    ngOnDestroy() {
        //this.saveToDisk();
    }

    close() {
        this.modalRef.hide();
    }

    @ViewChild('markTemplate')
    markTemplate: TemplateRef<any>;

    @ViewChild('statusHeaderTemplate')
    statusHeaderTemplate: TemplateRef<any>;

    @ViewChild('nameTemplate')
    nameTemplate: TemplateRef<any>;

    @ViewChild('descriptionTemplate')
    descriptionTemplate: TemplateRef<any>;

    @ViewChild('actionsTemplate')
    actionsTemplate: TemplateRef<any>;

    @ViewChild('actionStudentsTemplate')
    actionStudentsTemplate: TemplateRef<any>;

    @ViewChild('statusTemplate')
    statusTemplate: TemplateRef<any>;
}
