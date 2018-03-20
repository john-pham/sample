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

@Component({
    selector: 'students',
    templateUrl: './students.component.html',
    styleUrls: ['./students.component.css'],
    animations: [fadeInOut]
})

export class StudentsComponent implements OnInit, OnDestroy {
    /*
        Class Student
    */
    studentId: string;

    rows = [];
    columns = [];
    loadingIndicator: boolean = true;

    filterName: string;
    filterValue: string;

    allGrps: GrpMaterial[] = [];
    allTypes: TypeMaterial[] = [];
    allUnits: Unit[] = [];
    allSups: Supplier[] = [];
    allStuStatus: Studentstatus[] = [];
    allGenders: GenderStudent[] = [];

    private pointer: Student;
    private isEditMode = true;
    public changesSavedCallback: () => void;
    public changesFailedCallback: () => void;
    public changesCancelledCallback: () => void;


    modalRef: BsModalRef;

    modalStudentRef: BsModalRef;

    constructor(private alertService: AlertService, private translationService: AppTranslationService,
        private localService: StudentsService, private modalService: BsModalService,
        private typeservice: TypeMaterialsService) {
        this.pointer = new Student();
    }

    ngOnInit() {
        let gT = (key: string) => this.translationService.getTranslation(key);

        this.columns = [
            { prop: "code", name: gT('label.student.Code'), width: 100, headerTemplate: this.statusHeaderTemplate, cellTemplate: this.statusTemplate, resizeable: false, canAutoResize: false, sortable: false, draggable: false },
            { prop: 'name', name: gT('label.student.Name'), cellTemplate: this.nameTemplate },
            { prop: 'schoolName', name: gT('label.student.SchoolName'), cellTemplate: this.typenameTemplate },
            { prop: 'className', name: gT('label.student.Classname'), cellTemplate: this.grpnameTemplate },
            { prop: 'userName', name: gT('label.student.UserName'), cellTemplate: this.grpnameTemplate },
            { prop: 'note', name: gT('label.student.Note'), cellTemplate: this.descriptionTemplate },
            { name: '', width: 200, cellTemplate: this.actionsTemplate, resizeable: false, canAutoResize: false, sortable: false, draggable: false }
        ];

        //
        this.getFromServer();
    }

    ngOnDestroy() {
        //this.saveToDisk();
    }

    private getStudentStatus() {
        //load student status
        this.localService.getStudentStatus().subscribe(results => this.onDataLoadSuccessfulStudentStatus(results), error => this.onDataLoadFailed(error));
    }

    onDataLoadSuccessfulStudentStatus(status: Studentstatus[]) {
        if (status.length > 0) {
            this.pointer.studentStatusId = status[0].id;
        }
        this.allStuStatus = status;
    }

    private getGender() {
        //load student status
        this.localService.getGender().subscribe(results => this.onDataLoadSuccessfulGender(results), error => this.onDataLoadFailed(error));
    }

    onDataLoadSuccessfulGender(genders: GenderStudent[]) {
        if (genders.length > 0) {
            this.pointer.genderId = genders[0].id;
        }
        this.allGenders = genders;
    }

    addMaterialLearn(template: TemplateRef<any>) {

        //student status
        this.getStudentStatus();
        //gender
        this.getGender();
        this.modalRef = this.modalService.show(template, { class: 'modal-lg' });
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

    delete(row) {
        this.alertService.showDialog('Are you sure you want to delete the task?', DialogType.confirm, () => this.deleteHelper(row));
    }

    private deleteHelper(row) {
        this.localService.delete(row.id).subscribe(value => this.deleteSuccessHelper(row), error => this.deleteFailedHelper(error));
    }

    private deleteSuccessHelper(row: Student) {
        this.getFromServer();
        this.alertService.showMessage("Success", `MaterialLearn \"${row.name}\" was deleted successfully`, MessageSeverity.success);
        if (this.changesSavedCallback)
            this.changesSavedCallback();
    }


    private deleteFailedHelper(error: any) {
        this.alertService.stopLoadingMessage();
        this.alertService.showStickyMessage("Delete Error", "The below errors occured whilst deleting your changes:", MessageSeverity.error, error);
        this.alertService.showStickyMessage(error, null, MessageSeverity.error);

        if (this.changesFailedCallback)
            this.changesFailedCallback();
    }

    private getFromServer() {
        this.loadingIndicator = true;
        //
        var disp = this.localService.getAll().subscribe(
            results => this.onDataLoadSuccessful(results[0]),
            error => this.onDataLoadFailed(error),
            () => {
                disp.unsubscribe();
                setTimeout(() => { this.loadingIndicator = false; }, 1500);
            });
    }

    private onDataLoadSuccessful(students: Student[]) {
        this.rows = students;
        this.alertService.stopLoadingMessage();
    }

    private onDataLoadFailed(error: any) {
        this.alertService.stopLoadingMessage();
        this.alertService.showStickyMessage("Load Error", `Unable to retrieve user data from the server.\r\nErrors: "${Utilities.getHttpResponseMessage(error)}"`,
            MessageSeverity.error, error);

    }

    private onChangeMaterial(typeId: string) {
        this.localService.findGrpByTypeId(typeId).subscribe(results => this.onDataLoadSuccessfulChange(results), error => this.onDataLoadFailed(error));
    }

    private onDataLoadSuccessfulChange(grps: GrpMaterial[]) {
        this.allGrps = grps;
        this.alertService.stopLoadingMessage();
    }

    private save() {
        this.alertService.startLoadingMessage("Saving changes...");

        this.localService.save(this.pointer).subscribe(value => this.saveSuccessHelper(value), error => this.saveFailedHelper(error));
    }

    private editClass(template: TemplateRef<any>, index: string) {
        this.studentId = index;
        this.modalStudentRef = this.modalService.show(template, { class: 'modal-lg' });
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

    private saveSuccessHelper(user?: Student) {
        this.alertService.stopLoadingMessage();
        //this.resetForm();
        this.modalRef.hide();
        //
        this.getFromServer();
        //
        //if (this.isNewUser)
        this.alertService.showMessage("Success", `User \"${this.pointer.name}\" was created successfully`, MessageSeverity.success);
        //else if (!this.isEditingSelf)
        //    this.alertService.showMessage("Success", `Changes to user \"${this.pointer.Name}\" was saved successfully`, MessageSeverity.success);

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
