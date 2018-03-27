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
import { InventoriesService } from "../../services/inventories.service";
import { Inventories } from "../../models/inventories.model";

@Component({
    selector: 'inventorieslist',
    templateUrl: './inventorieslists.component.html',
    styleUrls: ['./inventorieslists.component.css'],
    animations: [fadeInOut]
})

export class InventoriesListsComponent implements OnInit, OnDestroy {
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

    constructor(private alertService: AlertService, private router: Router, private translationService: AppTranslationService, private localService: InventoriesService, private modalService: BsModalService) {
        var date = new Date(), y = date.getFullYear(), m = date.getMonth();
        this.fromDate = new Date(y, m, 1);
        this.toDate = new Date(y, m + 1, 0);
        this.filterValue = '';
    }

    ngOnInit() {
        let gT = (key: string) => this.translationService.getTranslation(key);

        this.columns = [
            { headerClass: "text-center", prop: 'code', name: gT('label.inventorieslist.Code'), cellTemplate: this.nameTemplate },
            { headerClass: "text-center", prop: 'name', name: gT('label.inventorieslist.Name'), cellTemplate: this.nameTemplate },
            { headerClass: "text-center", prop: 'typeName', name: gT('label.inventorieslist.TypeName'), cellTemplate: this.nameTemplate },
            { headerClass: "text-center", prop: 'grpName', name: gT('label.inventorieslist.GrpName'), cellTemplate: this.nameTemplate },
            { headerClass: "text-center", prop: 'quantityInv', name: gT('label.inventorieslist.QuantityInv'), cellTemplate: this.totalPriceTemplate, cellClass: 'text-right'},
            { headerClass: "text-center", prop: 'quantityInput', name: gT('label.inventorieslist.QuantityInput'), cellTemplate: this.totalPriceTemplate, cellClass: 'text-right' },
            { headerClass: "text-center", prop: 'quantityOutput', name: gT('label.inventorieslist.QuantityOutput'), cellTemplate: this.totalPriceTemplate, cellClass: 'text-right' },
            { headerClass: "text-center", prop: 'quantityEnd', name: gT('label.inventorieslist.QuantityEnd'), cellTemplate: this.totalPriceTemplate, cellClass: 'text-right' },

            { name: '', width: 80, cellTemplate: this.actionsTemplate, resizeable: false, canAutoResize: false, sortable: false, draggable: false }
        ];

        //
        this.search();

        //
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
        //this.rows = this.rowsCache.filter(r => Utilities.searchArray(value, false, r.name, r.description) || value == 'important' && r.important || value == 'not important' && !r.important);
    }

    private search() {
        this.loadingIndicator = true;
        //
        var disp = this.localService.getInventories(this.filterName, this.filterValue, this.fromDate, this.toDate).subscribe(
            list => this.onDataLoadSuccessful(list),
            error => this.onDataLoadFailed(error),
            () => {
                disp.unsubscribe();
                setTimeout(() => { this.loadingIndicator = false; }, 1500);
            });
    }

    private onDataLoadSuccessful(list: Inventories[]) {
        this.rows = list;
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
