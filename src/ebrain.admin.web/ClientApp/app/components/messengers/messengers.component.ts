// ======================================
// Author: Ebrain Team
// Email:  johnpham@ymail.com
// Copyright (c) 2017 supperbrain.visualstudio.com
// 
// ==> Contact Us: supperbrain@outlook.com
// ======================================

import { Component, OnInit, OnDestroy, TemplateRef, ViewChild } from '@angular/core';

import { BsModalService } from 'ngx-bootstrap/modal';
import { BsModalRef } from 'ngx-bootstrap/modal/bs-modal-ref.service';

import { fadeInOut } from '../../services/animations';
import { AppTranslationService } from "../../services/app-translation.service";
import { MessengerService } from "../../services/messengers.service";
import { AlertService, MessageSeverity, DialogType } from '../../services/alert.service';
import { Utilities } from '../../services/utilities';
import { File } from '../../models/file.model';
import { Messenger } from '../../models/messenger.model';
import { Results } from '../../models/results.model';
import { Page } from '../../models/page.model';

@Component({
    selector: 'messengers',
    templateUrl: './messengers.component.html',
    styleUrls: ['./messengers.component.css'],
    animations: [fadeInOut]
})

export class MessengerComponent implements OnInit, OnDestroy {
   
    loadingIndicator: boolean = true;

    isSendMessenger: boolean = true;

    constructor(private alertService: AlertService, private translationService: AppTranslationService, private localService: MessengerService,
        private modalService: BsModalService) {
       
    }

    ngOnInit() {

     
    }

    ngOnDestroy() {
        //this.saveToDisk();
    }
    
}
