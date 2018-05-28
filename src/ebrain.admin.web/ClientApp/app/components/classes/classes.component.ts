// ======================================
// Author: Ebrain Team
// Email:  johnpham@ymail.com
// Copyright (c) 2017 supperbrain.visualstudio.com
// 
// ==> Contact Us: supperbrain@outlook.com
// ======================================

import { Component, OnInit, OnDestroy, TemplateRef, ViewChild } from '@angular/core';
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
import { ShiftclassesService } from "../../services/shiftclasses.service";
import { Shiftclass } from "../../models/Shiftclass.model";
import { AccessRightsService } from "../../services/access-rights.service";
import { Results } from "../../models/results.model";

@Component({
    selector: 'classes',
    templateUrl: './classes.component.html',
    styleUrls: ['./classes.component.css'],
    animations: [fadeInOut]
})

export class ClassesComponent implements OnInit, OnDestroy {
    rowTimes = [];
    columnTimes = [];

    rowStudents = [];
    columnStudents = [];

    materials = [];
    status = [];
    students = [];
    suppliers: Supplier[] = [];
    todays: Today[] = [];
    rooms: Room[] = [];
    shifts: Shiftclass[] = [];

    loadingIndicator: boolean = true;

    filterName: string;
    filterValue: string;
    isEditMode: boolean = true;

    studentId: string;
    classId: string;

    private pointer: Class;
    public changesSavedCallback: () => void;
    public changesFailedCallback: () => void;
    public changesCancelledCallback: () => void;

    modalRef: BsModalRef;
    classExamineRef: BsModalRef;

    constructor(private alertService: AlertService, private translationService: AppTranslationService,
        private localService: ClassesService, private modalService: BsModalService,
        private materialService: MaterialLearnsService, private classStatusService: ClassStatusService,
        private studentService: StudentsService, private supplierService: SuppliersService,
        private todayService: TodayService, private roomService: RoomsService, public accessRightService: AccessRightsService,
        private shiftService: ShiftclassesService, private route: ActivatedRoute, private router: Router) {

        this.pointer = new Class();
    }

    ngOnInit() {
        let gT = (key: string) => this.translationService.getTranslation(key);

        this.columnTimes = [
            { headerClass: "text-center", prop: "todayName", name: gT('label.class.Today'), cellTemplate: this.nameTemplate },
            { headerClass: "text-center", prop: 'shiftName', name: gT('label.class.Shift'), cellTemplate: this.nameTemplate },
            { headerClass: "text-center", prop: 'roomName', name: gT('label.class.Room'), cellTemplate: this.descriptionTemplate },
            { headerClass: "text-center", prop: 'supplierName', name: gT('label.class.Teacher'), cellTemplate: this.descriptionTemplate },
            { name: '', width: 150, cellTemplate: this.actionsTemplate, resizeable: false, canAutoResize: false, sortable: false, draggable: false }
        ];

        this.columnStudents = [
            { headerClass: "text-center", prop: "fullName", name: gT('label.class.Student'), cellTemplate: this.nameTemplate },
            { headerClass: "text-center", prop: 'address', name: gT('label.class.Address'), cellTemplate: this.nameTemplate },
            { name: '', width: 100, cellTemplate: this.actionStudentsTemplate, resizeable: false, canAutoResize: false, sortable: false, draggable: false }
        ];

        //
        this.getFromServer(false);
        this.filterValue = "";
        //
    }

    ngOnDestroy() {
        //this.saveToDisk();
    }

    addClass(template: TemplateRef<any>) {
        this.pointer.id = "";
        this.modalRef = this.modalService.show(template, { class: 'modal-lg' });
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

    deletemaster(row) {
        this.alertService.showDialog('Are you sure you want to delete the task?', DialogType.confirm, () => this.deleteHelper());
    }

    private deleteHelper() {
        this.localService.delete(this.pointer.id).subscribe(value => this.deleteSuccessHelper(), error => this.deleteFailedHelper(error));
    }

    private deleteSuccessHelper() {
        this.alertService.showMessage("Success", `Class \"${this.pointer.name}\" was deleted successfully`, MessageSeverity.success);
        if (this.changesSavedCallback)
            this.changesSavedCallback();
        this.getDefault('', false);
    }


    private deleteFailedHelper(error: any) {
        this.alertService.stopLoadingMessage();
        this.alertService.showStickyMessage("Delete Error", "The below errors occured whilst deleting your changes:", MessageSeverity.error, error);
        this.alertService.showStickyMessage(error, null, MessageSeverity.error);

        if (this.changesFailedCallback)
            this.changesFailedCallback();
    }

    private getFromServer(isReset: boolean) {
        this.loadingIndicator = true;

        var disp = this.localService.search(this.filterName, this.filterValue).subscribe(
            list => this.onDataLoadSuccessful(list),
            error => this.onDataLoadFailed(error),
            () => {
                disp.unsubscribe();
                setTimeout(() => { this.loadingIndicator = false; }, 1500);
            });

        this.materialService.getMaterialLearn(this.filterName, this.filterValue, 0, 0).subscribe(
            list => this.onDataLoadMaterialSuccessful(list),
            error => this.onDataLoadFailed(error),
            () => {
                disp.unsubscribe();
                setTimeout(() => { this.loadingIndicator = false; }, 1500);
            });

        this.classStatusService.getAll().subscribe(
            list => this.onDataLoadClassStatusSuccessful(list),
            error => this.onDataLoadFailed(error),
            () => {
                disp.unsubscribe();
                setTimeout(() => { this.loadingIndicator = false; }, 1500);
            });
        this.supplierService.search("", "", 4, 0, 0).subscribe(
            list => this.onDataLoadSupplierSuccessful(list),
            error => this.onDataLoadFailed(error),
            () => {
                disp.unsubscribe();
                setTimeout(() => { this.loadingIndicator = false; }, 1500);
            });

        this.todayService.getAll().subscribe(
            list => this.onDataLoadTodaySuccessful(list),
            error => this.onDataLoadFailed(error),
            () => {
                disp.unsubscribe();
                setTimeout(() => { this.loadingIndicator = false; }, 1500);
            });

        this.roomService.search("", "", 0, 0).subscribe(
            list => this.onDataLoadRoomSuccessful(list),
            error => this.onDataLoadFailed(error),
            () => {
                disp.unsubscribe();
                setTimeout(() => { this.loadingIndicator = false; }, 1500);
            });

        this.studentService.search("", "").subscribe(
            list => {
                this.onDataLoadStudentSuccessful(list);
                //get default
                this.getDefault("", isReset);
            },
            error => this.onDataLoadFailed(error),
            () => {
                disp.unsubscribe();
                setTimeout(() => { this.loadingIndicator = false; }, 1500);
            });
        this.shiftService.search("", "", 0, 0).subscribe(
            list => this.onDataLoadShiftSuccessful(list),
            error => this.onDataLoadFailed(error),
            () => {
                disp.unsubscribe();
                setTimeout(() => { this.loadingIndicator = false; }, 1500);
            });
        //get default
        this.getDefault("", isReset);

    }

    private getDefault(index: string, isReset: boolean) {
        this.route.paramMap
            .switchMap((params: ParamMap) => {
                var id = '';
                if (isReset == false) {
                    id = params.get('id');
                }
                return this.localService.getDefault(id);
            })
            .subscribe(results => this.mappingDetail(results), error => this.onDataLoadFailed(error));

        //var disp = this.localService.getDefault(index).subscribe(
        //    list => this.onDataLoadDefaultSuccessful(list),
        //    error => this.onDataLoadFailed(error),
        //    () => {
        //        disp.unsubscribe();
        //        setTimeout(() => { this.loadingIndicator = false; }, 1500);
        //    });
    }

    private onDataLoadShiftSuccessful(resulted: Results<Shiftclass>) {
        let list = resulted.list;
        if (list.length > 0) {
            this.pointer.shiftId = list[0].id;
        }
        this.shifts = list;
    }

    private onDataLoadStudentSuccessful(list: Student[]) {
        if (list.length > 0) {
            this.pointer.studentId = list[0].id;
        }
        this.students = list;
    }

    private onDataLoadRoomSuccessful(resulted: Results<Room>) {
        var list = resulted.list;
        if (list.length > 0) {
            this.pointer.roomId = list[0].id;
        }
        this.rooms = list;
    }

    private onDataLoadTodaySuccessful(list: Today[]) {
        if (list.length > 0) {
            this.pointer.todayId = list[0].id;
        }
        this.todays = list;
    }

    private onDataLoadSupplierSuccessful(resulted: Results<Supplier>) {
        var list = resulted.list;
        if (list.length > 0) {
            this.pointer.supplierId = list[0].id;
            this.pointer.teacherTodayId = list[0].id;
        }
        this.suppliers = list;
    }

    private onDataLoadClassStatusSuccessful(list: ClassStatus[]) {
        if (list.length > 0) {
            this.pointer.statusId = list[0].id;
        }
        this.status = list;
        this.alertService.stopLoadingMessage();

    }

    private onDataLoadMaterialSuccessful(resulted: Results<MaterialLearn>) {
        var list = resulted.list;
        if (list.length > 0) {
            this.pointer.materialId = list[0].id;
        }
        this.materials = list;
        this.alertService.stopLoadingMessage();

    }

    private onDataLoadSuccessful(list: Class[]) {
        this.alertService.stopLoadingMessage();

    }

    private onDataLoadFailed(error: any) {
        this.alertService.stopLoadingMessage();
        this.alertService.showStickyMessage("Load Error", `Unable to retrieve user data from the server.\r\nErrors: "${Utilities.getHttpResponseMessage(error)}"`,
            MessageSeverity.error, error);

    }

    private save() {
        this.alertService.startLoadingMessage("Saving changes...");

        this.pointer.times = this.rowTimes;
        this.pointer.students = this.rowStudents;
        this.localService.save(this.pointer).subscribe(value => this.saveSuccessHelper(value), error => this.saveFailedHelper(error));
    }

    private saveSuccessHelper(item?: Class) {
        //set item for pointer
        if (item != null) {
            this.pointer = item;
            this.mappingDetail(item);
        }
    }

    private mappingDetail(item?: Class) {
        if (item != null) {
            this.pointer.id = item.id;
            this.pointer.code = item.code;
            this.pointer.name = item.code;
            this.pointer.startDate = item.startDate;
            this.pointer.endDate = item.endDate;
            this.pointer.endTime = new Date();
            this.pointer.startTime = new Date();
            this.pointer.longLearn = item.longLearn;
            this.pointer.maxStudent = item.maxStudent;

        }

        var itemPointer = this.pointer;

        if (item != null) {
            //set times
            var times = item.times;
            this.rowTimes = [];
            if (times != null && times.length > 0) {
                times.forEach(row => {
                    var itemNew = new ClassTime();
                    itemPointer.todayId = row.onTodayId;
                    itemPointer.startTime = row.startTime;
                    itemPointer.endTime = row.endTime;
                    itemPointer.roomId = row.roomId;
                    itemPointer.teacherTodayId = row.supplierId;
                    itemPointer.shiftId = row.shiftId;
                    this.addClassTime(row.id);
                });
            }

            //set students
            var studentes = item.students;
            this.rowStudents = [];
            if (studentes != null && studentes.length > 0) {
                studentes.forEach(row => {
                    itemPointer.studentId = row.studentId;
                    this.addClassStudent(row.id);
                });
            }
        }
        this.alertService.stopLoadingMessage();

        this.alertService.showMessage("Success", `User \"${this.pointer.name}\" was created successfully`, MessageSeverity.success);

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

    private addClassTime(id: string) {
        var item = this.pointer;
        var itemNew = new ClassTime();
        itemNew.onTodayId = item.todayId;
        itemNew.startTime = item.startTime;
        itemNew.endTime = item.endTime;
        itemNew.roomId = item.roomId;
        itemNew.supplierId = item.teacherTodayId;
        itemNew.id = id;
        itemNew.shiftId = item.shiftId;
        itemNew.classId = item.id;
        var room = this.rooms.filter(x => x.id == item.roomId)[0];
        if (room != null) {
            itemNew.roomName = room.name;
        }

        var today = this.todays.filter(x => x.id == item.todayId)[0];
        if (today != null) {
            itemNew.todayName = today.name;
        }

        var sup = this.suppliers.filter(x => x.id == item.supplierId)[0];
        if (sup != null) {
            itemNew.supplierName = sup.name;
        }

        var shift = this.shifts.filter(x => x.id == item.shiftId)[0];
        if (shift != null) {
            itemNew.shiftName = shift.name;
        }

        this.rowTimes.push(itemNew);
        this.rowTimes = [...this.rowTimes]
        this.pointer.times = this.rowTimes;
    }

    private addClassStudent(id: string) {
        var item = this.pointer;
        var itemNew = new ClassStudent();
        itemNew.studentId = item.studentId;
        var student = this.students.filter(x => x.id == item.studentId)[0];
        if (student != null) {
            itemNew.fullName = student.name;
            itemNew.address = student.address;
        }
        itemNew.classId = item.id;
        itemNew.id = id;
        this.rowStudents.push(itemNew);
        this.rowStudents = [...this.rowStudents]
        this.pointer.students = this.rowStudents;
    }

    deleteTime(row) {
        this.alertService.showDialog('Are you sure you want to delete the row?', DialogType.confirm, () => this.deleteTimeHelper(row));
    }

    private deleteTimeHelper(row) {
        this.rowTimes = this.rowTimes.filter(obj => obj !== row);
        this.rowTimes = [...this.rowTimes];
    }

    deleteStudent(row) {
        this.alertService.showDialog('Are you sure you want to delete the row?', DialogType.confirm, () => this.deleteStudentHelper(row));
    }

    deleteStudentHelper(row) {
        this.rowStudents = this.rowStudents.filter(obj => obj !== row);
        this.rowStudents = [...this.rowStudents];
    }

    private addnew() {
        this.pointer = new Class();
        this.rowStudents = [];
        this.rowTimes = [];
        this.getFromServer(true);
    }

    private markExamine(row, template: TemplateRef<any>) {
        this.studentId = row.studentId;
        this.classId = this.pointer.id;
        this.classExamineRef = this.modalService.show(template, { class: 'modal-lg' });
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
