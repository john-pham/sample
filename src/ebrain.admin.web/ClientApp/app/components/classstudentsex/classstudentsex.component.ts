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
import { Results } from "../../models/results.model";

@Component({
    selector: 'classstudentsex',
    templateUrl: './classstudentsex.component.html',
    styleUrls: ['./classstudentsex.component.css'],
    animations: [fadeInOut]
})

export class ClassStudentExComponent implements OnInit, OnDestroy {

    loadingIndicator: boolean = true;
    isEditMode: boolean = true;

    public pointer: Class;
    private columns = [];
    private rows = [];
    private classes = [];
    private materials: any = [];
    classId: string;

    @Input() studentId: any;
    @Input() ioStockId: any;
    @Input() materialId: any;
    @Input() ioStockDetailId: any;

    modalRef: BsModalRef;
    classExamineRef: BsModalRef;

    constructor(private alertService: AlertService, private translationService: AppTranslationService,
        private localService: ClassesService, private modalService: BsModalService,
        private materialService: MaterialLearnsService, private classStatusService: ClassStatusService,
        private studentService: StudentsService, private supplierService: SuppliersService,
        private todayService: TodayService, private roomService: RoomsService, private route: ActivatedRoute, private router: Router) {

        this.pointer = new Class();

    }

    ngOnInit() {
        let gT = (key: string) => this.translationService.getTranslation(key);

        this.getFromServer();

        this.studentService.get(this.studentId).subscribe(
            item => {
                if (item != null) {
                    this.pointer.studentName = item.name;
                    this.pointer.studentBirthday = item.birthday;
                    this.pointer.studentAddress = item.address;
                }
            });

        this.materialService.getMaterialLearn("", "", 0, 0).subscribe(
            list => this.onDataLoadMaterialSuccessful(list),
            error => this.onDataLoadFailed(error));
    }

    private getFromServer() {
        var disp = this.localService.search('', '', 0).subscribe(
            list => this.onDataLoadSuccessful(list),
            error => this.onDataLoadFailed(error),
            () => {
                disp.unsubscribe();
                setTimeout(() => { this.loadingIndicator = false; }, 1500);
            });
    }


    changedClasses() {
        this.localService.getsummaries("", "", "", "", this.classId, 0, 0, 0).subscribe(
            items => this.onDataLoadClassSuccessful(items),
            error => this.onDataLoadFailed(error));
    }


    private onDataLoadMaterialSuccessful(resulted: Results<MaterialLearn>) {
        var list = resulted.list;
        this.materials = list;
        this.pointer.materialId = this.materialId;
        this.alertService.stopLoadingMessage();
        this.loadingIndicator = false;
    }

    private onDataLoadAllClassStudentSuccessful(items: ClassList[]) {
        if (items == null) items = [];

        this.rows = items;
        this.rows = [...this.rows];
        this.alertService.stopLoadingMessage();
    }

    private onDataLoadClassSuccessful(resulted: Results<ClassList>) {
        var items = resulted.list;
        if (items != null && items.length > 0) {
            var item = items[0];

            this.pointer.code = item.code;
            this.pointer.name = item.name;
            this.pointer.id = item.id;
            this.pointer.materialName = item.materialName;
            this.pointer.maxStudent = item.maxStudent;
            this.pointer.countStudent = item.countStudent;
        }
        this.alertService.stopLoadingMessage();
    }

    private onDataLoadSuccessful(list: Class[]) {
        if (list != null && list.length > 0) {
            this.classId = list[0].id;
        }
        this.classes = list;
        this.changedClasses();
        this.alertService.stopLoadingMessage();
    }

    private onDataLoadFailed(error: any) {
        this.alertService.stopLoadingMessage();
        this.alertService.showStickyMessage("Load Error", `Unable to retrieve user data from the server.\r\nErrors: "${Utilities.getHttpResponseMessage(error)}"`,
            MessageSeverity.error, error);

    }

    private save() {
        let err = "";

        var item = this.pointer;
        if (this.studentId == null || this.studentId.length == 0) err = "Vui lòng chọn học viên";
        else if (item.materialId == null || item.materialId.length == 0) err = "Vui lòng chọn khóa học";
        else if (item.startDate == null) err = "Vui lòng chọn bắt đầu";
        else if (item.endDate == null) err = "Vui lòng chọn kết thúc";

        if (err.length > 0) {
            this.alertService.showStickyMessage("Superbrain thông báo", err, MessageSeverity.error);
        } else {
            var cls = [];

            var itemNew = new Class();
            itemNew.id = this.classId;
            itemNew.studentId = this.studentId;
            itemNew.materialId = item.materialId;
            itemNew.ioStockId = this.ioStockId;
            itemNew.ioStockDetailId = this.ioStockDetailId;
            itemNew.startDate = item.startDate;
            itemNew.endDate = item.endDate;
            cls.push(itemNew);


            var disp = this.localService.saveStudent(cls).subscribe(
                items => this.onDataSaveSuccessful(items),
                error => this.onDataLoadFailed(error),
                () => {
                    disp.unsubscribe();
                    setTimeout(() => { this.loadingIndicator = false; }, 1500);
                });
        }
    }

    private onDataSaveSuccessful(item: Class) {
        this.alertService.showMessage("Success", `Update was created successfully`, MessageSeverity.success);
    }

    ngOnDestroy() {
        //this.saveToDisk();
    }

    close() {
        this.modalRef.hide();
    }

    closeExamine() {
        this.classExamineRef.hide();
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

    @ViewChild('actionStudentsTemplate')
    actionStudentsTemplate: TemplateRef<any>;

    @ViewChild('statusTemplate')
    statusTemplate: TemplateRef<any>;
}
