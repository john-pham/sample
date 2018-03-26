// ======================================
// Author: Ebrain Team
// Email:  johnpham@ymail.com
// Copyright (c) 2017 supperbrain.visualstudio.com
// 
// ==> Contact Us: supperbrain@outlook.com
// ======================================

import { Component, OnInit, OnDestroy, TemplateRef, ViewChild } from '@angular/core';

import { fadeInOut } from '../../services/animations';
import { AppTranslationService } from "../../services/app-translation.service";
import { mn_financesService } from "../../services/mn-finances.service";
import { AlertService, MessageSeverity, DialogType } from '../../services/alert.service';
import { Utilities } from '../../services/utilities';

@Component({
    selector: 'mn-finances',
    templateUrl: './mn-finances.component.html',
    styleUrls: ['./mn-finances.component.css'],
    animations: [fadeInOut]
})

export class mn_financesComponent implements OnInit, OnDestroy {
    rows = [];
    columns = [];
    loadingIndicator: boolean = true;

    filterName: string;
    filterValue: string;

    constructor(private alertService: AlertService, private translationService: AppTranslationService, private localService: mn_financesService) {
    }

    ngOnInit() {
        this.loadingIndicator = true;

        let gT = (key: string) => this.translationService.getTranslation(key);

        this.columns = [
            { headerClass: "text-center", prop: "completed", name: '', width: 30, headerTemplate: this.statusHeaderTemplate, cellTemplate: this.statusTemplate, resizeable: false, canAutoResize: false, sortable: false, draggable: false },
            { headerClass: "text-center", prop: 'name', name: gT('todoDemo.management.Task'), cellTemplate: this.nameTemplate, width: 200 },
            { headerClass: "text-center", prop: 'description', name: gT('todoDemo.management.Description'), cellTemplate: this.descriptionTemplate, width: 500 },
            { name: '', width: 80, cellTemplate: this.actionsTemplate, resizeable: false, canAutoResize: false, sortable: false, draggable: false }
        ];

        this.fetch((data) => {
            this.rows = data;

            setTimeout(() => { this.loadingIndicator = false; }, 1500);
        });
    }

    ngOnDestroy() {
        //this.saveToDisk();
    }

    fetch(cb) {
        let data = this.getFromServer()

        //if (data == null) {
        //    setTimeout(() => {

        //        data = [
        //            { "completed": true, "important": true, "name": "Create visual studio extension", "description": "Create a visual studio VSIX extension package that will add this project as an aspnet-core project template" },
        //            { "completed": false, "important": true, "name": "Do a quick how-to writeup", "description": "" },
        //            {
        //                "completed": false, "important": false, "name": "Create aspnet-core/angular2 tutorials based on this project", "description": "Create tutorials (blog/video/youtube) on how to build applications (full stack)" +
        //                " using aspnet-core/angular2. The tutorial will focus on getting productive with the technology right away rather than the details on how and why they work so audience can get onboard quickly."
        //            },
        //        ];

        //        cb(data);
        //    }, 1000);
        //}
        //else {
        //    cb(data);
        //}
    }

    getFromServer() {
        return this.localService.search(this.filterName, this.filterValue).subscribe(list => this.onDataLoadSuccessful(list), error => this.onDataLoadFailed(error));
    }

    private onDataLoadSuccessful(list: any) {
        this.alertService.stopLoadingMessage();
    }

    private onDataLoadFailed(error: any) {
        this.alertService.stopLoadingMessage();
        this.alertService.showStickyMessage("Load Error", `Unable to retrieve user data from the server.\r\nErrors: "${Utilities.getHttpResponseMessage(error)}"`,
            MessageSeverity.error, error);

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
}
