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
import { GenderStudent } from '../models/genderstudent.model';
import 'rxjs/add/operator/map';

import { GenderStudentEndpoint } from './genderstudent-endpoint.service';
import { ConfigurationService } from './configuration.service';
import { JwtHelper } from './jwt-helper';

@Injectable()
export class GenderStudentService {

    constructor(private router: Router, private configurations: ConfigurationService, private endpointFactory: GenderStudentEndpoint) {
        this.initializeStatus();
    }
    
    getall(index: string) {
        return this.endpointFactory.getall()
            .map((response: Response) => <GenderStudent>response.json());
    }
    
    private initializeStatus() {
    }
}