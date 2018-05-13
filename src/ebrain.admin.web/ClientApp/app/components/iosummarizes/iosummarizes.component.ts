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
import { IOStudentListService } from "../../services/iostudentlists.service";
import { IOStockReport } from "../../models/iostockreport.model";
import { Page } from '../../models/page.model';
import { AccessRightsService } from "../../services/access-rights.service";
import { Results } from "../../models/results.model";
@Component({
    selector: 'iosummarizes',
    templateUrl: './iosummarizes.component.html',
    styleUrls: ['./iosummarizes.component.css'],
    animations: [fadeInOut]
})

export class IOSummarizesComponent implements OnInit, OnDestroy {
    rows = [];
    columns = [];
    loadingIndicator: boolean = true;

    filterName: string;
    filterValue: string;
    fromDate: Date;
    toDate: Date;
    private page: Page;

    public changesSavedCallback: () => void;
    public changesFailedCallback: () => void;
    public changesCancelledCallback: () => void;

    modalRef: BsModalRef;


    @Input() isAllIO: boolean = false;

    constructor(private alertService: AlertService, private router: Router, private translationService: AppTranslationService, private localService: IOStudentListService, public accessRightService: AccessRightsService, private modalService: BsModalService) {
        this.filterValue = "";
        this.page = new Page();
        this.page.pageNumber = 0;
        this.page.size = 20;
    }

    setPage(pageInfo) {
        this.page.pageNumber = pageInfo.offset;
        this.search();
    }

    ngOnInit() {
        var date = new Date(), y = date.getFullYear(), m = date.getMonth();
        if (this.isAllIO == false) {
            this.fromDate = date;
        }
        else {
            this.fromDate = new Date(1900, 1, 1);
        }
        this.toDate = new Date(y, m + 1, 0);

        let gT = (key: string) => this.translationService.getTranslation(key);

        this.columns = [
            { headerClass: "text-center", prop: 'code', name: gT('label.iostudentlist.Code'), cellTemplate: this.nameTemplate },
            { headerClass: "text-center", prop: 'createDate', name: gT('label.iostudentlist.CreateDate'), cellTemplate: this.nameTemplate },
            { headerClass: "text-center", prop: 'fullName', name: gT('label.iostudentlist.CreateUser'), cellTemplate: this.nameTemplate },
            { headerClass: "text-center", prop: 'studentName', name: gT('label.iostudentlist.Student'), cellTemplate: this.nameTemplate },
            { headerClass: "text-center", prop: 'totalPrice', name: gT('label.iostudentlist.TotalPrice'), cellTemplate: this.totalPriceTemplate, cellClass: 'text-right' },
            { headerClass: "text-center", prop: 'note', name: gT('label.iostudentlist.Note'), cellTemplate: this.descriptionTemplate },
            { name: '', width: 80, cellTemplate: this.actionsTemplate, resizeable: false, canAutoResize: false, sortable: false, draggable: false }
        ];

        //
        this.getFromServer();

        //
    }

    ngOnDestroy() {
        //this.saveToDisk();
    }

    goDetails(template: TemplateRef<any>, value: IOStockReport) {
        var url = '';
        if (value != null && value.ioTypeId == 1) { url = '/iooutput'; } else { url = '/ioinput'; }
        this.router.navigate([url, value.id]);
    }

    onRemoved(file: any) {
        // do some stuff with the removed file.
    }

    onUploadStateChanged(state: boolean) {
        console.log(JSON.stringify(state));
    }

    onSearchChanged(value: string) {
        this.filterValue = value;
        this.getFromServer();
    }

    private getFromServer() {
        this.loadingIndicator = true;
        //
        var disp = this.localService.getiobyiotypeid(this.filterName, this.filterValue, this.fromDate, this.toDate, this.page.pageNumber, this.page.size).subscribe(
            list => this.onDataLoadSuccessful(list),
            error => this.onDataLoadFailed(error),
            () => {
                disp.unsubscribe();
                setTimeout(() => { this.loadingIndicator = false; }, 1500);
            });
    }

    private onDataLoadSuccessful(resulted: Results<IOStockReport>) {
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

    close() {
        this.modalRef.hide();
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
}
