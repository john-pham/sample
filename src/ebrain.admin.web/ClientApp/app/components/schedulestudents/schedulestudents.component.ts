// ======================================
// Author: Ebrain Team
// Email:  johnpham@ymail.com
// Copyright (c) 2017 supperbrain.visualstudio.com
// 
// ==> Contact Us: supperbrain@outlook.com
// ======================================

import { Component, OnInit, OnDestroy, TemplateRef, ViewChild, Input } from '@angular/core';
import { Router } from '@angular/router';

import { fadeInOut } from '../../services/animations';
import { AppTranslationService } from "../../services/app-translation.service";
import { GrpsuppliersService } from "../../services/grpsuppliers.service";
import { AlertService, MessageSeverity, DialogType } from '../../services/alert.service';
import { Utilities } from '../../services/utilities';
import { Grpsupplier } from '../../models/Grpsupplier.model';
import { BsModalService } from 'ngx-bootstrap/modal';
import { BsModalRef } from 'ngx-bootstrap/modal/bs-modal-ref.service';

import { IOStockReport } from "../../models/iostockreport.model";
import { PaymentsService } from "../../services/payments.service";
import { Payment } from "../../models/payment.model";
import { ClassList } from "../../models/classlists.model";
import { ClassesService } from "../../services/classes.service";
import { SuppliersService } from "../../services/suppliers.service";
import { ClassStatusService } from "../../services/classstatus.service";
import { Supplier } from "../../models/supplier.model";
import { ClassStatus } from "../../models/classstatus.model";
import { AccessRightsService } from "../../services/access-rights.service";
import { Page } from "../../models/page.model";
import { Results } from "../../models/results.model";
import { Class } from '../../models/class.model';

@Component({
    selector: 'schedulestudents',
    templateUrl: './schedulestudents.component.html',
    styleUrls: ['./schedulestudents.component.css'],
    animations: [fadeInOut]
})

export class ScheduleStudentComponent implements OnInit, OnDestroy {
    @Input() studentId: any;
    @Input() classId: any;

    rows = [];
    columns = [];
    private classes = [];

    loadingIndicator: boolean = true;

    filterName: string;
    filterValue: string;
    

    public changesSavedCallback: () => void;
    public changesFailedCallback: () => void;
    public changesCancelledCallback: () => void;

    modalRef: BsModalRef;
    private page: Page;
    constructor(private alertService: AlertService, private router: Router, private translationService: AppTranslationService,
        private localService: ClassesService, private supplierService: SuppliersService,
        private classStatusService: ClassStatusService, public accessRightService: AccessRightsService,
        private modalService: BsModalService) {
        this.filterValue = '';
        this.page = new Page();
        this.page.pageNumber = 0;
        this.page.size = 20;
    }

    setPage(pageInfo) {
        this.page.pageNumber = pageInfo.offset;
        this.getFromServer();
    }

    ngOnInit() {
        let gT = (key: string) => this.translationService.getTranslation(key);

        this.columns = [
            { headerClass: "text-center", prop: 'className', name: gT('label.classlist.ClassName'), cellTemplate: this.nameTemplate },
            { headerClass: "text-center", prop: 'learnDate', name: gT('label.classlist.LearnDate'), cellTemplate: this.nameTemplate },
            { headerClass: "text-center", prop: 'todayName', name: gT('label.classlist.TodayName'), cellTemplate: this.nameTemplate },
            { headerClass: "text-center", prop: 'noteClass', name: gT('label.classlist.NoteClass'), cellTemplate: this.nameTemplate },
            { headerClass: "text-center", prop: 'materialName', name: gT('label.classlist.MaterialName'), cellTemplate: this.nameTemplate },
        ];

        var disp = this.localService.getClassCurrent(this.studentId).subscribe(
            list => this.onDataLoadClassSuccessful(list),
            error => this.onDataLoadFailed(error),
            () => {
                disp.unsubscribe();
                setTimeout(() => { this.loadingIndicator = false; }, 1500);
            });
    }

    ngOnDestroy() {
        //this.saveToDisk();
    }

    private getFromServer() {
        this.loadingIndicator = true;
        var disp = this.localService.getScheduleStudent(this.classId, this.studentId, this.page.pageNumber, this.page.size).subscribe(
            list => this.onDataLoadSuccessful(list),
            error => this.onDataLoadFailed(error),
            () => {
                disp.unsubscribe();
                setTimeout(() => { this.loadingIndicator = false; }, 1500);
            });


    }

    private onDataLoadClassSuccessful(list: Class[]) {
        if (list != null && list.length > 0) {
            this.classId = list[0].id;
        }
        this.classes = list;
        this.getFromServer();
        this.alertService.stopLoadingMessage();
    }

    private onDataLoadSuccessful(resulted: Results<ClassList>) {
        this.page.totalElements = resulted.total;
        this.rows = resulted.list;
        this.alertService.stopLoadingMessage();

    }

    private onDataLoadFailed(error: any) {
        this.alertService.stopLoadingMessage();
        this.alertService.showStickyMessage("Load Error", `Unable to retrieve user data from the server.\r\nErrors: "${Utilities.getHttpResponseMessage(error)}"`,
            MessageSeverity.error, error);

    }

    search() {
        this.getFromServer();
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

    @ViewChild('totalPriceTemplate')
    totalPriceTemplate: TemplateRef<any>;

    @ViewChild('template')
    template: TemplateRef<any>;
}
