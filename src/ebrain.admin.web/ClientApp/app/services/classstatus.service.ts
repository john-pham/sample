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
import { Unit } from '../models/unit.model';
import 'rxjs/add/operator/map';

import { UnitsEndpoint } from './units-endpoint.service';
import { ConfigurationService } from './configuration.service';
import { JwtHelper } from './jwt-helper';
import { ClassStatusEndpoint } from "./classstatus-endpoint.service";
import { ClassStatus } from "../models/classstatus.model";

@Injectable()
export class ClassStatusService {

    constructor(private router: Router, private configurations: ConfigurationService, private endpointFactory: ClassStatusEndpoint) {
        this.initializeStatus();
    }

    getAll() {
        return this.endpointFactory.getall()
            .map((response: Response) => <ClassStatus[]>response.json());
    }

    private initializeStatus() {

    }
}