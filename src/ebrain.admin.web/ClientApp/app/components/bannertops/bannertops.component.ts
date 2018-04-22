// ======================================
// Author: Ebrain Team
// Email:  johnpham@ymail.com
// Copyright (c) 2017 supperbrain.visualstudio.com
// 
// ==> Contact Us: supperbrain@outlook.com
// ======================================

import { Component, OnInit, OnDestroy, TemplateRef, ViewChild, Input } from '@angular/core';

import { BsModalService } from 'ngx-bootstrap/modal';
import { BsModalRef } from 'ngx-bootstrap/modal/bs-modal-ref.service';

import { fadeInOut } from '../../services/animations';
import { AppTranslationService } from "../../services/app-translation.service";
import { MessengerService } from "../../services/messengers.service";
import { AlertService, MessageSeverity, DialogType } from '../../services/alert.service';
import { Utilities } from '../../services/utilities';
import { File } from '../../models/file.model';
import { Messenger } from '../../models/messenger.model';
import { Results } from '../../models/results.model';
import { Page } from '../../models/page.model';
import { IOStudentListService } from "../../services/iostudentlists.service";
import { StudentsService } from "../../services/students.service";

@Component({
    selector: 'bannertops',
    templateUrl: './bannertops.component.html',
    styleUrls: ['./bannertops.component.css'],
    animations: [fadeInOut]
})

export class BannerTopsComponent implements OnInit, OnDestroy {
    newStudent: number = 0;
    allStudent: number = 0;
    ioNew: number = 0;
    ioAll: number = 0;


    public changesSavedCallback: () => void;
    public changesFailedCallback: () => void;
    public changesCancelledCallback: () => void;

    constructor(private alertService: AlertService, private translationService: AppTranslationService,
        private studentService: StudentsService, private ioService: IOStudentListService) {
        this.newStudent = 0;
        this.allStudent = 0;
        this.ioNew = 0;
        this.ioAll = 0;
    }

    ngOnInit() {

        this.getFromServer();
    }

    ngOnDestroy() {
        //this.saveToDisk();
    }



    private getFromServer() {
        var disp = this.studentService.getNewStudent().subscribe(
            count => {
                this.newStudent = count;
            },
            error => this.onDataLoadFailed(error));
        disp = this.studentService.getAllStudent().subscribe(
            count => {
                this.allStudent = count;
            },
            error => this.onDataLoadFailed(error));
        disp = this.ioService.getIONew().subscribe(
            count => {
                this.ioNew = count;
            },
            error => this.onDataLoadFailed(error));
        disp = this.ioService.getIOAll().subscribe(
            count => {
                this.ioAll = count;
            },
            error => this.onDataLoadFailed(error));
    }

    private onDataLoadFailed(error: any) {
        this.alertService.stopLoadingMessage();
        this.alertService.showStickyMessage("Load Error", `Unable to retrieve user data from the server.\r\nErrors: "${Utilities.getHttpResponseMessage(error)}"`,
            MessageSeverity.error, error);
    }

}
