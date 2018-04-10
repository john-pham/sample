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
import 'rxjs/add/operator/map';

import { ConfigurationService } from './configuration.service';
import { JwtHelper } from './jwt-helper';
import { FeatureGroups } from "../models/featuregroups.model";
import { FeatureGroupsEndpoint } from "./featuregroup-endpoint.service";

@Injectable()
export class FeatureGroupsService {

    constructor(private router: Router, private configurations: ConfigurationService, private endpointFactory: FeatureGroupsEndpoint) {
        this.initializeStatus();
    }

    getAll() {
        return this.endpointFactory.getall()
            .map((response: Response) => <FeatureGroups[]>response.json());
    }
       
    private initializeStatus() {

    }
}