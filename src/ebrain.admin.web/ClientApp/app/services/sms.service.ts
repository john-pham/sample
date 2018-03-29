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
import { Branch } from '../models/branch.model';
import { Results } from '../models/results.model';
import 'rxjs/add/operator/map';

import { SMSEndpoint } from './sms-endpoint.service';
import { ConfigurationService } from './configuration.service';
import { JwtHelper } from './jwt-helper';
import { SMS } from "../models/sms.model";

@Injectable()
export class SMSService {

    constructor(private router: Router, private configurations: ConfigurationService, private endpointFactory: SMSEndpoint) {
        this.initializeStatus();
    }
    
    search(filter: string, value: string, page: number, size: number) {
        return this.endpointFactory.search(filter, value, page, size)
            .map((response: Response) => <Results<SMS>>response.json());
    }

    get(index: string) {
        return this.endpointFactory.get(index)
            .map((response: Response) => <SMS>response.json());
    }

    save(value: SMS) {
        return this.endpointFactory.save(value)
            .map((response: Response) => <SMS>response.json());
    }
    

    delete(id: string) {
        return this.endpointFactory.delete(id)
            .map((response: Response) => <Boolean>response.json());
    }

    private initializeStatus() {
        
    }
}