// ======================================
// Author: Ebrain Team
// Email:  johnpham@ymail.com
// Copyright (c) 2017 supperbrain.visualstudio.com
// 
// ==> Contact Us: supperbrain@outlook.com
// ======================================

import { Injectable } from '@angular/core';
import { Router, NavigationExtras } from "@angular/router";
import { Response } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import { Subject } from 'rxjs/Subject';
import 'rxjs/add/operator/map';

import { mn_classesEndpoint } from './mn-classes-endpoint.service';
import { ConfigurationService } from './configuration.service';
import { JwtHelper } from './jwt-helper';

@Injectable()
export class mn_classesService {

    constructor(private router: Router, private configurations: ConfigurationService, private endpointFactory: mn_classesEndpoint) {
        this.initializeStatus();
    }
    
    search(filter: string, value: string) {
        return this.endpointFactory.getDataEndpoint(filter, value)
            .map((response: Response) => this.processSearchResponse(response));
    }

    private processSearchResponse(response: Response) {
    }

    private initializeStatus() {
        
    }
}