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

import { UserGroupsEndpoint } from './usergroup-endpoint.service';
import { ConfigurationService } from './configuration.service';
import { JwtHelper } from './jwt-helper';
import { UserGroups } from "../models/usergroups.model";

@Injectable()
export class UserGroupsService {

    constructor(private router: Router, private configurations: ConfigurationService, private endpointFactory: UserGroupsEndpoint) {
        this.initializeStatus();
    }

    getAll() {
        return this.endpointFactory.getall()
            .map((response: Response) => <UserGroups[]>response.json());
    }
       
    private initializeStatus() {

    }
}