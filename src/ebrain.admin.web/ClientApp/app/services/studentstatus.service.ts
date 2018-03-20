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
import { Studentstatus } from '../models/studentstatus.model';
import 'rxjs/add/operator/map';

import { StudentstatusEndpoint } from './studentstatus-endpoint.service';
import { ConfigurationService } from './configuration.service';
import { JwtHelper } from './jwt-helper';

@Injectable()
export class StudentstatusService {

    constructor(private router: Router, private configurations: ConfigurationService, private endpointFactory: StudentstatusEndpoint) {
        this.initializeStatus();
    }

    search(filter: string, value: string) {
        return this.endpointFactory.search(filter, value)
            .map((response: Response) => <Studentstatus[]>response.json());
    }

    get(index: string) {
        return this.endpointFactory.get(index)
            .map((response: Response) => <Studentstatus>response.json());
    }

    save(value: Studentstatus) {
        return this.endpointFactory.save(value)
            .map((response: Response) => <Studentstatus>response.json());
    }

    delete(id: string) {
        return this.endpointFactory.delete(id);
    }

    private initializeStatus() {

    }
}