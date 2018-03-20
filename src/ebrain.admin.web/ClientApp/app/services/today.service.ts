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
import { TodayEndpoint } from "./today-endpoint.service";
import { Today } from "../models/today.model";

@Injectable()
export class TodayService {

    constructor(private router: Router, private configurations: ConfigurationService, private endpointFactory: TodayEndpoint) {
        this.initializeStatus();
    }

    getAll() {
        return this.endpointFactory.getAll()
            .map((response: Response) => <Today[]>response.json());
    }

    private initializeStatus() {

    }
}