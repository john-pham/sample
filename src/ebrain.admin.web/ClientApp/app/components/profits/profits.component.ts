// ======================================
// Author: Ebrain Team
// Email:  johnpham@ymail.com
// Copyright (c) 2017 supperbrain.visualstudio.com
// 
// ==> Contact Us: supperbrain@outlook.com
// ======================================

import { Component, OnInit, OnDestroy, TemplateRef, ViewChild } from '@angular/core';
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
import { ProfitsService } from "../../services/profits.service";
import { Profit } from "../../models/profits.model";
import { AccessRightsService } from "../../share/services/access-rights.service";
@Component({
    selector: 'profits',
    templateUrl: './profits.component.html',
    styleUrls: ['./profits.component.css'],
    animations: [fadeInOut]
})

export class ProfitsComponent implements OnInit, OnDestroy {
    rows = [];
    columns = [];
    loadingIndicator: boolean = true;

    filterName: string;
    filterValue: string;
    fromDate: Date;
    toDate: Date;

    public changesSavedCallback: () => void;
    public changesFailedCallback: () => void;
    public changesCancelledCallback: () => void;

    modalRef: BsModalRef;

    constructor(private alertService: AlertService, private router: Router, private translationService: AppTranslationService, private localService: ProfitsService, public accessRightService: AccessRightsService,private modalService: BsModalService) {
        var date = new Date(), y = date.getFullYear(), m = date.getMonth();
        this.fromDate = new Date(y, m, 1);
        this.toDate = new Date(y, m + 1, 0);
        this.filterValue = '';
    }

    ngOnInit() {
       
    }

    ngOnDestroy() {
        //this.saveToDisk();
    }

    onRemoved(file: any) {
        // do some stuff with the removed file.
    }

    onUploadStateChanged(state: boolean) {
        console.log(JSON.stringify(state));
    }

    onSearchChanged(value: string) {
        this.filterValue = value;
    }

    private updated() {
        this.loadingIndicator = true;
        //
        var disp = this.localService.updateProfits(this.filterName, this.filterValue, this.fromDate, this.toDate).subscribe(
            list => this.onDataLoadSuccessful(list),
            error => this.onDataLoadFailed(error),
            () => {
                disp.unsubscribe();
                setTimeout(() => { this.loadingIndicator = false; }, 1500);
            });
    }

    private onDataLoadSuccessful(list: boolean) {
        this.alertService.stopLoadingMessage();
    }

    private onDataLoadFailed(error: any) {
        this.alertService.stopLoadingMessage();
        this.alertService.showStickyMessage("Load Error", `Unable to retrieve user data from the server.\r\nErrors: "${Utilities.getHttpResponseMessage(error)}"`,
            MessageSeverity.error, error);
    }


    close() {
        this.modalRef.hide();
    }
    
}
