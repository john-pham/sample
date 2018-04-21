﻿// ======================================
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

import { AccessRightsEndpoint } from './access-rights.endpoint';
import { ConfigurationService } from './configuration.service';
import { JwtHelper } from './jwt-helper';
import { AccessRight } from "../models/accessright.model";
import { Utilities } from "./utilities";

@Injectable()
export class AccessRightsService {

    constructor(private router: Router, private configurations: ConfigurationService, private endpointFactory: AccessRightsEndpoint) {
        this.initializeStatus();
    }

    search(groupId: string, featureGroupId: string, page: number, size: number) {
        return this.endpointFactory.search(groupId, featureGroupId, page, size)
            .map((response: Response) => <Results<AccessRight>>response.json());
    }

    getAll() {
        return this.endpointFactory.getAll()
            .map((response: Response) => <AccessRight>response.json());
    }

    get(index: string) {
        return this.endpointFactory.get(index)
            .map((response: Response) => <Branch>response.json());
    }

    save(values: AccessRight[]) {
        return this.endpointFactory.save(values)
            .map((response: Response) => <Boolean>response.json());
    }

    saveHead(value: Branch[]) {
        return this.endpointFactory.saveHead(value)
            .map((response: Response) => <Branch[]>response.json());
    }

    delete(id: string) {
        return this.endpointFactory.delete(id)
            .map((response: Response) => <Boolean>response.json());
    }

    getBranchHead(index: string) {
        return this.endpointFactory.getBranchHead(index)
            .map((response: Response) => <Branch[]>response.json());
    }

    private initializeStatus() {

    }

    //permission accessRight

    private isViewFeatureGroup(featureGroupId: string) {
        var accessRights = Utilities.accessRights;
        if (accessRights != null && accessRights.length > 0) {
            var array = accessRights.filter(p => p.featureGroupId == featureGroupId.toLowerCase() && p.view == true);
            return array != null && array.length > 0;
        }
        return false;
    }

    private isViewFeature(featureId: string) {
        var accessRights = Utilities.accessRights;
        if (accessRights != null && accessRights.length > 0) {
            var array = accessRights.filter(p => p.featureId == featureId.toLowerCase() && p.view == true);
            return array != null && array.length > 0;
        }
        return false;
    }
}