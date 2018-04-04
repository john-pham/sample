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
    selector: 'mn_main_document',
    templateUrl: './mn-main-document.component.html',
    styleUrls: ['./mn-main-document.component.css'],
    animations: [fadeInOut]
})

export class mn_main_documentComponent implements OnInit, OnDestroy {
    ngOnDestroy() { }
    ngOnInit() { }
}
