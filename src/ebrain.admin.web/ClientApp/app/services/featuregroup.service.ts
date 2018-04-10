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
import { Results } from "../models/results.model";

@Injectable()
export class FeatureGroupsService {

    constructor(private router: Router, private configurations: ConfigurationService, private endpointFactory: FeatureGroupsEndpoint) {
        this.initializeStatus();
    }

    getAll() {
        return this.endpointFactory.getall()
            .map((response: Response) => <FeatureGroups[]>response.json());
    }

    search(filter: string, value: string, page: number, size: number) {
        return this.endpointFactory.search(filter, value, page, size)
            .map((response: Response) => <Results<FeatureGroups>>response.json());
    }

    get(index: string) {
        return this.endpointFactory.get(index)
            .map((response: Response) => <FeatureGroups>response.json());
    }

    save(value: FeatureGroups) {
        return this.endpointFactory.save(value)
            .map((response: Response) => <FeatureGroups>response.json());
    }


    delete(id: string) {
        return this.endpointFactory.delete(id)
            .map((response: Response) => <Boolean>response.json());
    }

    private initializeStatus() {

    }
}