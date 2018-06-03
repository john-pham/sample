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

@Component({
    selector: 'classliststeachers',
    templateUrl: './classliststeachers.component.html',
    styleUrls: ['./classliststeachers.component.css'],
    animations: [fadeInOut]
})

export class ClassListTeachersComponent implements OnInit, OnDestroy {
   
    public changesSavedCallback: () => void;
    public changesFailedCallback: () => void;
    public changesCancelledCallback: () => void;

    modalRef: BsModalRef;
    private page: Page;
    constructor(private alertService: AlertService, private router: Router, private translationService: AppTranslationService,
        private localService: ClassesService, private supplierService: SuppliersService,
        private classStatusService: ClassStatusService, public accessRightService: AccessRightsService,
        private modalService: BsModalService) {
      
    }

    ngOnInit() { }
    ngOnDestroy() {
        //this.saveToDisk();
    }

    
}
