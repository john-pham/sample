// ======================================
// Author: Ebrain Team
// Email:  johnpham@ymail.com
// Copyright (c) 2017 supperbrain.visualstudio.com
// 
// ==> Contact Us: supperbrain@outlook.com
// ======================================

import { Component, OnInit, OnDestroy, TemplateRef, ViewChild } from '@angular/core';

import { fadeInOut } from '../../services/animations';
import { AppTranslationService } from "../../services/app-translation.service";
import { AttendancesService } from "../../services/attendances.service";
import { AlertService, MessageSeverity, DialogType } from '../../services/alert.service';
import { Utilities } from '../../services/utilities';
import { Attendance } from '../../models/attendance.model';
import { BsModalService } from 'ngx-bootstrap/modal';
import { BsModalRef } from 'ngx-bootstrap/modal/bs-modal-ref.service';
import { ClassesService } from "../../services/classes.service";
import { Class } from '../../models/Class.model';

@Component({
    selector: 'attendances',
    templateUrl: './attendances.component.html',
    styleUrls: ['./attendances.component.css'],
    animations: [fadeInOut]
})

export class AttendancesComponent implements OnInit, OnDestroy {
    rows = [];
    classes = [];

    loadingIndicator: boolean = true;

    filterName: string;
    filterValue: string;
    createDate: Date;
    classId: string;

    public changesSavedCallback: () => void;
    public changesFailedCallback: () => void;
    public changesCancelledCallback: () => void;

    modalRef: BsModalRef;

    constructor(private alertService: AlertService, private translationService: AppTranslationService, private localService: AttendancesService,
        private modalService: BsModalService,
        private classService: ClassesService) {

    }

    ngOnInit() {
        let gT = (key: string) => this.translationService.getTranslation(key);
        this.getFromServer();
        this.createDate = new Date();
    }

    ngOnDestroy() {
        //this.saveToDisk();
    }


    private getFromServer() {
        this.loadingIndicator = true;
        var disp = this.classService.search("", "").subscribe(
            list => this.onDataLoadClassesSuccessful(list),
            error => this.onDataLoadFailed(error),
            () => {
                disp.unsubscribe();
                setTimeout(() => { this.loadingIndicator = false; }, 1500);
            });
    }

    private search() {
        var disp = this.localService.search(this.classId, "", this.createDate).subscribe(
            list => this.onDataLoadSuccessful(list),
            error => this.onDataLoadFailed(error),
            () => {
                disp.unsubscribe();
                setTimeout(() => { this.loadingIndicator = false; }, 1500);
            });
    }

    private onDataLoadClassesSuccessful(list: Class[]) {
        if (list.length > 0) {
            this.classId = list[0].id;
        }
        this.search();
        this.classes = list;
        this.alertService.stopLoadingMessage();

    }

    private onDataLoadSuccessful(list: Attendance[]) {
        this.rows = list;
        this.alertService.stopLoadingMessage();

    }

    private onDataLoadFailed(error: any) {
        this.alertService.stopLoadingMessage();
        this.alertService.showStickyMessage("Load Error", `Unable to retrieve user data from the server.\r\nErrors: "${Utilities.getHttpResponseMessage(error)}"`,
            MessageSeverity.error, error);

    }

    private save() {
        this.alertService.startLoadingMessage("Saving changes...");

        this.localService.save(this.rows).subscribe(
            value => this.onDataLoadSuccessful(value),
            error => this.saveFailedHelper(error));
    }


    private saveFailedHelper(error: any) {
        this.alertService.stopLoadingMessage();
        this.alertService.showStickyMessage("Save Error", "The below errors occured whilst saving your changes:", MessageSeverity.error, error);
        this.alertService.showStickyMessage(error, null, MessageSeverity.error);

        if (this.changesFailedCallback)
            this.changesFailedCallback();
    }

    close() {
        this.modalRef.hide();
    }

    updateAbsent(row, event, rowIndex) {
        row.absent = event.target.checked;
        row.notAbsent = !row.absent;
        this.refreshValueRow(row, rowIndex);
    }

    updateNotAbsent(row, event, rowIndex) {
        row.notAbsent = event.target.checked;
        row.absent = !row.notAbsent;
        this.refreshValueRow(row, rowIndex);
    }

    refreshValueRow(row, rowIndex) {

        let rows = [...this.rows];
        rows[rowIndex] = row;

        this.rows = [...this.rows]
    }

    @ViewChild('statusHeaderTemplate')
    statusHeaderTemplate: TemplateRef<any>;

    @ViewChild('nameTemplate')
    nameTemplate: TemplateRef<any>;

    @ViewChild('descriptionTemplate')
    descriptionTemplate: TemplateRef<any>;

    @ViewChild('actionsTemplate')
    actionsTemplate: TemplateRef<any>;

    @ViewChild('statusTemplate')
    statusTemplate: TemplateRef<any>;
}