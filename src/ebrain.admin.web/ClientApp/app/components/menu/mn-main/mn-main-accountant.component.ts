// ======================================
// Author: Ebrain Team
// Email:  johnpham@ymail.com
// Copyright (c) 2017 supperbrain.visualstudio.com
// 
// ==> Contact Us: supperbrain@outlook.com
// ======================================

import { Component, OnInit, OnDestroy, TemplateRef, ViewChild } from '@angular/core';
import { fadeInOut } from '../../../services/animations';

@Component({
    selector: 'mn_main_accountant',
    templateUrl: './mn-main-accountant.component.html',
    styleUrls: ['./mn-main-accountant.component.css'],
    animations: [fadeInOut]
})

export class mn_main_accountantComponent implements OnInit, OnDestroy {
    ngOnDestroy() { }
    ngOnInit() { }
}