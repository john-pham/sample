// ======================================
// Author: Ebrain Team
// Email:  johnpham@ymail.com
// Copyright (c) 2017 supperbrain.visualstudio.com
// 
// ==> Contact Us: supperbrain@outlook.com
// ======================================

import { Component, OnInit, OnDestroy, TemplateRef, ViewChild, Input, EventEmitter, Output } from '@angular/core';
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
import { DateOnlyPipe } from "../../directives/dateonlypipe.directive";

@Component({
    selector: 'classmodules',
    templateUrl: './classmodules.component.html',
    styleUrls: ['./classmodules.component.css'],
    animations: [fadeInOut]
})

export class ClassModuleComponent implements OnInit, OnDestroy {

    loadingIndicator: boolean = true;
    isEditMode: boolean = true;

    @Input() classId: any = "";
    @Input() studentId: any = "";
    @Input() classRef: any;
    @Output() private funcReloadData = new EventEmitter<any>();
    readonly offsetTab = "offset";
    readonly exTab = "ex";
    readonly scheduleTab = "schedule";
    readonly pendingTab = "pending";
    readonly markTab = "mark";

    activeTab: string = this.offsetTab;
    isOffsetActive = true;
    isExActive = false;
    isScheduleActive = false;
    isPendingActive = false;
    isMarkActive = false;

    constructor(private alertService: AlertService, private translationService: AppTranslationService,
        private localService: ClassesService,
        private shiftService: ShiftclassesService,
        private modalService: BsModalService,
        private route: ActivatedRoute, private router: Router) {

    }

    private funcReloadDataClass() {
        if (this.funcReloadData !== undefined) {
            this.funcReloadData.emit();
        }
    }

    ngOnInit() {

    }


    ngOnDestroy() {
        //this.saveToDisk();
    }

    onShowTab(event) {
        this.setActiveTab(event.target.hash);

        switch (this.activeTab) {
            case this.offsetTab:
                this.isOffsetActive = true;
                break;
            case this.exTab:
                this.isExActive = true;
                break;
            case this.scheduleTab:
                this.isScheduleActive = true;
                break;
            case this.pendingTab:
                this.isPendingActive = true;
                break;
            case this.markTab:
                this.isMarkActive = true;
                break;
            default:
                throw new Error("Selected bootstrap tab is unknown. Selected Tab: " + this.activeTab);
        }
    }

    setActiveTab(tab: string) {
        this.activeTab = tab.split("#", 2).pop();
    }


    closeClass() {
        this.classRef.hide();
    }

}
