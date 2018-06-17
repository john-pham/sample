// ======================================
// Author: Ebrain Team
// Email:  johnpham@ymail.com
// Copyright (c) 2017 supperbrain.visualstudio.com
// 
// ==> Contact Us: supperbrain@outlook.com
// ======================================

import { Component, OnInit, OnDestroy, TemplateRef, ViewChild, Input } from '@angular/core';

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
import { AccessRightsService } from "../../share/services/access-rights.service";
import { Page } from "../../models/page.model";
import { Results } from "../../models/results.model";

@Component({
    selector: 'attendanceteaches',
    templateUrl: './attendanceteaches.component.html',
    styleUrls: ['./attendanceteaches.component.css'],
    animations: [fadeInOut]
})

export class AttendanceTeachersComponent implements OnInit, OnDestroy {
   
    loadingIndicator: boolean = true;

    public changesSavedCallback: () => void;
    public changesFailedCallback: () => void;
    public changesCancelledCallback: () => void;

    modalRef: BsModalRef;
    private page: Page;
    constructor(private alertService: AlertService, private translationService: AppTranslationService,
        private localService: AttendancesService, public accessRightService: AccessRightsService,
        private modalService: BsModalService,
        private classService: ClassesService) {

    }

    ngOnInit() {
    }

    ngOnDestroy() {
        //this.saveToDisk();
    }

}
