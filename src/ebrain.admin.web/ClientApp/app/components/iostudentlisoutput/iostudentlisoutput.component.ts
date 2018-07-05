// ======================================
// Author: Ebrain Team
// Email:  johnpham@ymail.com
// Copyright (c) 2017 supperbrain.visualstudio.com
// 
// ==> Contact Us: supperbrain@outlook.com
// ======================================

import { Component, OnInit, OnDestroy, TemplateRef, ViewChild, Input, Output, EventEmitter } from '@angular/core';
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
import { AccessRightsService } from "../../services/access-rights.service";
import { Page } from "../../models/page.model";
import { Results } from "../../models/results.model";
import { Chart } from "../../models/chart.model";
import { IOStockDetail } from "../../models/iostockdetail.model";
@Component({
    selector: 'iostudentlisoutput',
    templateUrl: './iostudentlisoutput.component.html',
    styleUrls: ['./iostudentlisoutput.component.css'],
    animations: [fadeInOut]
})

export class IOStudenListOutputComponent implements OnInit, OnDestroy {

    public changesSavedCallback: () => void;
    public changesFailedCallback: () => void;
    public changesCancelledCallback: () => void;


    constructor(private alertService: AlertService, private translationService: AppTranslationService,
        public accessRightService: AccessRightsService,
        private localService: IOStudentListService, private modalService: BsModalService, private router: Router) {

    }

    ngOnInit() {
    }

    ngOnDestroy() {
        //this.saveToDisk();
    }

}
