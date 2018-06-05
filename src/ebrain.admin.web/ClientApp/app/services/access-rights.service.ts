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

import { AccessRightsEndpoint } from './access-rights.endpoint';
import { ConfigurationService } from './configuration.service';
import { JwtHelper } from './jwt-helper';
import { AccessRight } from "../models/accessright.model";
import { Utilities } from "./utilities";
import { LocalStoreManager } from "./local-store-manager.service";
import { DBkeys } from "./db-Keys";

@Injectable()
export class AccessRightsService {

    constructor(private router: Router, private configurations: ConfigurationService, private endpointFactory: AccessRightsEndpoint,
        private localStorage: LocalStoreManager) {
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

    getAccessRightPerson(featureId: string, value: string) {
        return this.endpointFactory.getAccessRightPerson(featureId, value)
            .map((response: Response) => <Results<AccessRight>>response.json());
    }

    saveAccessRightPerson(value: AccessRight[]) {
        return this.endpointFactory.saveAccessRightPerson(value)
            .map((response: Response) => <boolean>response.json());
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
    get currentAccessRights(): AccessRight[] {
        let access = this.localStorage.getDataObject<AccessRight[]>(DBkeys.PERMISSIONS_ACCESSRIGHTS);
        return access;
    }

    private isViewFeatureGroup(featureGroupId: string) {
        let accessRights = this.currentAccessRights;
        if (accessRights != null && accessRights.length > 0) {
            var array = accessRights.filter(p => p.featureGroupId == featureGroupId.toLowerCase() && p.view == true);
            return array != null && array.length > 0;
        }
        return false;
    }

    private isViewFeature(featureId: string) {
        let accessRights = this.currentAccessRights;
        if (accessRights != null && accessRights.length > 0) {
            var array = accessRights.filter(p => p.featureId == featureId.toLowerCase() && p.view == true);
            return array != null && array.length > 0;
        }
        return false;
    }

    public isEdit(featureId: string) {
        let accessRights = this.currentAccessRights;
        if (accessRights != null && accessRights.length > 0) {
            var array = accessRights.filter(p => p.featureId == featureId.toLowerCase() && p.edit == true);
            return array != null && array.length > 0;
        }
        return false;
    }

    private isCreate(featureId: string) {
        let accessRights = this.currentAccessRights;
        if (accessRights != null && accessRights.length > 0) {
            var array = accessRights.filter(p => p.featureId == featureId.toLowerCase() && p.create == true);
            return array != null && array.length > 0;
        }
        return false;
    }

    private isDelete(featureId: string) {
        let accessRights = this.currentAccessRights;
        if (accessRights != null && accessRights.length > 0) {
            var array = accessRights.filter(p => p.featureId == featureId.toLowerCase() && p.delete == true);
            return array != null && array.length > 0;
        }
        return false;
    }
}