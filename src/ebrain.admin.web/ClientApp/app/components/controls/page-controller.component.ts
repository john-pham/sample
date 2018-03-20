// ======================================
// Author: Ebrain Team
// Email:  johnpham@ymail.com
// Copyright (c) 2017 supperbrain.visualstudio.com
// 
// ==> Contact Us: supperbrain@outlook.com
// ======================================

import { Component, OnInit, ViewChild, Input } from '@angular/core';

import { AlertService, MessageSeverity } from '../../services/alert.service';
import { Utilities } from '../../services/utilities';

@Component({
    selector: 'page-controller',
    templateUrl: './page-controller.component.html',
    styleUrls: ['./page-controller.component.css']
})

export class PageControllerComponent implements OnInit {
    
    constructor(private alertService: AlertService) {
    }

    ngOnInit() {
    }

}
