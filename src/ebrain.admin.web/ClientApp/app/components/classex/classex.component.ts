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
import { Results } from "../../models/results.model";
import { Shiftclass } from '../../models/Shiftclass.model';
import { ShiftclassesService } from '../../services/shiftclasses.service';
import { ClassOffset } from "../../models/classoffset.model";
import { ClassEx } from "../../models/classex.model";

@Component({
    selector: 'classex',
    templateUrl: './classex.component.html',
    styleUrls: ['./classex.component.css'],
    animations: [fadeInOut]
})

export class ClassExComponent implements OnInit, OnDestroy {

    loadingIndicator: boolean = true;
    isEditMode: boolean = true;

    @Input() classId: any = "";
    @Input() studentId: any = "";

    public pointer: Class;
    private columns = [];
    private rows = [];
    shifts: Shiftclass[] = [];

    modalRef: BsModalRef;

    constructor(private alertService: AlertService, private translationService: AppTranslationService,
        private localService: ClassesService,
        private shiftService: ShiftclassesService,
        private modalService: BsModalService,
        private route: ActivatedRoute, private router: Router) {

        this.pointer = new Class();

    }

    ngOnInit() {
        let gT = (key: string) => this.translationService.getTranslation(key);

        this.columns = [
            { headerClass: "text-center", prop: "learnDate", name: gT('label.class.LearnDate'), cellTemplate: this.nameTemplate },
            { headerClass: "text-center", prop: 'shiftName', name: gT('label.class.Shift'), cellTemplate: this.nameTemplate },
            { name: '', width: 150, cellTemplate: this.actionsTemplate, resizeable: false, canAutoResize: false, sortable: false, draggable: false }
        ];

        this.shiftService.search("", "", 0, 0).subscribe(
            list => this.onDataLoadShiftSuccessful(list),
            error => this.onDataLoadFailed(error));

        this.localService.getClassEx(this.studentId, this.classId).subscribe(
            list => {
                this.rows = list;
            },
            error => this.onDataLoadFailed(error));
    }

    private addClassOffset(id: string) {
        let err = "";
        var item = this.pointer;
        if (item.learnDate == null) err = "Vui lòng chọn thời gian học";
        else if (item.shiftId == null || item.shiftId.length == 0) err = "Vui lòng chọn ca học";

        if (err.length > 0) {
            this.alertService.showStickyMessage("Save Error", err, MessageSeverity.error);
        }
        else {
            var itemNew = new ClassEx();
            itemNew.learnDate = item.learnDate;
            itemNew.classExId = "";
            itemNew.studentId = this.studentId;
            itemNew.shiftId = item.shiftId;
            itemNew.classId = this.classId;

            var shift = this.shifts.filter(x => x.id == item.shiftId)[0];
            if (shift != null) {
                itemNew.shiftName = shift.name;
            }

            this.rows.push(itemNew);
            this.rows = [...this.rows];
        }
        this.loadingIndicator = false;
    }

    private save() {
        this.alertService.startLoadingMessage("Saving changes...");
        this.loadingIndicator = false;
        this.localService.saveEx(this.rows).subscribe(value => {
            this.rows = [...value];
        }, error => this.saveFailedHelper(error));
    }

    private saveFailedHelper(error: any) {
        this.alertService.stopLoadingMessage();
        this.alertService.showStickyMessage("Save Error", "The below errors occured whilst saving your changes:", MessageSeverity.error, error);
        this.alertService.showStickyMessage(error, null, MessageSeverity.error);
    }

    private onDataLoadShiftSuccessful(resulted: Results<Shiftclass>) {
        let list = resulted.list;
        if (list.length > 0) {
            this.pointer.shiftId = list[0].id;
        }
        this.shifts = list;
        this.loadingIndicator = false;
    }

    private onDataLoadFailed(error: any) {
        this.alertService.stopLoadingMessage();
        this.alertService.showStickyMessage("Load Error", `Unable to retrieve user data from the server.\r\nErrors: "${Utilities.getHttpResponseMessage(error)}"`,
            MessageSeverity.error, error);
        this.loadingIndicator = false;
    }

    ngOnDestroy() {
        //this.saveToDisk();
    }

    close() {
        this.modalRef.hide();
    }

    @ViewChild('nameTemplate')
    nameTemplate: TemplateRef<any>;

    @ViewChild('actionsTemplate')
    actionsTemplate: TemplateRef<any>;
}
