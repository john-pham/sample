// ======================================
// Author: Ebrain Team
// Email:  johnpham@ymail.com
// Copyright (c) 2017 supperbrain.visualstudio.com
// 
// ==> Contact Us: supperbrain@outlook.com
// ======================================

import { Component, OnInit, OnDestroy, TemplateRef, ViewChild } from '@angular/core';
import { fadeInOut } from '../../../services/animations';
import { AccessRightsService } from "../../../share/services/access-rights.service";
@Component({
    selector: 'mn_main_sms',
    templateUrl: './mn-main-sms.component.html',
    styleUrls: ['./mn-main-sms.component.css'],
    animations: [fadeInOut]
})

export class mn_main_smsComponent implements OnInit, OnDestroy {
    constructor(public accessRightService: AccessRightsService) {

    }
    ngOnDestroy() { }
    ngOnInit() { }
}
