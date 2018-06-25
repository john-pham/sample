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
import { ClassPending } from "../../models/classpending.model";
import { DateOnlyPipe } from "../../directives/dateonlypipe.directive";

@Component({
    selector: 'classpending',
    templateUrl: './classpending.component.html',
    styleUrls: ['./classpending.component.css'],
    animations: [fadeInOut]
})

export class ClassPendingComponent implements OnInit, OnDestroy {

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
            { headerClass: "text-center", prop: "fromDate", name: gT('label.class.FromDate'), cellTemplate: this.nameTemplate, pipe: new DateOnlyPipe('en-US'), cellClass: 'text-right' },
            { headerClass: "text-center", prop: 'toDate', name: gT('label.class.ToDate'), cellTemplate: this.nameTemplate, pipe: new DateOnlyPipe('en-US'), cellClass: 'text-right' },
            { headerClass: "text-center", prop: 'note', name: gT('label.class.Note'), cellTemplate: this.nameTemplate },
            { name: '', width: 150, cellTemplate: this.actionsTemplate, resizeable: false, canAutoResize: false, sortable: false, draggable: false }
        ];
        this.getClassPending();
    }

    private getClassPending() {
        this.localService.getClassPending(this.studentId, this.classId).subscribe(
            list => {
                this.rows = list;
                this.loadingIndicator = false;
            },
            error => this.onDataLoadFailed(error));
    }
    private addClassPending(id: string) {
        let err = "";
        var item = this.pointer;
        if (item.fromDate == null) err = "Vui lòng chọn từ thời gian.";
        else if (item.toDate == null) err = "Vui lòng chọn đến thời gian.";
        else if (this.rows !== undefined &&
            this.rows.filter(p => new Date(p.fromDate).getDate() === new Date(item.fromDate).getDate()
                && new Date(p.toDate).getDate() === new Date(item.toDate).getDate())[0] !== undefined) {
            err = "Thời gian đã tồn tại";
        }
        if (err.length > 0) {
            this.alertService.showStickyMessage("Superbrain thông báo", err, MessageSeverity.error);
            this.alertService.stopLoadingMessage();
        }
        else {
            var itemNew = new ClassPending();
            itemNew.fromDate = item.fromDate;
            itemNew.classPendingId = "";
            itemNew.studentId = this.studentId;
            itemNew.toDate = item.toDate;
            itemNew.classId = this.classId;
            itemNew.note = item.note;

            this.rows.push(itemNew);
            this.rows = [...this.rows];
        }
        this.loadingIndicator = false;
    }

    private save() {
        let arrs = this.rows;
        if (this.rows === undefined || this.rows.length === 0) {
            var itemNew = new ClassPending();
            itemNew.studentId = this.studentId;
            itemNew.classId = this.classId;
            arrs.push(itemNew);
        }
        this.alertService.startLoadingMessage("Saving changes...");
        this.loadingIndicator = true;
        this.localService.savePending(arrs).subscribe(value => {
            this.rows = [...value];
            this.loadingIndicator = false;
            this.alertService.stopLoadingMessage();
            this.alertService.showMessage("Success", `Lưu dữ liệu thành công.`, MessageSeverity.success);
        }, error => this.saveFailedHelper(error));
    }

    private saveFailedHelper(error: any) {
        this.alertService.stopLoadingMessage();
        this.alertService.showStickyMessage("Superbrain thông báo", "The below errors occured whilst saving your changes:", MessageSeverity.error, error);
        this.alertService.showStickyMessage(error, null, MessageSeverity.error);
        this.loadingIndicator = false;
    }

    private onDataLoadFailed(error: any) {
        this.alertService.stopLoadingMessage();
        this.alertService.showStickyMessage("Load Error", `Unable to retrieve user data from the server.\r\nErrors: "${Utilities.getHttpResponseMessage(error)}"`,
            MessageSeverity.error, error);
        this.loadingIndicator = false;
    }

    private deleteClasses(row) {
        this.alertService.showDialog('Bạn muốn xóa dữ liệu của dòng này?', DialogType.confirm, () => this.deleteTimeHelper(row));
    }

    private deleteTimeHelper(row) {
        this.rows = this.rows.filter(obj => obj !== row);
        this.rows = [...this.rows];
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
