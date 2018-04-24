// ======================================
// Author: Ebrain Team
// Email:  johnpham@ymail.com
// Copyright (c) 2017 supperbrain.visualstudio.com
// 
// ==> Contact Us: supperbrain@outlook.com
// ======================================

import { Component, OnInit, OnDestroy, TemplateRef, ViewChild, ElementRef } from '@angular/core';
import { Router } from '@angular/router';
import { ActivatedRoute, ParamMap } from '@angular/router';

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

@Component({
    selector: 'warehousecards',
    templateUrl: './warehousecards.component.html',
    styleUrls: ['./warehousecards.component.css'],
    animations: [fadeInOut]
})

export class WarehouseCardsComponent implements OnInit, OnDestroy {
    rows = [];
    columns = [];
    loadingIndicator: boolean = true;

    filterName: string = "";
    filterValue: string = "";
    fromDate: Date;
    toDate: Date;

    private pointer: Grpsupplier;

    public changesSavedCallback: () => void;
    public changesFailedCallback: () => void;
    public changesCancelledCallback: () => void;

    modalRef: BsModalRef;

    constructor(private alertService: AlertService, private translationService: AppTranslationService,
        private localService: IOStudentListService, private modalService: BsModalService, private router: Router, private route: ActivatedRoute) {
        this.pointer = new Grpsupplier();
        var date = new Date(), y = date.getFullYear(), m = date.getMonth();
        this.fromDate = new Date(y, m, 1);
        this.toDate = new Date(y, m + 1, 0);
        this.filterValue = '';
    }

    ngOnInit() {
        let gT = (key: string) => this.translationService.getTranslation(key);

        this.columns = [
            { headerClass: "text-center", prop: 'code', name: gT('label.iostudentlist.Code'), cellTemplate: this.nameTemplate },
            { headerClass: "text-center", prop: 'createDate', name: gT('label.iostudentlist.CreateDate'), cellTemplate: this.nameTemplate },
            { headerClass: "text-center", prop: 'fullName', name: gT('label.iostudentlist.CreateUser'), cellTemplate: this.nameTemplate },
            { headerClass: "text-center", prop: 'studentName', name: gT('label.iostudentlist.Student'), cellTemplate: this.nameTemplate },
            { headerClass: "text-center", prop: 'materialCode', name: gT('label.iostudentlist.MaterialCode'), cellTemplate: this.nameTemplate },
            { headerClass: "text-center", prop: 'materialName', name: gT('label.iostudentlist.MaterialName'), cellTemplate: this.nameTemplate },
            { headerClass: "text-center", prop: 'quantityInput', name: gT('label.iostudentlist.QuantityInput'), cellTemplate: this.nameTemplate, cellClass: 'text-right' },
            { headerClass: "text-center", prop: 'quantityOutput', name: gT('label.iostudentlist.QuantityOutput'), cellTemplate: this.nameTemplate, cellClass: 'text-right' },
            { headerClass: "text-center", prop: 'note', name: gT('label.iostudentlist.Note'), cellTemplate: this.descriptionTemplate }
        ];

        //
        this.getFromServer();

        //
    }

    ngOnDestroy() {
        //this.saveToDisk();
    }

    onSearchChanged(value: string) {
        this.filterValue = value;
    }

    search() {
        this.router.navigate(["/warehousecards", ""]);
        this.getFromServer();
    }

    private getFromServer() {
        this.loadingIndicator = true;

        this.route.paramMap
            .switchMap((params: ParamMap) => {
                var id = params.get('id');
                this.filterValue = id;
                
                return this.localService.getWarehouseCard(this.filterName, this.filterValue, this.fromDate, this.toDate);
            })
            .subscribe(results => this.onDataLoadSuccessful(results), error => this.onDataLoadFailed(error));
    }

    private onDataLoadSuccessful(list: IOStockReport[]) {
        this.rows = list;
        this.alertService.stopLoadingMessage();
        this.loadingIndicator = false;
        this.filterValue = "";
    }

    private onDataLoadFailed(error: any) {
        this.alertService.stopLoadingMessage();
        this.alertService.showStickyMessage("Load Error", `Unable to retrieve user data from the server.\r\nErrors: "${Utilities.getHttpResponseMessage(error)}"`,
            MessageSeverity.error, error);

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
